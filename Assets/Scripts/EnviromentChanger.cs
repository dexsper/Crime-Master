using System;
using UnityEngine;
using Zenject;

public class EnviromentChanger : MonoBehaviour
{
    private GameObject _currentEnviroment;

    [Inject]
    private LevelManager _levelManager;

    private void Awake()
    {
        _levelManager.LevelChanged.AddListener(ChangeEnviroment);
    }

    private void ChangeEnviroment(Level level)
    {
        if (level.Enviroment == null) return;

        if (_currentEnviroment != null)
            Destroy(_currentEnviroment.gameObject);


        _currentEnviroment = Instantiate(level.Enviroment, transform);
    }
}
