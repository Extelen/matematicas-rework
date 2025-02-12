using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBehaviour : Singleton<ScoreBehaviour>
{
    public ScoreboardBehaviour scoreboard;

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

    private void OnEnable() => GameManager.OnStateSwitch += OnStateSwitch;
    private void OnDisable() => GameManager.OnStateSwitch -= OnStateSwitch;

    private void OnStateSwitch(GameState state)
    {
        if (state == GameState.GameOver)
        {
            scoreboard.LoadScoreboardData();
            if (scoreboard.IsScoreQualifying(score))
            {
                scoreboard.submitScoreButton.SetActive(true);
                scoreboard.AddScoreEntry("DEV", score, true);
                scoreboard.UpdateScoreboard();
            }
            else
            {
                scoreboard.submitScoreButton.SetActive(false);
                scoreboard.UpdateScoreboard();
            }
        }
        
    }
}
