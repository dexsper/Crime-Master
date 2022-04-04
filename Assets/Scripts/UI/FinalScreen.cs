using Cinemachine;
using System.Collections;
using UnityEngine;
using Zenject;
using DG.Tweening;
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

    [Inject]
    private Player _player;

    public void RestartLevel()
    {
        _levelManager.ChangeLevel(_levelManager.CurrentLevel);
        StartCoroutine(DisableFinishPanel());

        ShowCity();
    }
    public void NextLevel()
    {
        StartCoroutine(DisableFinishPanel());
        ShowCity();
    }

    public void ShowLose()
    {
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        _losePanel.transform.localScale = Vector3.zero;
        _losePanel.gameObject.SetActive(true);
        _losePanel.transform.DOScale(Vector3.one, 0.5f);
    }

    public void ShowSuccess()
    {
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        _successPanel.transform.localScale = Vector3.zero;
        _successPanel.gameObject.SetActive(true);
        _successPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }

    private IEnumerator DisableFinishPanel()
    {
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
        yield return new WaitForSeconds(2f);
        _player.gameObject.SetActive(true);
        _successPanel.gameObject.SetActive(false);
        _losePanel.gameObject.SetActive(false);

    }

    private void ShowCity()
    {
        _playerCamera.Priority = -1;
        DOTween.To(() => _cityCamera.m_Lens.FieldOfView, x => _cityCamera.m_Lens.FieldOfView = x, 70, 1.5f).SetEase(Ease.InOutBack);
    }
}
