using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreEntry
{
    public int rank;
    public string name;
    public int score;
    public bool isInputEntry; // For the player to input their name
}

[System.Serializable]
public class ScoreEntryData
{
    public string name;
    public int score;
}