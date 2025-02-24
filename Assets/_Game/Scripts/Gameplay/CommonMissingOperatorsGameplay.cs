using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class CommonMissingOperatorsGameplay : GameplayBehaviour
{

    [Header("References")]
    [SerializeField]
    protected TMP_Text m_leftNumberRenderer;
    [SerializeField]
    protected TMP_Text m_symbolRenderer;
    [SerializeField]
    protected TMP_Text m_rightNumberRenderer;
    [SerializeField]
    protected TMP_Text m_resultNumberRenderer;

    [Space]
    [SerializeField]
    private KeyboardBehaviour m_keyboardBehaviour;

    protected int m_rightNumber;
    protected int m_leftNumber;
    protected string m_currentSymbol;
    protected string m_correctSymbol;

    public override void CheckAnswer()
    {
        if (m_currentSymbol == m_correctSymbol)
        {
            Correct();
        }
        else
        {
            Wrong();
        }
    }

    public override void StartGame()
    {
        /*
        m_currentSymbol = "n";

        LeftNumber = GetLeftNumber();
        RightNumber = GetRightNumber();
        m_correctSymbol = GetCorrectSymbol();

        m_leftNumberRenderer.text = LeftNumber.ToString();
        m_rightNumberRenderer.text = RightNumber.ToString();
        m_answerRenderer.text = "X";*/
    }

    public void OnStringChange(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            m_currentSymbol = "";
            m_symbolRenderer.text = "?";
            return;
        }

        m_currentSymbol = str;
        m_symbolRenderer.text = str;
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
    
    public void ResetAnswer()
    {
        m_keyboardBehaviour.Clear();
        m_symbolRenderer.text = "?";
    }
}
