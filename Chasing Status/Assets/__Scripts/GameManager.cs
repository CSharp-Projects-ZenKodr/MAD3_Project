using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    HighscoreManager hm;

    // Variable Declaration
    public static bool gameOver;
    public GameObject gameOverPanel;
    public GameObject pauseMenu;

    public static bool isGameRunning;
    [SerializeField]
    public GameObject startText;

    // Player Score information
    public TextMeshProUGUI score;
    float scoreCount = 100;
    float pointsPerSecond = 50;

    public TextMeshProUGUI playerName;

    public int playerCoins;

    private void Awake()
    {
        playerCoins = PlayerPrefs.GetInt("coins");
    }

    // Start is called before the first frame update
    void Start()
    {
        // Gets singleton instance of Highscore Manager
        hm = HighscoreManager.Instance;
        // Set Gameover screen to false on start
        gameOver = false;
        // Sets the world time = 1, used to make sure a reply does spawn a frozen world
        Time.timeScale = 1;
        // Set to false so game doesn't run on start
        isGameRunning = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        // Checks if gameOver is true
        if (gameOver)
        {
            // Worlds time = 0, Stops the world moving
            Time.timeScale = 0;
            // Sets GameObject active (visible)
            gameOverPanel.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            isGameRunning = true;
            Destroy(startText);
        }

        // If the player is not moving then dont start updating the score
        if (player.transform.position.z > 0f)
        {
            // Increments the score and prints it to the screen rounding to a whole number
            scoreCount += pointsPerSecond * Time.deltaTime;
            score.text = "Score: " + Mathf.RoundToInt(scoreCount);
        }

        pauseGame();
    }

    public void pauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    private void addScore(int score, string name)
    {
        hm.AddHighscoreEntry(score, name);
    }

    public void SubmitScore()
    {
        string name = playerName.text;
        int playerScore = Mathf.RoundToInt(scoreCount);

        Debug.Log(name + " - " + playerScore);
        hm.AddHighscoreEntry(playerScore, name);
    }
}
