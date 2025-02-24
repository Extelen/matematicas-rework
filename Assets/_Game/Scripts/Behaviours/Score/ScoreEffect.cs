using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreEffect : MonoBehaviour
{
    private TextMeshProUGUI numberText;    
    private Color initialColor;

    [Header("Combo Colors")]
    public Color lowIntensityColor = Color.white;
    public Color mediumIntensityColor = Color.yellow;
    public Color highIntensityColor = Color.red;

    public enum ComboIntensity
    {
        Low,
        Medium,
        High
    }

    void Awake()
    {
        numberText = GetComponent<TextMeshProUGUI>();
        initialColor = numberText.color;
    }

    /// <summary>
    /// Play the score effect animation
    /// </summary>
    /// <param name="text">The text to display</param>
    /// <param name="initialPosition">The initial position of the effect</param>
    /// <param name="intensity">The intensity of the combo</param>
    /// <param name="duration">The duration of the effect</param>
    /// <param name="floatDistance">The distance the text floats</param>
    public void PlayEffect(string text, Vector3 initialPosition, ComboIntensity intensity, float duration = 1f, float floatDistance = 30f)
    {
        transform.position = initialPosition;
        numberText.color = GetColorForIntensity(intensity);
        numberText.fontSize = GetFontSizeForIntensity(intensity);
        numberText.text = GetTextWithIntensity(text, intensity);

        // Play effect
        transform.DOMoveY(transform.position.y + floatDistance, duration);
        ShakeEffect(0.5f, intensity);
        numberText.DOFade(0, duration).OnComplete(() => gameObject.SetActive(false));
    }

    private Color GetColorForIntensity(ComboIntensity intensity)
    {
        switch (intensity)
        {
            case ComboIntensity.Low:
                return lowIntensityColor;
            case ComboIntensity.Medium:
                return mediumIntensityColor;
            case ComboIntensity.High:
                return highIntensityColor;
            default:
                return initialColor;
        }
    }

    private float GetFontSizeForIntensity(ComboIntensity intensity)
    {
        switch (intensity)
        {
            case ComboIntensity.Low:
                return 36f;
            case ComboIntensity.Medium:
                return 48f;
            case ComboIntensity.High:
                return 60f;
            default:
                return 36f;
        }
    }

    private string GetTextWithIntensity(string text, ComboIntensity intensity)
    {
        switch (intensity)
        {
            case ComboIntensity.Low:
                return "+" + text;
            case ComboIntensity.Medium:
                return "+" + text + "!";
            case ComboIntensity.High:
                return "+" + text + "!!";
            default:
                return "+" + text;
        }
    }

    private void ShakeEffect(float duration, ComboIntensity intensity)
    {
        float strength = GetShakeStrengthForIntensity(intensity);
        transform.DOShakePosition(duration, new Vector3(strength, strength, 0), 20, 90, false, true);
    }

    private float GetShakeStrengthForIntensity(ComboIntensity intensity)
    {
        switch (intensity)
        {
            case ComboIntensity.Low:
                return 0;
            case ComboIntensity.Medium:
                return 1f;
            case ComboIntensity.High:
                return 4f;
            default:
                return 0f;
        }
    }
}
