using System;
using UnityEngine;
using Zenject;

public class Track : MonoBehaviour
{
    [Inject]
    private LevelManager _levelManager;

    private void Awake()
    {
        _levelManager.OnLevelChanged.AddListener(ChangeSize);
    }

    private void ChangeSize(Level level)
    {
        transform.localScale = new Vector3(1, 1, 1 * level.TrackScale);
    }
}
