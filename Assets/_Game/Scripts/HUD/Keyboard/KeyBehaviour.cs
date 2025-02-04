using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class KeyBehaviour : MonoBehaviour
{
    // Variables
    [Header("Animation")]
    [SerializeField]
    private float m_appearDelay = 0.1f;

    [SerializeField]
    private float m_animationTime = 0.1f;

    [Space]
    [SerializeField]
    private Easing.Type m_scaleEasing = Easing.Type.OutElastic;

    [SerializeField]
    private float m_disabledScale = 0.6f;

    [Header("References")]
    [SerializeField]
    private CanvasGroup m_canvasGroup;

    // Methods
    protected void Initialize(int index)
    {
        StopAllCoroutines();
        StartCoroutine(DoAppear(index));
    }

    public abstract void OnKeyPress();

    // Coroutines
    private IEnumerator DoAppear(float index)
    {
        m_canvasGroup.alpha = 0;
        m_canvasGroup.interactable = false;

        transform.localScale = Vector3.one * m_disabledScale;

        yield return new WaitForSeconds(index * m_appearDelay);

        for (float i = 0; i < m_animationTime; i += Time.deltaTime)
        {
            m_canvasGroup.alpha = i / m_animationTime;

            var t = Easing.Evaluate(i / m_animationTime, m_scaleEasing);
            transform.localScale = Vector3.one * Mathf.LerpUnclamped(m_disabledScale, 1, t);

            yield return null;
        }

        m_canvasGroup.alpha = 1;
        transform.localScale = Vector3.one;
        m_canvasGroup.interactable = true;
    }

}
