using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(Animator))]
public class AnimatedTMP : MonoBehaviour
{
    private TextMeshProUGUI m_textRenderer;
    private Animator m_animator; // Use "TMP" controller as Controller
    public string text
    {
        get
        {
            // Return the value of the private field.
            return text;
        }
        set
        {
            m_textRenderer.text = text;
            text = value;
        }
    }

    public void Awake()
    {
        m_textRenderer = GetComponent<TextMeshProUGUI>();
        m_animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Increases text size for a bit then returns to it's previous size
    /// </summary>
    public void PopOut()
    {
        m_animator.Play("TMP_PopOut", -1, 0f);
        /*
        // Cache the original scale.
        Vector3 originalScale = transform.localScale;
        // Create a sequence: scale up then back to normal.
        Sequence popSequence = DOTween.Sequence();
        popSequence.Append(transform.DOScale(originalScale * 1.2f, 0.2f))
                   .Append(transform.DOScale(originalScale, 0.2f));*/
    }

    /// <summary>
    /// Shakes text, turns it red, then fades the red out to the previous color
    /// </summary>
    public void ShakeError(float duration = 0.5f, float strength = 10f)
    {
        Color originalColor = m_textRenderer.color;

        // Immediately set the text to red.
        m_textRenderer.color = Color.red;

        // Shake the text.
        float shakeDuration = duration;
        float shakeStrength = strength;
        ShakeEffect(shakeDuration, shakeStrength);

        // Tween the color from red back to the original color over the shake duration.
        m_textRenderer.DOColor(originalColor, shakeDuration);
    }


    private void ShakeEffect(float duration, float strength)
    {
        transform.DOShakePosition(duration, new Vector3(strength, strength, 0), 20, 90, false, true);
    }
}
