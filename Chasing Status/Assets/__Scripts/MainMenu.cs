using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Loads the Base_Level scene
    public void PlayGame()
    {
        SceneManager.LoadScene("Base_Level");
    }

    // Quits the game application when built 
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
