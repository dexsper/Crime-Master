using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class BuildingMarker : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private GameObject _marker;
    [SerializeField] private Material _emissionMaterial;

    private Material _defaultMaterial;
    private bool _enabled = false;
    private MeshRenderer _meshRenderer;

    [Inject]
    private LevelStart _levelStart;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _marker.SetActive(false);
        _defaultMaterial = _meshRenderer.material;
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

        _levelStart.gameObject.SetActive(true);

        SetEnable(false);
    }
}
