using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public BuildZoneTrigger buildZone;
    public bool UpdateScore = true;

    private float totalScore = 0f;

    private void Update()
    {
        totalScore = 0f;

        foreach (var block in buildZone.GetBlocksInZone())
        {
            totalScore += block.GetScore();
        }
        if (UpdateScore)
        {
            scoreText.text = totalScore.ToString("F1");
        }
    }

    public float GetCurrentScore()
    {
        scoreText.text = totalScore.ToString("F1");
        return totalScore;
    }
}
