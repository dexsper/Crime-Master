using System;
using System.Collections;
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

    public IEnumerator ShowAnimation(Action callback)
    {
        _buildings[_currentMarker].AnimationObject.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        _buildings[_currentMarker].AnimationObject.SetActive(false);


        callback?.Invoke();
    }
}
