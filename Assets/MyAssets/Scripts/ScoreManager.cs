using UnityEngine;
using TMPro;

public class ScoringManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private ScoringBlock[] allBlocks;

    private void Update()
    {
        allBlocks = FindObjectsOfType<ScoringBlock>();

        float totalScore = 0f;

        foreach (var block in allBlocks)
        {
            totalScore += block.GetScore();
        }

        scoreText.text = "Punkte: " + totalScore.ToString("F1"); // eine Nachkommastelle
    }
}
