using DG.Tweening;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UI_LevelCards : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _powerText;

    [SerializeField] private Image _fillImage;

    [Inject]
    private LevelManager _levelManager;

    [Inject]
    private Player _player;

    private void Awake()
    {
        _levelManager.LevelChanged.AddListener((x) => UpdateUI());

    }

    private void Start()
    {
        _player.Inventory.OnCardAdded.AddListener((x) => UpdateUI());
        _player.Inventory.OnCardRemoved.AddListener((x) => UpdateUI());

    }

    private void OnEnable()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        float power = 0f;

        if (_player != null && _player.Inventory != null)
        {
            var cards = _player.Inventory.Cards;
            power = cards.Sum(x => x.Power);
        }

        if (_levelManager.CurrentLevel != null)
        {
            var places = _levelManager.CurrentLevel.Places;
            float requiredPower = places.Sum(x => x.RequiredPower);

            float value = Mathf.Clamp01(power / requiredPower);

            _powerText.text = $"{power} / {requiredPower}";
            _fillImage.DOFillAmount(value, 0.3f).SetEase(Ease.OutBack);
        }
        else
        {
            _powerText.text = "";
        }
    }
}
