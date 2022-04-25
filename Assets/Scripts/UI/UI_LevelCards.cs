using DG.Tweening;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[Serializable]
public class Ability
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _fill;

    public TextMeshProUGUI Text => _text;
    public Image Image => _fill;
}

public class UI_LevelCards : MonoBehaviour
{
    [Header("Abilities")]
    [SerializeField] private Ability _power;
    [SerializeField] private Ability _terrifying;
    [SerializeField] private Ability _intelect;

    [Inject]
    private LevelManager _levelManager;
    [Inject]
    private CameraController _cameraController;
    [Inject]
    private Player _player;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _levelManager.LevelChanged.AddListener((x) => UpdateUI());
        _cameraController.OnCityShow.AddListener(Disable);
        _cameraController.OnPlayerShow.AddListener(Enable);

        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _player.Inventory.OnCardAdded.AddListener((x) => UpdateUI());
        _player.Inventory.OnCardRemoved.AddListener((x) => UpdateUI());

    }

    private void Enable()
    {
        _canvasGroup.DOFade(1f, .5f);
    }

    private void Disable()
    {
        _canvasGroup.DOFade(0f, .5f);
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        int power = 0;
        int terrifying = 0;
        int intelect = 0;

        if (_player != null && _player.Inventory != null)
        {
            for (int i = 0; i < _player.Inventory.Cards.Count; i++)
            {
                var card = _player.Inventory.Cards[i];

                power += card.Power;
                intelect += card.Intellect;
                terrifying += card.Terrifying;
            }
        }

        if (_levelManager.CurrentLevel != null)
        {
            var places = _levelManager.CurrentLevel.Places;

            int requiredPower = 0;
            int requiredTerrifying = 0;
            int requiredIntelect = 0;

            for (int i = 0; i < places.Length; i++)
            {
                requiredIntelect += places[i].Intellect;
                requiredTerrifying += places[i].Terrifying;
                requiredPower += places[i].Power;
            }

            float powerValue = Mathf.Clamp01((float)power / (float)requiredPower);
            float intelectValue = Mathf.Clamp01((float)intelect / (float)requiredIntelect);
            float terrifyingValue = Mathf.Clamp01((float)terrifying / (float)requiredTerrifying);

            _power.Text.text = $"{power} / {requiredPower}";
            _terrifying.Text.text = $"{terrifying} / {requiredTerrifying}";
            _intelect.Text.text = $"{intelect} / {requiredIntelect}";

            _power.Image.DOFillAmount(powerValue, 0.3f).SetEase(Ease.OutBack);
            _terrifying.Image.DOFillAmount(terrifyingValue, 0.3f).SetEase(Ease.OutBack);
            _intelect.Image.DOFillAmount(intelectValue, 0.3f).SetEase(Ease.OutBack);

            _power.Image.transform.parent.gameObject.SetActive(requiredPower > 0);
            _terrifying.Image.transform.parent.gameObject.SetActive(requiredTerrifying > 0);
            _intelect.Image.transform.parent.gameObject.SetActive(requiredIntelect > 0);

        }
        else
        {
            _power.Text.text = "";
            _terrifying.Text.text = "";
            _intelect.Text.text = "";

            _power.Image.fillAmount = 0;
            _terrifying.Image.fillAmount = 0;
            _intelect.Image.fillAmount = 0;
        }
    }
}
