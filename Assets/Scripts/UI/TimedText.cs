using TMPro;
using UnityEngine;
using DG.Tweening;

public class TimedText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textPrefab;


    public void Show(string text, Color color)
    {
        var textObject = Instantiate(_textPrefab, transform);

        textObject.text = text;
        textObject.color = color;
        textObject.DOFade(1f, .6f).From().SetEase(Ease.InQuad).OnComplete(() =>
        {
            textObject.DOFade(0f, 1.5f).SetEase(Ease.OutQuad);
        });

        Destroy(textObject.gameObject, 1.5f);

    }
}
