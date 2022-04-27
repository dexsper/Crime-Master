using System.Collections;
using UnityEngine;
using Zenject;
using DG.Tweening;
using TMPro;

public class FinalScreen : Panel
{

    [Header("Panels")]
    [SerializeField] private GameObject _successPanel;
    [SerializeField] private GameObject _losePanel;

    [Header("Success")]
    [SerializeField] private TextMeshProUGUI _earnedText;
    [SerializeField] private Transform _characterIconParent;

    [Inject]
    private CameraController _cameraController;
    [Inject]
    private Player _player;

    public void RestartLevel()
    {
        StartCoroutine(DisableFinishPanel());

        OnHide?.Invoke();
    }
    public void NextLevel()
    {
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
        var uiSkin = Instantiate(_player.Skin.Next.ImagePrefab, _characterIconParent);

        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
        _successPanel.transform.localScale = Vector3.zero;
        _successPanel.gameObject.SetActive(true);
        _successPanel.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            float progress = Mathf.Clamp01((float)_player.Economics.EarnedMoney / (float)_player.Skin.Next.NeedMoney);

            uiSkin.UpdateProgress(progress);

            _player.Skin.AddProgress(progress);

        });

        if (_earnedText != null)
        {
            _earnedText.text = $"{_player.Economics.EarnedMoney}$";
        }


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


        for (int i = 0; i < _characterIconParent.childCount; i++)
        {
            Destroy(_characterIconParent.GetChild(i).gameObject);
        }
    }
}
