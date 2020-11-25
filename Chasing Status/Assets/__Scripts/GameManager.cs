using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Variable Declaration
    public static bool gameOver;
    public GameObject gameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        // Set Gameover screen to false on start
        gameOver = false;
        // Sets the world time = 1, used to make sure a reply does spawn a frozen world
        Time.timeScale = 1;
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
    }
}
