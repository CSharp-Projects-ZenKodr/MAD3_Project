using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public float timeStart = 60.0f;
    [SerializeField]
    public TextMeshProUGUI textBox;

    // Start is called before the first frame update
    void Start()
    {
        textBox.text = timeStart.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        timeStart -= Time.deltaTime;
        textBox.text = Mathf.Round(timeStart).ToString();
    }
}
