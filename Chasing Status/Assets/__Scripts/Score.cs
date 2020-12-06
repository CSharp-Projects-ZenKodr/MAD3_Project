using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI score;

    public float scoreCount;
    public float pointsPerSecond;

    private void Update()
    {
        scoreCount += pointsPerSecond * Time.deltaTime;
        score.text = "Score: " + Mathf.RoundToInt(scoreCount);
    }
}
