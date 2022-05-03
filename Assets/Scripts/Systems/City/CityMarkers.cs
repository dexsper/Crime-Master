using System;
using System.Collections.Generic;
using System.Linq;
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
    public event Action OnMarkersUpdated;
    public List<BuildingMarker> ActiveMarkers { get; private set; } = new List<BuildingMarker>();

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
        List<Level> levelList = new List<Level>();

        if (_completedLevels.Count == 0)
        {
            levelList.Add(_levelManager.Levels[0]);
        }
        else
        {
            levelList = _levelManager.Levels.Where(x => !_completedLevels.Contains(x)).ToList();
        }

        ShowLevels(levelList);
    }

    private void ShowLevels(List<Level> levels)
    {
        ActiveMarkers.Clear();

        for (int i = 0; i < _buildings.Length; i++)
        {
            if (i >= levels.Count)
            {
                _buildings[i].SetEnable(false);
                continue;
            }

            _buildings[i].SetLevel(levels[i]);
            ActiveMarkers.Add(_buildings[i]);
        }

        OnMarkersUpdated?.Invoke();
    }
}
