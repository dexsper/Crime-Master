using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;

    public Level CurrentLevel { get; private set; }

    public UnityEvent<Level> OnNextLevel;
    public UnityEvent<Level> LevelChanged;
    public UnityEvent<Level> OnRestart;

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

    public void Restart()
    {
        ChangeLevel(CurrentLevel);
        OnRestart?.Invoke(CurrentLevel);
    }

    public void NextLevel()
    {
        int index = _levels.IndexOf(CurrentLevel);
        index++;

        ChangeLevel(_levels[index]);
        OnNextLevel?.Invoke(CurrentLevel);
    }
}
