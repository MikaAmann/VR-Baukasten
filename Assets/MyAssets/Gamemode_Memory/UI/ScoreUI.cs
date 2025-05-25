using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI  scoreText;

    public void UpdateScoreUI(float score)
    {
        int percentage = Mathf.RoundToInt(score * 100f);
        scoreText.text = percentage + "%";
    }
}
