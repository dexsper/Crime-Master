using System.Linq;
using UnityEngine;
using Zenject;

public class CityMarkers : MonoBehaviour
{
    [SerializeField] private BuildingMarker[] _buildings;

    [Inject]
    private LevelManager _levelManager;

    private void Awake()
    {
        _levelManager.LevelChanged.AddListener(ShowMarker);
    }

    private int _currentMarker = 0;

    private void ShowMarker(Level level)
    {
        if (_currentMarker > _buildings.Length)
            _currentMarker = 0;

        _buildings[_currentMarker].SetEnable(true);
    }
}
