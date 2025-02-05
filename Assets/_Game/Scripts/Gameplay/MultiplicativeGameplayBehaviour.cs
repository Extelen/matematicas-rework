using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplicativeGameplayBehaviour : CommonMathGameplay
{
    // Variables
    [Header("Settings")]
    [SerializeField]
    private MinMaxInt m_multiplicationRange = new MinMaxInt(0, 10);

    // Methods
    public override int GetLeftNumber()
    {
        return m_multiplicationRange.Random();
    }

    public override int GetRightNumber()
    {
        return m_multiplicationRange.Random();
    }

    public override int GetCorrectNumber()
    {
        return LeftNumber * RightNumber;
    }
}
