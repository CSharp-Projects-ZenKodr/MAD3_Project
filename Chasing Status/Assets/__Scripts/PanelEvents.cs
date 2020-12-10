using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelEvents : MonoBehaviour
{
    public void ReplayGame()
    {
        SceneManager.LoadScene("Base_Level");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
