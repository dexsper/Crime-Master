using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using DG.Tweening;
using System.Collections;

public class BuildingMarker : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _marker;
    [SerializeField] private Material _emissionMaterial;
    [SerializeField] private GameObject _animationObject;

    private Material _defaultMaterial;
    private bool _enabled = false;
    private MeshRenderer _meshRenderer;
    [Inject]
    private LevelStart _levelStart;

    public GameObject AnimationObject => _animationObject;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _marker.SetActive(false);
        _animationObject.SetActive(false);
        _defaultMaterial = _meshRenderer.material;
    }

    private void OnEnable()
    {
        StartCoroutine(IEPlayMarkerAnimation());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void SetEnable(bool enable)
    {
        _enabled = enable;

        _marker.SetActive(_enabled);

        _meshRenderer.material = _enabled ? _emissionMaterial : _defaultMaterial;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_enabled) return;

        _levelStart.transform.localScale = Vector3.zero;
        _levelStart.gameObject.SetActive(true);
        _levelStart.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

        SetEnable(false);
    }

    private IEnumerator IEPlayMarkerAnimation()
    {
        float animationLength = 0.3f;
        yield return new WaitForSeconds(2);
        _marker.transform.DOScale(Vector3.one * 0.013f, animationLength).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(animationLength);
        _marker.transform.DOScale(Vector3.one * 0.01f, animationLength).SetEase(Ease.InOutBack);
        yield return IEPlayMarkerAnimation();
    }
}
