using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterKeyBehaviour : KeyBehaviour
{
    // Variables
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI m_textRenderer;

    private event System.Action<char> OnPress;

    public char Character
    {
        get;
        private set;
    }

    // Methods
    /// <summary>
    /// Initialize the key and set the character and the on key press event.
    /// </summary>
    public void Initialize(char character, int index, System.Action<char> onPress)
    {
        base.Initialize(index);

        OnPress = onPress;
        m_textRenderer.text = character.ToString();

        Character = character;

    }

    public override void OnKeyPress()
    {
        OnPress?.Invoke(Character);
    }
}
