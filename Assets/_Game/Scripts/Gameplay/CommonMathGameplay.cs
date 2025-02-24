using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class CommonMathGameplay : GameplayBehaviour
{
    // Variables
    [Header("References")]
    [SerializeField]
    private TMP_Text m_leftNumberRenderer;
    [SerializeField]
    private TMP_Text m_rightNumberRenderer;
    [SerializeField]
    private TMP_Text m_answerRenderer;

    [Space]
    [SerializeField]
    private KeyboardBehaviour m_keyboardBehaviour;

    protected int LeftNumber { get; set; }
    protected int RightNumber { get; set; }

    private int m_currentNumber;
    private int m_correctNumber;

    // Methods
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        m_currentNumber = -1;

        LeftNumber = GetLeftNumber();
        RightNumber = GetRightNumber();
        m_correctNumber = GetCorrectNumber();

        m_leftNumberRenderer.text = LeftNumber.ToString();
        m_rightNumberRenderer.text = RightNumber.ToString();
        m_answerRenderer.text = "?";
    }

    public override void StartGame()
    {
        // Enable keyboard?
    }

    public abstract int GetLeftNumber();
    public abstract int GetRightNumber();
    public abstract int GetCorrectNumber();

    public void OnStringChange(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            m_currentNumber = -1;
            m_answerRenderer.text = "?";
            return;
        }

        m_currentNumber = int.Parse(str);
        m_answerRenderer.text = str;
    }

    public void ResetAnswer()
    {
        m_keyboardBehaviour.Clear();
        m_answerRenderer.text = "?";
    }

    public override void CheckAnswer()
    {
        if (m_currentNumber == m_correctNumber)
        {
            Correct();
        }
        else
        {
            Wrong();
        }
    }

    public override void Correct()
    {
        ScoreBehaviour.Instance.AddScore(DifficultyScore.GetScore(ModuleBehaviour.DifficultyRating));
        ScoreBehaviour.Instance.AddCombo();
        ModuleBehaviour.End();
    }

    public override void Wrong()
    {
        ScoreBehaviour.Instance.ResetCombo();
        ResetAnswer();
    }

    

}
