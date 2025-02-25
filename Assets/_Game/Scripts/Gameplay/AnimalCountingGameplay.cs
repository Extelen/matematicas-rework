using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimalCountingGameplay : GameplayBehaviour
{

    [SerializeField] private TMP_Text m_answerRenderer;
    [SerializeField] private KeyboardBehaviour m_keyboardBehaviour;
    [Space]
    [SerializeField] private GameObject m_animalPrefab;
    [SerializeField] private Transform m_animalSpawnpoint;
    [Header("Settings")]
    [SerializeField] private MinMaxInt m_spawnAmountRange = new MinMaxInt(1, 10);

    private int m_animalsToSpawn = 0;
    private int m_currentAnswer = -1;

    public override void CheckAnswer()
    {
        //throw new System.NotImplementedException();
        if (m_currentAnswer == m_animalsToSpawn)
        {
            Correct();
        }
        else
        {
            Wrong();
        }
    }

    public override void StartGame()
    {
        
    }

    void Awake()
    {
        m_animalsToSpawn = m_spawnAmountRange.RandomInclusive();

        for (int i = 0; i < m_animalsToSpawn; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-2f,2f), Random.Range(-2f,2f), 0);
            Instantiate(m_animalPrefab, m_animalSpawnpoint.position + randomOffset, Quaternion.identity);
        }
    }

    public void OnStringChange(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            m_currentAnswer = -1;
            m_answerRenderer.text = "?";
            return;
        }

        m_currentAnswer = int.Parse(str);
        m_answerRenderer.text = str;
    }

    public override void Correct()
    {
        ScoreBehaviour.Instance.AddScore(DifficultyScore.GetScore(ModuleBehaviour.DifficultyRating));
        ScoreBehaviour.Instance.AddCombo();
        ModuleBehaviour.End();
    }

    public override void Wrong()
    {
        ScoreBehaviour.Instance.ResetCombo();
        ResetAnswer();
    }

    public void ResetAnswer()
    {
        m_keyboardBehaviour.Clear();
        m_answerRenderer.text = "?";
        m_currentAnswer = -1;
    }
}
