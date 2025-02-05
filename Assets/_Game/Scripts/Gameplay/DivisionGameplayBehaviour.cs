using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivisionGameplayBehaviour : CommonMathGameplay
{
    // Variables
    [Header("Settings")]
    [SerializeField]
    private MinMaxInt m_divisionRange = new MinMaxInt(1, 10); // Min or Max should never be 0

    private int m_leftNumber;
    private int m_rightNumber;

    // Methods
    private void GenerateNumbers() 
    {
        m_rightNumber = m_divisionRange.Random();
        m_leftNumber = m_rightNumber * m_divisionRange.Random(); // This way the correct number is always a whole number
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
        return LeftNumber / RightNumber;
    }
}
