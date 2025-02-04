using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModuleBehaviour : MonoBehaviour
{
    // Singleton
    public static ModuleBehaviour Current { get; private set; }

    // Variables
    [Header("Gameplay")]
    [SerializeField]
    private GameplayBehaviour m_moduleElement;

    // Methods
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        Current = this;
    }

    public void Play()
    {
        StartElement();
    }

    private void StartElement()
    {
        if (m_moduleElement)
            m_moduleElement.Execute(this);

        else
            End();
    }

    public void End()
    {
        LevelManager.Instance.NextModule();
    }
}
