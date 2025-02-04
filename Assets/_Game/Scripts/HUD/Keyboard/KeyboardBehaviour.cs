using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyboardBehaviour : MonoBehaviour
{
    // Variables
    [Header("Settings")]
    [SerializeField]
    private string m_characters = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ_$";

    [SerializeField]
    [Tooltip("The max length of the string to return. -1 is no limit.")]
    private int m_maxLength = -1;

    public int MaxLength
    {
        get
        {
            return m_maxLength;
        }

        set
        {
            m_maxLength = value;
        }
    }

    [Header("References")]
    [SerializeField]
    private Transform m_keyLayout;

    [SerializeField]
    private CharacterKeyBehaviour m_characterKeyPrefab;

    [SerializeField]
    private IconKeyBehaviour m_iconKeyPrefab;

    [Header("Icons")]
    [SerializeField]
    private Sprite m_deleteIcon;

    [Header("Events")]
    [SerializeField]
    private UnityEvent<string> m_onStringChange;

    private List<KeyBehaviour> m_keys;

    public string CurrentString
    {
        get;
        private set;
    }

    // Methods
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        m_keys = new List<KeyBehaviour>();

        for (int i = 0; i < m_keyLayout.childCount; i++)
            Destroy(m_keyLayout.GetChild(i).gameObject);
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        CurrentString = "";

        m_keys.ForEach(c => Destroy(c.gameObject));
        m_keys.Clear();

        int index;

        // Character Keys
        for (index = 0; index < m_characters.Length; index++)
        {
            char character = m_characters[index];

            if (character == '$')
            {
                var deleteKey = Instantiate(m_iconKeyPrefab, m_keyLayout);
                deleteKey.Initialize(m_deleteIcon, index, RemoveLast);
                m_keys.Add(deleteKey);
                continue;
            }

            var key = Instantiate(m_characterKeyPrefab, m_keyLayout);
            key.Initialize(character, index, OnKeyPress);

            m_keys.Add(key);
        }
    }

    private void OnKeyPress(char character)
    {
        if (character == '_')
            character = ' ';

        Add(character);
    }

    public void Clear()
    {
        CurrentString = "";
        m_onStringChange?.Invoke(CurrentString);
    }

    public void Add(char character)
    {
        if (m_maxLength != -1 && CurrentString.Length >= m_maxLength) return;

        if (CurrentString == "" && character == ' ')
            return;

        CurrentString += character;
        m_onStringChange?.Invoke(CurrentString);
    }

    public void RemoveLast()
    {
        if (string.IsNullOrEmpty(CurrentString))
            return;

        CurrentString = CurrentString.Remove(CurrentString.Length - 1);
        m_onStringChange?.Invoke(CurrentString);
    }

    public void Set(string current)
    {
        if (m_maxLength != -1) CurrentString = current.Substring(0, m_maxLength);
        else CurrentString = current;

        m_onStringChange?.Invoke(CurrentString);
    }
}
