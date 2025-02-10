using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreEntryUI : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;

    public void SetScoreEntry(ScoreboardBehaviour.ScoreEntry entry)
    {
        rankText.text = entry.rank.ToString();
        nameText.text = entry.name;
        scoreText.text = entry.score.ToString();
    }
}
