using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;
    
    public Text playerHealthText;
    public Text coinCollectedText;
    public Text enemyKilledText;

    public Button resumeButton;
    public Button pauseButton;

    private Animator playerAnim;
    private int playerHealth;
    //private int tempHealth;
    private int coinCollected;
    private int enemyKilled;
    private bool immunity;
    private bool healthProgress;

    // Hide unnecessary variables from public view
    [HideInInspector] public Text pauseMessage;
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

        resumeButton.gameObject.SetActive(false); // Hide the resume button in game scene
        pauseMessage.gameObject.SetActive(false); // HJide pause message in game scene
        loseMessage.gameObject.SetActive(false); // Hide lose message in game scene

        immunity = false; // Set immunity to false
        healthProgress = true; // Set immunity to true

        //playerAnim = player.GetComponent<Animator>();

        //playerHealth = 3;
        coinCollected = 0;
        enemyKilled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //playerHealthText.text = playerHealth.ToString();
        coinCollectedText.text = coinCollected.ToString();
        enemyKilledText.text = enemyKilled.ToString();

    }

    public void ReduceHealth()
    {
        if (health3.gameObject.activeInHierarchy && healthProgress && !immunity)
        {
            health3.gameObject.SetActive(false); // Hide life
            healthProgress = false; // Turn healthProgress to false
            immunity = true;
            TurnHealthProgressPositive(); // Invoke method to turn back to true;
        }
        else if (health2.gameObject.activeInHierarchy && healthProgress && !immunity)
        {
            health2.gameObject.SetActive(false);
            healthProgress = false;
            immunity = true;
            TurnHealthProgressPositive(); // Invoke method to turn back to true;
        }
        else if (health1.gameObject.activeInHierarchy && healthProgress && !immunity)
        {
            health1.gameObject.SetActive(false);
            healthProgress = false;
            immunity = true;
            TurnHealthProgressPositive(); // Invoke method to turn back to true;
        }
        else if (!health1.gameObject.activeInHierarchy && healthProgress && !immunity)
        {
            Die();
        }
    }

    public void TurnHealthProgressPositive()
    {
        if (!healthProgress)
        {
            StartCoroutine("DelayHealthProgress");
        }
    }

    IEnumerator DelayHealthProgress()
    {
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(1);
        ;
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
        // Player will find the game object position and assign the position to the player position
        player.transform.position = GameObject.Find("Spawn").transform.position;
    }

    public void Die()
    {
        if (!health1.gameObject.activeInHierarchy)
        {
            Destroy(player.gameObject);
            loseMessage.gameObject.SetActive(true);
        }
    }
}
