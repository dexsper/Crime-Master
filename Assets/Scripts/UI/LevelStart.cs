using Cinemachine;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;

public class LevelStart : MonoBehaviour
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
    [SerializeField] private TextMeshProUGUI _fireText;
    [SerializeField] private TextMeshProUGUI _hackerText;
    [SerializeField] private TextMeshProUGUI _horrifyText;

    [Header("Images")]
    [SerializeField] private Image _safeImage;

    [Inject]
    private LevelManager _levelManager;

    [Inject]
    private CameraController _cameraController;

    private void OnEnable()
    {
        SetupInterface();
    }

    private void SetupInterface()
    {
        _bloorPanel.SetActive(true);
        _contentPanel.SetActive(true);

        var level = _levelManager.CurrentLevel;

        int firePower = level.Places.Sum(x => x.FirePower);
        int hackerPower = level.Places.Sum(x => x.HackerPower);
        int horrifyPower = level.Places.Sum(x => x.HorrifyPower);

        if (_levelNameText != null)
            _levelNameText.text = level.LevelName;
        if (_fireText != null)
            _fireText.text = $"{firePower}";
        if (_hackerText != null)
            _hackerText.text = $"{hackerPower}";
        if (_horrifyText != null)
            _horrifyText.text = $"{horrifyPower}";

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