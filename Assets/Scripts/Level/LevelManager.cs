using System;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level _startLevel;

    public UnityEvent<Level> OnLevelChanged;

    private Level _currentLevel;

    private void Awake()
    {
        if (_startLevel == null)
            throw new NullReferenceException("Start level can't be null");
    }

    private void Start()
    {
        ChangeLevel(_startLevel);
    }

    public void ChangeLevel(Level startLevel)
    {
        _currentLevel = startLevel;
        OnLevelChanged?.Invoke(_currentLevel);
    }
}
