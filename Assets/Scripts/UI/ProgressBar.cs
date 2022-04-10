using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _fillImage;

    private void OnDisable()
    {
        _fillImage.fillAmount = 0;
    }

    public void SetProgress(float value)
    {
        value = Mathf.Clamp01(value);

        _fillImage.DOFillAmount(value, 0.3f).SetEase(Ease.OutBack);
    }
}
