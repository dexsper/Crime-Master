using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level _startLevel;

    public Level CurrentLevel { get; private set; }

    private void Awake()
    {
        if (_startLevel == null)
            throw new NullReferenceException("Start level can't be null");
        ChangeLevel(_startLevel);
    }

    public void ChangeLevel(Level startLevel)
    {
        CurrentLevel = startLevel;
    }
}
