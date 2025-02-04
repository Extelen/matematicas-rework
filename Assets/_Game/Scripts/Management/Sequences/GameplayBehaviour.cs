using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameplayBehaviour : MonoBehaviour
{
    // Variables
    protected ModuleBehaviour ModuleBehaviour { get; private set; }

    // Methods
    /// <summary>
    /// Function that the module behaviour will execute indicating that it is 
    /// the sequence needs to start.
    /// </summary>
    public void Execute(ModuleBehaviour manager)
    {
        ModuleBehaviour = manager;
        StartGame();
    }

    public abstract void StartGame();
}