using UnityEngine.EventSystems;
using UnityEngine;

public class CardDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Camera _camera;
    private Vector2 _offset;
    private Transform _defaultParent;
    private CanvasGroup _canvasGroup;

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
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position + _offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_defaultParent);

        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
    }
}
