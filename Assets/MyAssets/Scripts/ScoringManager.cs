using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public BuildZoneTrigger buildZone;

    private float totalScore = 0f;

    private void Update()
    {
        totalScore = 0f;

        foreach (var block in buildZone.GetBlocksInZone())
        {
            totalScore += block.GetScore();
        }

        scoreText.text = totalScore.ToString("F1");
    }

    public float GetCurrentScore()
    {
        return totalScore;
    }
}
