using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class UI_LevelCards : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _fireText;
    [SerializeField] private TextMeshProUGUI _hackerText;
    [SerializeField] private TextMeshProUGUI _horrifyText;

    [Inject]
    private LevelManager _levelManager;

    [Inject]
    private Player _player;

    private void Awake()
    {
        _player.Inventory.OnCardAdded.AddListener((x) => UpdateUI());
        _player.Inventory.OnCardRemoved.AddListener((x) => UpdateUI());
        _levelManager.LevelChanged.AddListener((x) => UpdateUI());
    }

    private void UpdateUI()
    {
        var cards = _player.Inventory.Cards;
        int fire = cards.Sum(x => x.FirePower);
        int hacker = cards.Sum(x => x.HackerPower);
        int horrify = cards.Sum(x => x.HorrifyPower);

        var places = _levelManager.CurrentLevel.Places;
        int requiredFire = places.Sum(x => x.FirePower);
        int requiredHacker = places.Sum(x => x.HackerPower);
        int requiredHorrify = places.Sum(x => x.HorrifyPower);

        _fireText.text = $"{fire} / {requiredFire}";
        _hackerText.text = $"{hacker} / {requiredHacker}";
        _horrifyText.text = $"{horrify} / {requiredHorrify}";
    }
}
