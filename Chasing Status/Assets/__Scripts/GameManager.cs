using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variable Declaration
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameRunning;
    [SerializeField]
    public GameObject startText;

    // Start is called before the first frame update
    void Start()
    {
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
    }
}
