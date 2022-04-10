using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UI_Card : MonoBehaviour
{
    private CardInfo _cardInfo;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _powerText;
    [SerializeField] private TextMeshProUGUI _costText;

    [Header("Images")]
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _costBorder;
    [SerializeField] private Image _backgroundBorder;

    [Inject]
    private Player _player;
    public CardInfo Info => _cardInfo;

    public void Setup(CardInfo info)
    {
        _cardInfo = info;

        if (_powerText != null)
            _powerText.text = $"{info.Power}";
        if (_costText != null)
            _costText.text = $"{info.Cost} $";

        if (_iconImage != null)
            _iconImage.sprite = info.IconSprite;

        if (_costBorder != null)
            _costBorder.color = CardInfo.TierColors[info.Tier];
        if (_backgroundBorder != null)
            _backgroundBorder.color = CardInfo.TierColors[info.Tier];

    }
}
