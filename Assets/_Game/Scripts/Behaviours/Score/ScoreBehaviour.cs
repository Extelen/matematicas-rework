using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBehaviour : Singleton<ScoreBehaviour>
{
    public int score = 0;
    public event Action<int> OnScoreChange;

    public void AddScore(int value)
    {
        score += value;
        OnScoreChange?.Invoke(score);
    }

    public void ResetScore()
    {
        score = 0;
        OnScoreChange?.Invoke(score);
    }

    public void SetScore(int value)
    {
        score = value;
        OnScoreChange?.Invoke(score);
    }

    public int GetScore()
    {
        return score;
    }
}
