using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameplayBehaviour : MonoBehaviour
{
    // Variables
    protected ModuleBehaviour ModuleBehaviour { get; private set; }

    // Methods
    /// <summary>
    /// Function that the module behaviour will execute indicating that it is 
    /// the sequence needs to start.
    /// </summary>
    public void Execute(ModuleBehaviour manager)
    {
        ModuleBehaviour = manager;
        StartGame();
    }

    /// <summary>
    /// Called at the start of the module.
    /// </summary>
    public abstract void StartGame();

    /// <summary>
    /// Called when the player presses the "Confirmar" button.
    /// It is recommended to call the "Correct()" or "Wrong()" methods here.
    /// Example:
    /// <code>
    /// if (m_currentNumber == m_correctNumber)
    /// {
    ///     Correct();
    /// }
    /// else
    /// {
    ///     Wrong();
    /// }
    /// </code>
    /// </summary>
    public abstract void CheckAnswer();

    /// <summary>
    /// Behaviour when the player answers correctly.
    /// It is recommended to add score and combo here.
    /// Example:
    /// <code>
    /// ScoreBehaviour.Instance.AddScore(DifficultyScore.GetScore(ModuleBehaviour.DifficultyRating));
    /// ScoreBehaviour.Instance.AddCombo();
    /// ModuleBehaviour.End();
    /// </code>
    /// </summary>
    public abstract void Correct();

    /// <summary>
    /// Template method for behaviour when the player answers incorrectly.
    /// Example:
    /// <code>
    /// ScoreBehaviour.Instance.ResetCombo();
    /// ResetAnswer();
    /// </code>
    /// </summary>
    public abstract void Wrong();
}