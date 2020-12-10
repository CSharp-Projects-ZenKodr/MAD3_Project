using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
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
        Debug.Log(rm.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        ReachedCheckpoint();
        FailedToReachCheckpoint();
        timeStart -= Time.deltaTime;
        textBox.text = Mathf.Round(timeStart).ToString();
    }

    void ReachedCheckpoint()
    {       
        if (rm.IsCheckpoint() == true)
        {
            timeStart = 25.0f;
        }
    }

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
