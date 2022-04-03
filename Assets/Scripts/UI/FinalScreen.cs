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

    public void RestartLevel()
    {
        // _successPanel.gameObject.SetActive(false);
        // _losePanel.gameObject.SetActive(false);

        _levelManager.ChangeLevel(_levelManager.CurrentLevel);
        StartCoroutine(DisableFinishPanel());

        ShowCity();
    }
    public void NextLevel()
    {
        // _successPanel.gameObject.SetActive(false);
        StartCoroutine(DisableFinishPanel());
        ShowCity();
    }

    public void ShowLose()
    {
        _losePanel.transform.localScale = Vector3.zero;
        _losePanel.gameObject.SetActive(true);
        _losePanel.transform.DOScale(Vector3.one, 0.5f);
    }

    public void ShowSuccess()
    {
        _successPanel.transform.localScale = Vector3.zero;
        _successPanel.gameObject.SetActive(true);
        _successPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }

    private IEnumerator DisableFinishPanel()
    {
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    private void ShowCity()
    {
        _playerCamera.Priority = -1;
        DOTween.To(() => _cityCamera.m_Lens.FieldOfView, x => _cityCamera.m_Lens.FieldOfView = x, 70, 1.5f).SetEase(Ease.InOutBack);
    }
}
