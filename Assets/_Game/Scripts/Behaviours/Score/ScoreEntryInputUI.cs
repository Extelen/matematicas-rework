using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreEntryInputUI : ScoreEntryUI
{
    [SerializeField] TMP_InputField inputField;

    public override void SetScoreEntry(ScoreEntry newEntry)
    {
        rankText.text = newEntry.rank.ToString();
        scoreText.text = newEntry.score.ToString();
        scoreEntry = newEntry;
    }

    public string GetInputName()
    {
        return inputField.text;
    }
}
