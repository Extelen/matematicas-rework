using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DifficultyRating
{
    Easy,
    Medium,
    Hard
}

public static class DifficultyScore
{
    /// <summary>
    /// Get the score based on the difficulty rating
    /// </summary>
    /// <param name="difficulty">The difficulty rating for which the score is to be calculated</param>
    /// <returns>The score corresponding to the given difficulty rating</returns>
    public static int GetScore(DifficultyRating difficulty)
    {
        switch (difficulty)
        {
            case DifficultyRating.Easy:
                return 50;
            case DifficultyRating.Medium:
                return 100;
            case DifficultyRating.Hard:
                return 150;
            default:
                return 0;
        }
    }
}