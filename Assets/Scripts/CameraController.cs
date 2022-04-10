using Cinemachine;
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject _city;

    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera _playerCamera;
    [SerializeField] private CinemachineVirtualCamera _cityCamera;

    [Header("Events")]
    public UnityEvent OnCityShow;
    public UnityEvent OnPlayerShow;

    [Inject]
    private IInput _input;

    [Inject]
    private Player _player;

    public void ShowCity()
    {
        _city.gameObject.SetActive(true);
        _playerCamera.Priority = -1;

        OnCityShow?.Invoke();
        DOTween.To(() => _cityCamera.m_Lens.FieldOfView, x => _cityCamera.m_Lens.FieldOfView = x, 70, 1.5f).SetEase(Ease.InOutBack);
    }
    public void ShowPlayer()
    {
        DOTween.To(() => _cityCamera.m_Lens.FieldOfView, x => _cityCamera.m_Lens.FieldOfView = x, 20, 1.5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            _playerCamera.Priority = 1;
            _input.Enabled = true;
            _player.Movement.enabled = true;
            OnPlayerShow?.Invoke();
        });
    }
}
