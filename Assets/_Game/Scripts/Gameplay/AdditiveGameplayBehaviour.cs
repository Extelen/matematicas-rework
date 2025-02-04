using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AdditiveGameplayBehaviour : GameplayBehaviour
{
    // Variables
    [Header("Settings")]
    [SerializeField]
    private MinMaxInt m_additionRange = new MinMaxInt(0, 10);

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

    private int m_currentNumber;
    private int m_correctNumber;

    // Methods
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        m_currentNumber = -1;

        int left = m_additionRange.Random();
        m_leftNumberRenderer.text = left.ToString();

        int right = m_additionRange.Random();
        m_rightNumberRenderer.text = right.ToString();

        m_correctNumber = left + right;
        m_answerRenderer.text = "X";
    }

    public override void StartGame()
    {
        // Enable keyboard?
    }

    public void OnStringChange(string str)
    {
        if (string.IsNullOrEmpty(str))
            return;

        m_currentNumber = int.Parse(str);
        m_answerRenderer.text = str;
    }

    private void ResetAnswer()
    {
        m_keyboardBehaviour.Clear();
        m_answerRenderer.text = "X";
    }

    public void Check()
    {
        bool correct = m_currentNumber == m_correctNumber;

        if (correct)
            ModuleBehaviour.End();

        else
            ResetAnswer();
    }
}
