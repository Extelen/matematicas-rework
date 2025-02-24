using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicOperatorGameplay : CommonMissingOperatorsGameplay
{
    // Note: This also includes Addition and substraction operations as a player at this level
    // should be able to handle it. The contrary is not necessarily true.

    // TO DO: Include the multiple answer edge cases.

    // Variables
    [Header("Settings")]
    [SerializeField]
    private MinMaxInt m_numberRange = new MinMaxInt(2, 10); // Start from 2 to avoid issues with multiple answers with 1
    protected int m_resultNumber = 0;

    public void Awake()
    {
        m_leftNumber = m_numberRange.Random();
        m_rightNumber = m_numberRange.Random();

        int random_operation = Random.Range(0, 4);
        // The reason I decide the operation first and then the numbers, is because there's 1 case where the numbers matter
        switch (random_operation)
        {
            case 0: // Multiplication
                m_resultNumber = m_leftNumber * m_rightNumber;

                // Avoid edge case 2 * 2 which makes 2 answers possible, since 2 * 2 = 2 + 2
                while (m_leftNumber == 2 && m_rightNumber == 2)
                {
                    m_leftNumber = m_numberRange.Random();
                    m_rightNumber = m_numberRange.Random();
                }

                m_correctSymbol = "x";
                break;
            case 1: // Division
                m_resultNumber = m_numberRange.Random();

                // Avoid edge case 4 / 2 which makes 2 answers possible, since 4 / 2 = 4 - 2
                while (m_leftNumber == 4 && m_resultNumber == 2)
                {
                    m_leftNumber = m_numberRange.Random();
                    m_resultNumber = m_numberRange.Random();
                }

                m_leftNumber = m_resultNumber * m_rightNumber;


                m_correctSymbol = "÷";
                break;
            case 2: // Addition
                m_resultNumber = m_leftNumber + m_rightNumber;

                // Avoid edge case 2 + 2 which makes 2 answers possible, since 2 * 2 = 2 + 2
                while (m_leftNumber == 2 && m_rightNumber == 2)
                {
                    m_leftNumber = m_numberRange.Random();
                    m_rightNumber = m_numberRange.Random();
                }

                m_correctSymbol = "+";
                break;
            case 3: // Subtraction
                m_resultNumber = m_leftNumber - m_rightNumber;

                // Avoid edge case 4 - 2 which makes 2 answers possible, since 4 / 2 = 4 - 2
                while (m_leftNumber == 4 && m_rightNumber == 2)
                {
                    m_leftNumber = m_numberRange.Random();
                    m_rightNumber = m_numberRange.Random();
                }

                m_correctSymbol = "-";
                break;
        }

        m_leftNumberRenderer.text = m_leftNumber.ToString();
        m_rightNumberRenderer.text = m_rightNumber.ToString();
        m_resultNumberRenderer.text = m_resultNumber.ToString();

        m_symbolRenderer.text = "?";

        base.StartGame();
    }
/*
    public override void CheckAnswer()
    {
        var _symbol = m_currentSymbol
        //if (m_leftNumber)

        if (m_currentSymbol == m_correctSymbol)
        {
            Correct();
        }
        else
        {
            Wrong();
        }
    }*/

    public int GetOperationResult(int leftNum, string operatorSymbol, int rightNum)
    {
        switch (operatorSymbol)
        {
            case "+": return leftNum + rightNum;
            case "-": return leftNum - rightNum;
            case "x": return leftNum * rightNum;
            case "÷": return leftNum / rightNum;
            default: throw new System.InvalidOperationException("Invalid operator: " + operatorSymbol);
        }
    }
}
