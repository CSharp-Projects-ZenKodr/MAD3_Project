using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI[] scores;

    void Start()
    {
        ScoreSetUp(scores);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void ScoreSetUp(TextMeshProUGUI[] scores)
    {
        for (int i = 1; i < 6; i++)
        {
            if (!PlayerPrefs.HasKey("score" + i))
            {
                PlayerPrefs.SetInt("score" + i, 100 + i);
            }
            scores[i - 1].SetText("Score - " + PlayerPrefs.GetInt("score" + i));
            Debug.Log("Score-" + i + "-Setup");
        }
    }

}
