using System.Collections;
using UnityEngine;
using Zenject;
using DG.Tweening;

public class FinalScreen : Panel
{

    [Header("Panels")]
    [SerializeField] private GameObject _successPanel;
    [SerializeField] private GameObject _losePanel;

    [Inject]
    private LevelManager _levelManager;

    [Inject]
    private CameraController _cameraController;

    [Inject]
    private Player _player;

    public void RestartLevel()
    {
        _levelManager.ChangeLevel(_levelManager.CurrentLevel);
        StartCoroutine(DisableFinishPanel());

        OnHide?.Invoke();
    }
    public void NextLevel()
    {
        _levelManager.NextLevel();
        StartCoroutine(DisableFinishPanel());

        OnHide?.Invoke();
    }

    public void ShowLose()
    {
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        _losePanel.transform.localScale = Vector3.zero;
        _losePanel.gameObject.SetActive(true);
        _losePanel.transform.DOScale(Vector3.one, 0.5f);

        OnShow?.Invoke();
    }

    public void ShowSuccess()
    {
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        _successPanel.transform.localScale = Vector3.zero;
        _successPanel.gameObject.SetActive(true);
        _successPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

        OnShow?.Invoke();
    }

    private IEnumerator DisableFinishPanel()
    {
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
        _cameraController.ShowCity();
        yield return new WaitForSeconds(2f);
        _player.gameObject.SetActive(true);
        _successPanel.gameObject.SetActive(false);
        _losePanel.gameObject.SetActive(false);

    }
}
