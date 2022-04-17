using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using System.Collections;

public class ProgressBar : MonoBehaviour
{
    [Header("Animation Settings")]
    [Range(1, 3)]
    [SerializeField] private int _passesCount = 2;
    [Range(1, 10f)]
    [SerializeField] private float _animationTime = 5f;
    [Space]

    [Header("UI")]
    [SerializeField] private Image _fillImage;
    [SerializeField] private RectTransform _cursorArea;
    [SerializeField] private Image _cursor;

    private void OnDisable()
    {
        _fillImage.fillAmount = 0;

        _cursor.rectTransform.anchoredPosition = CursorStartPosition();
    }

    public IEnumerator AnimateCursor(float t, Action callback = null)
    {
        Vector2 start = CursorStartPosition();
        float endX = -start.x;

        _cursor.rectTransform.anchoredPosition = new Vector2(start.x, start.y);

        float duration = _animationTime / _passesCount;

        for (int i = 0; i < _passesCount - 1; i++)
        {
            _cursor.rectTransform.DOAnchorPosX(endX, duration);

            yield return new WaitForSeconds(duration);

            _cursor.rectTransform.DOAnchorPosX(start.x, duration);

            yield return new WaitForSeconds(duration);
        }

        float targetX = Mathf.Lerp(start.x, endX, t);

        _cursor.rectTransform.DOAnchorPosX(targetX, duration).OnComplete(() => callback?.Invoke());

        yield return null;
    }

    public void SetProgress(float value)
    {
        value = Mathf.Clamp01(value);

        _fillImage.DOFillAmount(value, 0.3f).SetEase(Ease.OutBack);
    }

    private Vector2 CursorStartPosition()
    {
        float x = _cursorArea.rect.x;
        float y = _cursor.rectTransform.anchoredPosition.y;

        return new Vector2(x, y);
    }
}
