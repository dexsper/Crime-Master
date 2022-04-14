using TMPro;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimedText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void Show(string text, Color color)
    {
        _text.text = text;
        _text.color = color;
        _text.DOFade(1f, .6f).From().SetEase(Ease.InQuad).OnComplete(() =>
        {
            _text.DOFade(0f, 1.5f).SetEase(Ease.OutQuad);
        });

    }
}
