using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtractiveGameplayBehaviour : CommonMathGameplay
{
    // Variables
    [Header("Settings")]
    [SerializeField]
    private MinMaxInt m_subtractionRange = new MinMaxInt(0, 10);

    private int m_leftNumber;
    private int m_rightNumber;

    // Methods
    private void GenerateNumbers() 
    {
        int num1 = m_subtractionRange.Random();
        int num2 = m_subtractionRange.Random();

        // This way the correct number is always positive
        m_leftNumber = Mathf.Max(num1, num2);
        m_rightNumber = Mathf.Min(num1, num2);
    }

    public override int GetLeftNumber()
    {
        GenerateNumbers(); // Left gets called first
        return m_leftNumber;
    }

    public override int GetRightNumber()
    {
        return m_rightNumber;
    }

    public override int GetCorrectNumber()
    {
        return m_leftNumber - m_rightNumber;
    }
}
