using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnScreenScore : MonoBehaviour
{
    private TextMeshProUGUI m_renderer;
    private RectTransform rectTransform;

    [HideInInspector] public Pool<ScoreEffect> scoreEffectPool;
    public float scoreEffectDuration = 1f;
    private Vector3 effectOffset = new Vector3(30, 0, 0);

    private int displayedScore = 0;
    private int currentCombo = 0;
    private Coroutine updateCoroutine;

    private void Awake()
    {
        m_renderer = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        m_renderer.text = displayedScore.ToString();

        ScoreBehaviour.Instance.OnScoreChange += OnScoreUpdate;
        ScoreBehaviour.Instance.OnComboChange += OnComboUpdate;

        scoreEffectPool.Create();
        
    }
    private void OnDestroy()
    {
        ScoreBehaviour.Instance.OnScoreChange -= OnScoreUpdate;
        ScoreBehaviour.Instance.OnComboChange -= OnComboUpdate;
    }

    private void OnScoreUpdate(int targetScore)
    {

        if (updateCoroutine != null)
            StopCoroutine(updateCoroutine);

        int scoreDifference = targetScore - displayedScore;
        updateCoroutine = StartCoroutine(AnimateScore(targetScore));
        var instance = scoreEffectPool.Get();
        ScoreEffect.ComboIntensity intensity = ScoreEffect.ComboIntensity.Low;
        if (currentCombo >= 1)
        {
            intensity = ScoreEffect.ComboIntensity.Medium;
        }
        else if (currentCombo >= 2)
        {
            intensity = ScoreEffect.ComboIntensity.High;
        }
        
        float width = rectTransform.rect.width;
        instance.PlayEffect(scoreDifference.ToString(), transform.position + effectOffset + new Vector3(width, 0, 0), intensity, scoreEffectDuration, 25f);
    }

    private void OnComboUpdate(int combo)
    {
        currentCombo = combo;
    }

    private IEnumerator AnimateScore(int targetScore)
    {
        float duration = 0.5f; // Duration in seconds
        float elapsed = 0f;
        int initialScore = displayedScore;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            
            float t = elapsed / duration;
            
            displayedScore = (int)Mathf.Lerp(initialScore, targetScore, t);
            m_renderer.text = displayedScore.ToString();
            yield return null;
        }

        displayedScore = targetScore;
        m_renderer.text = displayedScore.ToString();
    }
}
