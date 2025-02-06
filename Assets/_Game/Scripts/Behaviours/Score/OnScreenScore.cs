using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnScreenScore : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI m_renderer;

    private void Start()
    {
        ScoreBehaviour.Instance.OnScoreChange += OnScoreUpdate;
    }

    private void OnScoreUpdate(int score)
    {
        m_renderer.text = score.ToString();
    }

}
