using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Finish : MonoBehaviour
{
    private bool _entered = false;

    [Inject]
    private Player _player;

    [Inject]
    private IInput _input;

    [Inject]
    private Robbery _robbery;

    [Inject]
    private FinalScreen _finalScreen;

    [Inject]
    private CameraController _cameraController;

    [Inject]
    private LevelManager _levelManager;

    public UnityEvent OnEntered;

    private void Awake()
    {
        _levelManager.LevelChanged.AddListener(LevelChanged);
    }

    private void LevelChanged(Level arg0)
    {
        _entered = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player.gameObject && !_entered)
        {
            _entered = true;

            _player.Movement.enabled = false;
            _input.Enabled = false;
            _player.gameObject.SetActive(false);
            OnEntered.Invoke();
            if (_player.Inventory.Cards.Count > 0)
            {
                _robbery.gameObject.SetActive(true);
            }
            else
            {
                _cameraController.ShowCity();
                _finalScreen.ShowLose();
            }


        }
    }
}
