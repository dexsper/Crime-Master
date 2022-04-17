using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

public class UI_LevelCards : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _powerText;

    [Inject]
    private LevelManager _levelManager;

    [Inject]
    private Player _player;

    private void Start()
    {
        _player.Inventory.OnCardAdded.AddListener((x) => UpdateUI());
        _player.Inventory.OnCardRemoved.AddListener((x) => UpdateUI());
        _levelManager.LevelChanged.AddListener((x) => UpdateUI());
    }

    private void UpdateUI()
    {
        var cards = _player.Inventory.Cards;
        int power = cards.Sum(x => x.Power);

        var places = _levelManager.CurrentLevel.Places;
        int requiredPower = places.Sum(x => x.RequiredPower);

        _powerText.text = $"{power} / {requiredPower}";
    }
}
