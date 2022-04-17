using Cinemachine;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;
using UnityEngine.Events;

public class LevelStart : Panel
{
    [SerializeField] private GameObject _city;

    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera _playerCamera;
    [SerializeField] private CinemachineVirtualCamera _cityCamera;

    [Header("Panels")]
    [SerializeField] private GameObject _bloorPanel;
    [SerializeField] private GameObject _contentPanel;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _levelNameText;
    [SerializeField] private TextMeshProUGUI _powerText;

    [Header("Images")]
    [SerializeField] private Image _safeImage;

    [Inject]
    private LevelManager _levelManager;

    [Inject]
    private CameraController _cameraController;


    private void OnEnable()
    {
        SetupInterface();

        OnShow?.Invoke();
    }

    private void SetupInterface()
    {
        _bloorPanel.SetActive(true);
        _contentPanel.SetActive(true);

        var level = _levelManager.CurrentLevel;

        int firePower = level.Places.Sum(x => x.RequiredPower);

        if (_levelNameText != null)
            _levelNameText.text = level.LevelName;
        if (_powerText != null)
            _powerText.text = $"{firePower}";

        if (_safeImage != null)
            _safeImage.sprite = level.SafeSprite;
    }

    public void StartLevel()
    {
        StartCoroutine(DisableStartPanel());
        _cameraController.ShowPlayer();
    }

    private IEnumerator DisableStartPanel()
    {
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        _city.gameObject.SetActive(false);
    }
}