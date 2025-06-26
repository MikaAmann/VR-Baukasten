using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class ScoreHistoryManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TextMeshProUGUI scoreHistoryText;

    private List<float> scores = new List<float>();

    public void SaveCurrentScore()
    {
        float currentScore = scoreManager.GetCurrentScore();
        scores.Add(currentScore);

        // Sortieren: Höchste zuerst
        scores = scores.OrderByDescending(s => s).ToList();

        // Maximal 5 Scores behalten
        if (scores.Count > 5)
            scores = scores.Take(5).ToList();

        UpdateHistoryText();
    }

    private void UpdateHistoryText()
    {
        scoreHistoryText.text = "";

        for (int i = 0; i < scores.Count; i++)
        {
            scoreHistoryText.text += $"{i + 1}. {scores[i]:F1} Punkte\n";
        }
    }
}

