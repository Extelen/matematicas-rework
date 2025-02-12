using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModuleBehaviour : MonoBehaviour
{
    // Singleton
    public static ModuleBehaviour Current { get; private set; }

    // Variables
    [Header("Gameplay")]
    [SerializeField]
    private GameplayBehaviour m_moduleElement;

    [SerializeField]
    private DifficultyRating difficultyRating = DifficultyRating.Easy;

    public DifficultyRating DifficultyRating => difficultyRating;

    // Methods
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        Current = this;
    }

    public void Play()
    {
        StartElement();
    }

    private void StartElement()
    {
        if (m_moduleElement)
            m_moduleElement.Execute(this);

        else
            End();
    }

    public void End()
    {
        ScoreBehaviour.Instance.AddScore(DifficultyScore.GetScore(difficultyRating));

        if (GameManager.CurrentState == GameState.GameOver)
            return;
        LevelManager.Instance.NextModule();
    }
}
