using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    public GameObject weapon;

    public Text gameGoal;

    public Text playerHealthText;
    public Text coinCollectedText;
    public Text enemyKilledText;

    public Button resumeButton;
    public Button pauseButton;

    public Transform Lives;

    private Animator playerAnim;
    private Animator swordAnim;
    private int playerHealth;
    private int coinCollected;
    private int enemyKilled;
    private bool immunity;
    private bool healthProgress;

    // Hide unnecessary variables from public view
    [HideInInspector] public Text pauseMessage;
    [HideInInspector] public Text winMessage;
    [HideInInspector] public Text loseMessage;
    [HideInInspector] public Image health1;
    [HideInInspector] public Image health2;
    [HideInInspector] public Image health3;
    [HideInInspector] public int delay = 1;

    // Wake the instance of the game manager
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
        OnSceneLoaded(); // Call the method to start immediately when the game runs

        playerAnim = player.GetComponent<Animator>(); // Get the animator component from player
        swordAnim = weapon.GetComponent<Animator>(); // Get the animator component from weapon

        resumeButton.gameObject.SetActive(false); // Hide the resume button in game scene
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
        // Main scenme will reload
        SceneManager.LoadScene("Main");
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

    // Use to spawn character at a specific location
    public void OnSceneLoaded()
    {
        Time.timeScale = 1;
        gameGoal.gameObject.SetActive(true);
        StartCoroutine("HideGameGoal");

        // Player will find the game object position and assign the position to the player position
        player.transform.position = GameObject.Find("Spawn").transform.position;
    }

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
            Destroy(player.gameObject);
            loseMessage.gameObject.SetActive(true);
        }
    }

    // Congratuilation method to be invoked when player wins
    public void Congratulation()
    {
        winMessage.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
