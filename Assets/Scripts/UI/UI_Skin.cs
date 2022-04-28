using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Skin : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private TextMeshProUGUI _percentageText;

    public void UpdateProgress(float p)
    {
        _fillImage.DOFillAmount(p, .7f).SetEase(Ease.OutBack);

        int textPercentage = Mathf.RoundToInt(p * 100);

        _percentageText.text = $"{textPercentage}%";
    }
}
