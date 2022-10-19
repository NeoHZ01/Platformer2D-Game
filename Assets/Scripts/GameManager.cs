using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public GameObject weapon;
    public GameObject HUD;
    public GameObject cmcam1;
    public GameObject eventSystem;

    public Text gameGoal;
    public Text coinCollectedText;
    public Text enemyKilledText;

    public Button resumeButton;
    public Button pauseButton;
    public Button tryAgainButton;
    public Button nextLevelButton;

    public Transform Lives;

    private Animator playerAnim;
    private Animator swordAnim;
    private int playerHealth;
    private int coinCollected;
    private int enemyKilled;
    private bool immunity;
    private bool healthProgress;

    private GameObject cmcam;

    // Hide unnecessary variables from public view
    [HideInInspector] public Text pauseMessage;
     public Text winMessage;
    [HideInInspector] public Text loseMessage;
    [HideInInspector] public Image health1;
    [HideInInspector] public Image health2;
    [HideInInspector] public Image health3;
    [HideInInspector] public int delay = 1;

    // Wake the instance of the game manager
    void Awake()
    {
        // Check if instance is not null - when scene is restarted/reloaded etc, instance will not be empty (game manager is already assigned to it)
        if (GameManager.instance != null)
        {
            // Destroy these objects
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(cmcam1);
            Destroy(HUD);
            Destroy(eventSystem);
            Destroy(Camera.main.gameObject);
        }
        else // When game is first started, instance is empty
        {
            instance = this; // Set current game manager to instance
            //SceneManager.sceneLoaded += LoadState; // Adding additional tasks to sceneloaded which contains a list of delegations
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

        playerAnim = player.GetComponent<Animator>(); // Get the animator component from player
        swordAnim = weapon.GetComponent<Animator>(); // Get the animator component from weapon

        resumeButton.gameObject.SetActive(false); // Hide the resume button in game scene
        tryAgainButton.gameObject.SetActive(false); // Hide the try again button in game scene - only appears when player lose the game
        nextLevelButton.gameObject.SetActive(false); // Hide next level button in game scene - only appears after player win the game
        pauseMessage.gameObject.SetActive(false); // Hide pause message in game scene
        loseMessage.gameObject.SetActive(false); // Hide lose message in game scene until invoked
        winMessage.gameObject.SetActive(false); // Hide win message in game scene until invoked

        immunity = false; // Set immunity to false
        healthProgress = true; // Set immunity to true

        coinCollected = 0;
        enemyKilled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        coinCollectedText.text = coinCollected.ToString(); // Coin collected will be turned to string and assigned as the new text variable in every frame 
        enemyKilledText.text = enemyKilled.ToString(); // Enemy killed will be turned to string and assigned as the new text variable in every frame 

        if (!GameObject.FindGameObjectWithTag("Collectible") && !GameObject.FindGameObjectWithTag("Minion")) // Keep checking for objects tagged as collectibe and Minion
        {
            Congratulation(); // If none is found, player has won - invoke congratulation method
        }
    }

    // To reduce health of the player
    public void ReduceHealth()
    {
        if (Lives.GetChild(2).gameObject.activeInHierarchy && healthProgress && !immunity)
        {
            Lives.GetChild(2).gameObject.SetActive(false); // Hide life
            healthProgress = false; // Turn healthProgress to false
            immunity = true;
            TurnHealthProgressPositive(); // Invoke method to turn back to true;
        }
        else if (Lives.GetChild(1).gameObject.activeInHierarchy && healthProgress && !immunity)
        {
            Lives.GetChild(1).gameObject.SetActive(false);
            healthProgress = false;
            immunity = true;
            TurnHealthProgressPositive(); // Invoke method to turn back to true;
        }
        else if (Lives.GetChild(0).gameObject.activeInHierarchy && healthProgress && !immunity)
        {
            Lives.GetChild(0).gameObject.SetActive(false);
            healthProgress = false;
            immunity = true;
            TurnHealthProgressPositive(); // Invoke method to turn back to true;
        }
        else if (!Lives.GetChild(0).gameObject.activeInHierarchy && healthProgress && !immunity)
        {
            Die();
        }
    }

    // To ensure player will be immune from any enemies for a fixed amount of time after receiving damage
    public void TurnHealthProgressPositive()
    {
        if (!healthProgress)
        {
            playerAnim.SetBool("Damaged", true);
            swordAnim.SetBool("Damaged", true);
            StartCoroutine("DelayHealthProgress");
        }
    }

    // Coroutine for the immune system
    IEnumerator DelayHealthProgress()
    {
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(1);

        playerAnim.SetBool("Damaged", false);
        swordAnim.SetBool("Damaged", false);
        healthProgress = true;
        immunity = false;
    }

    // To increment kill count when player kill an enemy in the game
    public void AddKillCount()
    {
        enemyKilled = enemyKilled + 1;
    }

    // To increment the coin collected during the game
    public void AddCoinCount()
    {
        coinCollected = coinCollected + 1;
    }

    // When player click restart
    public void Restart()
    {
        Debug.Log("Restart");

        //Destroy(cmcam); // Destroy 

        coinCollected = 0;
        enemyKilled = 0;

        loseMessage.gameObject.SetActive(false);
        tryAgainButton.gameObject.SetActive(false);

        // Main scene will reload
        SceneManager.LoadScene("Level1");
    }

    // Pause the game when pause button is pressed
    public void Pause()
    {
        Time.timeScale = 0;
        resumeButton.gameObject.SetActive(true); // Show the resume button
        pauseButton.gameObject.SetActive(false); // Hide the pause button
        pauseMessage.gameObject.SetActive(true); // Show pause message
    }

    // Resume the game when resume button is pressed
    public void Resume()
    {
        Time.timeScale = 1;
        resumeButton.gameObject.SetActive(false); // Hide the resume button
        pauseButton.gameObject.SetActive(true); // Show the pause button
        pauseMessage.gameObject.SetActive(false); // Hide pause message
    }

    // Load Scene 2
    public void NextLevel()
    {
        winMessage.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);

        Debug.Log(winMessage);

        SceneManager.LoadScene("Level2");


        //winMessage.gameObject.SetActive(false);
        //nextLevelButton.gameObject.SetActive(false);

        //player.transform.position = GameObject.Find("Spawn").transform.position;
    }

    // Use to spawn character at a specific location
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        Time.timeScale = 1;
        cmcam = GameObject.Find("CM vcam1");
        cmcam.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;

        HUD = GameObject.Find("HUD");

        gameGoal.gameObject.SetActive(true);
        StartCoroutine("HideGameGoal");

        winMessage.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);

        // Player will find the game object position and assign the position to the player position
        player.transform.position = GameObject.Find("Spawn").transform.position;
    }

    // Use to spawn character at a specific location
    /*public void OnSceneLoaded()
    {
        Time.timeScale = 1;
        gameGoal.gameObject.SetActive(true);
        StartCoroutine("HideGameGoal");


        //winMessage.gameObject.SetActive(false);
        //nextLevelButton.gameObject.SetActive(false);

        // Player will find the game object position and assign the position to the player position
        player.transform.position = GameObject.Find("Spawn").transform.position;
    } */

    //public void LoadState(Scene s, LoadSceneMode mode) // Sceneloaded comes with two parameter (scene and loadscenemode)
    //{
    //    // Sceneloaded is a list of delegations
    //    // In this case, I am adding additional tasks to the list of delegations
    //    SceneManager.sceneLoaded += LoadState; // Sceneloaded is an array of functions
    //}

    // Coroutine to hide game goal message after the game is started
    IEnumerator HideGameGoal()
    {
        yield return new WaitForSeconds(3);

        gameGoal.gameObject.SetActive(false);
    }

    // Die method to be invoked when player loses
    public void Die()
    {
        if (!health1.gameObject.activeInHierarchy)
        {
            loseMessage.gameObject.SetActive(true);
            tryAgainButton.gameObject.SetActive(true);
        }
    }

    // Congratuilation method to be invoked when player wins
    public void Congratulation()
    {
        winMessage.gameObject.SetActive(true);
        nextLevelButton.gameObject.SetActive(true);
    }
}
