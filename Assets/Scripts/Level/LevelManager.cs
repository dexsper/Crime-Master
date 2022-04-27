using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;

    public List<Level> Levels => _levels;
    public Level CurrentLevel { get; private set; }

    public UnityEvent<Level> LevelChanged;

    public void ChangeLevel(Level level)
    {
        CurrentLevel = level;

        LevelChanged?.Invoke(CurrentLevel);
    }
}
