using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditiveGameplayBehaviour : CommonMathGameplay
{
    // Variables
    [Header("Settings")]
    [SerializeField]
    private MinMaxInt m_additionRange = new MinMaxInt(0, 10);

    // Methods
    public override int GetLeftNumber()
    {
        return m_additionRange.Random();
    }

    public override int GetRightNumber()
    {
        return m_additionRange.Random();
    }

    public override int GetCorrectNumber()
    {
        return LeftNumber + RightNumber;
    }
}
