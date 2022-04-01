using Cinemachine;
using System.Collections;
using UnityEngine;
using Zenject;

public class FinalScreen : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera _playerCamera;
    [SerializeField] private CinemachineVirtualCamera _cityCamera;

    [Header("Panels")]
    [SerializeField] private GameObject _successPanel;
    [SerializeField] private GameObject _losePanel;

    [Inject]
    private LevelManager _levelManager;

    [Inject]
    private IInput _input;

    public void RestartLevel()
    {
        _successPanel.gameObject.SetActive(false);
        _losePanel.gameObject.SetActive(false);

        _levelManager.ChangeLevel(_levelManager.CurrentLevel);

        StartCoroutine(ShowCity());
    }
    public void NextLevel()
    {
        _successPanel.gameObject.SetActive(false);
        StartCoroutine(ShowCity());
    }

    public void ShowLose()
    {
        _losePanel.gameObject.SetActive(true);
    }

    public void ShowSuccess()
    {
        _successPanel.gameObject.SetActive(true);
    }

    private IEnumerator ShowCity()
    {

        _playerCamera.Priority = 0;
        while (_cityCamera.m_Lens.FieldOfView < 70)
        {
            _cityCamera.m_Lens.FieldOfView += 1.5f;
            yield return new WaitForSeconds(.02f);
        }
    }
}
