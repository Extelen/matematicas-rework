using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlusMinusOperatorGameplay : CommonMissingOperatorsGameplay
{
    // Variables
    [Header("Settings")]
    [SerializeField]
    private MinMaxInt m_numberRange = new MinMaxInt(1, 10);
    protected int m_resultNumber = 0;

    public void Awake()
    {
        int num1 = m_numberRange.Random();
        int num2 = m_numberRange.Random();

        if (Random.value < 0.5f)
        {
            // Plus
            m_leftNumber = num1;
            m_rightNumber = num2;

            m_resultNumber = m_leftNumber + m_rightNumber;
            m_correctSymbol = "+";
        }
        else
        {
            // Minus
            // This way the result is always positive, as kids that only know of these don't know negative numbers
            m_leftNumber = Mathf.Max(num1, num2);
            m_rightNumber = Mathf.Min(num1, num2);

            m_resultNumber = m_leftNumber - m_rightNumber;
            m_correctSymbol = "-";
        }

        m_leftNumberRenderer.text = m_leftNumber.ToString();
        m_rightNumberRenderer.text = m_rightNumber.ToString();
        m_resultNumberRenderer.text = m_resultNumber.ToString();

        m_symbolRenderer.text = "?";

        base.StartGame();
    }
}
