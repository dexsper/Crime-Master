using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _fillImage;

    public void SetProgress(float value)
    {
        value = Mathf.Clamp01(value);

        _fillImage.fillAmount = value;
    }
}
