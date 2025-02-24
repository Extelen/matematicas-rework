using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicOperatorGameplay : CommonMissingOperatorsGameplay
{
    // Note: This also includes Addition and substraction operations as a player at this level
    // should be able to handle it. The contrary is not necessarily true.

    // Variables
    [Header("Settings")]
    [SerializeField]
    private MinMaxInt m_numberRange = new MinMaxInt(1, 10);
    protected int m_resultNumber = 0;

    public void Awake()
    {
        m_leftNumber = m_numberRange.RandomInclusive();
        m_rightNumber = m_numberRange.RandomInclusive();

        int random_operation = Random.Range(0, 4);
        switch (random_operation)
        {
            case 0: // Multiplication
                m_resultNumber = m_leftNumber * m_rightNumber;
                break;
            case 1: // Division
                m_resultNumber = m_numberRange.RandomInclusive();
                m_leftNumber = m_resultNumber * m_rightNumber;
                break;
            case 2: // Addition
                m_resultNumber = m_leftNumber + m_rightNumber;
                break;
            case 3: // Subtraction
                m_resultNumber = m_leftNumber - m_rightNumber;
                break;
        }

        m_leftNumberRenderer.text = m_leftNumber.ToString();
        m_rightNumberRenderer.text = m_rightNumber.ToString();
        m_resultNumberRenderer.text = m_resultNumber.ToString();

        m_symbolRenderer.text = "?";

        base.StartGame();
    }

    public override void CheckAnswer()
    {
        var _operation_result = GetOperationResult(m_leftNumber,m_currentSymbol, m_rightNumber);
        if (m_resultNumber == _operation_result)
        {
            Correct();
        }
        else
        {
            Wrong();
        }
    }
    public int GetOperationResult(int leftNum, string operatorSymbol, int rightNum)
    {
        switch (operatorSymbol)
        {
            case "+": return leftNum + rightNum;
            case "-": return leftNum - rightNum;
            case "x": return leftNum * rightNum;
            case "÷": return leftNum / rightNum;
            default: return 9999999;
        }
    }
}
