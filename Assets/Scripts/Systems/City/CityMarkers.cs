using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CityMarkers : MonoBehaviour
{
    [SerializeField] private BuildingMarker[] _buildings;

    [Inject]
    private LevelManager _levelManager;
    [Inject]
    private CameraController _cameraController;
    [Inject]
    private Robbery _robbery;

    private List<Level> _completedLevels = new List<Level>();

    private void Start()
    {
        _cameraController.OnCityShow.AddListener(UpdateMarkers);
        _robbery.OnSuccess.AddListener(CompleteLevel);

    }

    private void CompleteLevel()
    {
        _completedLevels.Add(_levelManager.CurrentLevel);
    }

    private void UpdateMarkers()
    {
        for (int i = 0; i < _levelManager.Levels.Count; i++)
        {
            var level = _levelManager.Levels[i];

            if (_completedLevels.Contains(level)) continue;

            if (i >= _buildings.Length)
                break;

            _buildings[i].SetLevel(level);
        }
    }
}
