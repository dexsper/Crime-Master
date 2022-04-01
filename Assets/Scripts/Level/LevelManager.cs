using System;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level[] _levels;

    public Level CurrentLevel { get; private set; }

    public UnityEvent<Level> LevelChanged;

    private void Start()
    {
        if (_levels == null)
            throw new NullReferenceException("Start level can't be null");

        ChangeLevel(_levels[0]);
    }

    public void ChangeLevel(Level startLevel)
    {
        CurrentLevel = startLevel;
        LevelChanged?.Invoke(CurrentLevel);
    }
}
