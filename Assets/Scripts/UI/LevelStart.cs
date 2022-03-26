using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelStart : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _levelNameText;
    [SerializeField] private TextMeshProUGUI _fireText;
    [SerializeField] private TextMeshProUGUI _hackerText;
    [SerializeField] private TextMeshProUGUI _horrifyText;

    [Header("Images")]
    [SerializeField] private Image _safeImage;

    [Inject]
    private Player _player;

    [Inject]
    private LevelManager _levelManager;

    [Inject]
    private IInput _input;

    private void Start()
    {
        _input.Enabled = false;
        _player.Movement.enabled = false;

        SetupInterface();
    }

    private void SetupInterface()
    {
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
        _input.Enabled = true;
        _player.Movement.enabled = true;

        gameObject.SetActive(false);
    }
}
