using System;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level _startLevel;

    public Level CurrentLevel { get; private set; }

    public UnityEvent<Level> LevelChanged;

    private void Start()
    {
        if (_startLevel == null)
            throw new NullReferenceException("Start level can't be null");
        ChangeLevel(_startLevel);
    }

    public void ChangeLevel(Level startLevel)
    {
        CurrentLevel = startLevel;
        LevelChanged?.Invoke(CurrentLevel);
    }
}
