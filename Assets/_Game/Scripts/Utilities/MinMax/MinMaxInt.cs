using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Parameters
[System.Serializable]
public struct MinMaxInt
{
    //Variables
    [SerializeField] public int min;
    [SerializeField] public int max;

    //Constructors
    public MinMaxInt(int min, int max)
    {
        this.min = min;
        this.max = max;
    }

    //Methods
    /// <summary>
    /// Get a random number between (inclusive) min, and (exclusive) max.
    /// </summary>
    /// <returns> The random number </returns>
    public int Random()
    {
        return UnityEngine.Random.Range(min, max);
    }

    public int RandomInclusive()
    {
        return UnityEngine.Random.Range(min, max + 1);
    }
}
