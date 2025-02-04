using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IconKeyBehaviour : KeyBehaviour
{
    // Variables
    [Header("References")]
    [SerializeField]
    private Image m_iconRenderer;

    private event System.Action OnPress;

    // Methods
    /// <summary>
    /// Initialize the key and set the character and the on key press event.
    /// </summary>
    public void Initialize(Sprite icon, int index, System.Action onPress)
    {
        base.Initialize(index);

        OnPress = onPress;
        m_iconRenderer.sprite = icon;
    }

    public override void OnKeyPress()
    {
        OnPress?.Invoke();
    }
}
