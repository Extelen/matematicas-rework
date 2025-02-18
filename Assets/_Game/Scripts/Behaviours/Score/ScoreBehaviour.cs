using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBehaviour : Singleton<ScoreBehaviour>
{
    public ScoreboardBehaviour scoreboard;

    private int score = 0;
    private int combo = 0; // Modules completed without making mistakes  TO DO: Show in UI
    private int comboScoreAddition = 10; // Score added for each combo
    private int maxComboMultiplier = 2;

    public event Action<int> OnScoreChange;
    public event Action<int> OnComboChange;


    public void AddScore(int value)
    {
        int comboMultiplier = Mathf.Min(combo, maxComboMultiplier);
        UpdateScore(score + value + (comboScoreAddition * comboMultiplier));
    }

    public void ResetScore() => UpdateScore(0);
    public void SetScore(int value) => UpdateScore(value);
    public int GetScore() => score;

    private void UpdateScore(int value)
    {
        score = value;
        OnScoreChange?.Invoke(score);
    }

    public void AddCombo() => UpdateCombo(combo + 1);
    public void ResetCombo() => UpdateCombo(0);
    public int GetCombo() => combo;

    private void UpdateCombo(int value)
    {
        combo = value;
        OnComboChange?.Invoke(combo);
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
