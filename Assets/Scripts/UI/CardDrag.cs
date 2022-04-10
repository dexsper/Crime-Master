using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
using DG.Tweening;
public class CardDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Camera _camera;
    private Vector2 _offset;
    private Transform _defaultParent;
    private CanvasGroup _canvasGroup;
    private Tween _lastTween;

    private Vector3 defaultScale;

    private void Awake()
    {
        _camera = Camera.main;
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _defaultParent = transform.parent;
        _offset = (Vector2)transform.position - eventData.position;

        transform.SetParent(transform.parent.parent.parent.parent);

        _canvasGroup.alpha = .7f;
        _canvasGroup.blocksRaycasts = false;

        defaultScale = transform.localScale;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + _offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        StartCoroutine(HandleEndDrag());
    }

    public IEnumerator HandleEndDrag()
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        _lastTween = transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
        yield return new WaitForSeconds(0.3f);
        transform.SetParent(_defaultParent);
        _lastTween = transform.DOScale(defaultScale, 0.3f).SetEase(Ease.OutBack);
    }

    private void OnDestroy()
    {
        _lastTween.Complete();
    }
}
