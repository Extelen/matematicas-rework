using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreEntryUI : MonoBehaviour
{

    public TextMeshProUGUI rankText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;

    public ScoreEntry scoreEntry;

    public virtual void SetScoreEntry(ScoreEntry newEntry)
    {
        rankText.text = newEntry.rank.ToString();
        nameText.text = newEntry.name;
        scoreText.text = newEntry.score.ToString();
        scoreEntry = newEntry;
    }

    public string GetName()
    {
        return scoreEntry.name;
    }

    public int GetRank()
    {
        return scoreEntry.rank;
    }

    public int GetScore()
    {
        return scoreEntry.score;
    }
}
