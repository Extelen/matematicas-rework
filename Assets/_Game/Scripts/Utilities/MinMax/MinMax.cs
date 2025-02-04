using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Parameters
[System.Serializable]
public struct MinMax
{
    //Variables
    [SerializeField] public float min;
    [SerializeField] public float max;

    //Constructors
    public MinMax(float min, float max)
    {
        this.min = min;
        this.max = max;
    }

    //Methods
    /// <summary>
    /// Get a random number between (inclusive) min, and (inclusive) max.
    /// </summary>
    /// <returns> The random number </returns>
    public float Random()
    {
        return UnityEngine.Random.Range(min, max);
    }

    public float Lerp(float t)
    {
        return Mathf.Lerp(min, max, t);
    }

    public float Clamp(float value)
    {
        return Mathf.Clamp(value, min, max);
    }
}
