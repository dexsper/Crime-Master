using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _fillImage;

    public void SetProgress(float value)
    {
        value = Mathf.Clamp01(value);

        _fillImage.DOFillAmount(value, 0.3f).SetEase(Ease.OutBack);
    }
}
