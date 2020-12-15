using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelEvents : MonoBehaviour
{
    // Reloads the Base_Level scene
    public void ReplayGame()
    {
        SceneManager.LoadScene("Base_Level");
    }

    // Loads the Main_Menu scene
    public void QuitGame()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
