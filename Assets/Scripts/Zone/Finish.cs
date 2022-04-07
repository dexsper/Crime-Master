using UnityEngine;
using Zenject;

public class Finish : MonoBehaviour
{
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player.gameObject)
        {
            _player.Movement.enabled = false;
            _input.Enabled = false;
            _player.gameObject.SetActive(false);

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
