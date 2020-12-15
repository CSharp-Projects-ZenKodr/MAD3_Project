using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    // References to PlayerController and Road_manager scripts
    PlayerController pc;
    Road_Manager rm;

    public float timeStart = 60.0f;
    [SerializeField]
    public TextMeshProUGUI textBox;

    // Start is called before the first frame update
    void Start()
    {
        rm = Road_Manager.Instance;
        textBox.text = timeStart.ToString();
        pc = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        ReachedCheckpoint();
        FailedToReachCheckpoint();
        if (GameManager.isGameRunning)
        {
            textBox.text = Mathf.Round(timeStart).ToString();
            timeStart -= Time.deltaTime;
        } 
    }

    /**
     * Fucntion checks if the player has past a checkpoint and then
     * checks the checkpointCounter and decides the timer reset value.
     */
    void ReachedCheckpoint()
    {       
        if (rm.IsCheckpoint() == true)
        {
            switch (rm.checkpointCounter)
            {
                default:
                    timeStart = 15.0f;
                    break;
                case 1:
                    timeStart = 35.0f;
                    break;
                case 2:
                    timeStart = 30.0f;
                    break;
                case 3:
                    timeStart = 25.0f;
                    break;
                case 4:
                    timeStart = 20.0f;
                    break;
                case 5:
                    timeStart = 19.0f;
                    break;
                case 6:
                    timeStart = 18.0f;
                    break;
                case 7:
                    timeStart = 17.0f;
                    break;
                case 8:
                    timeStart = 16.0f;
                    break;
            }
        }
    }

    /**
     * If the player fails to reach the check point before the timer runs out
     * the GameManager is called and gameover is set to truw
     */
    bool FailedToReachCheckpoint()
    {
        if (timeStart < 0.0f)
        {
            GameManager.gameOver = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}
