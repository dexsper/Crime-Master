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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == _player.gameObject)
        {
            _player.Movement.enabled = false;
            _input.Enabled = false;

            _robbery.gameObject.SetActive(true);
        }
    }
}
