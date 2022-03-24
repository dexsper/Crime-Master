using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Card : MonoBehaviour
{
    [SerializeField]
    private CardInfo _cardInfo;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _fireText;
    [SerializeField] private TextMeshProUGUI _hackerText;
    [SerializeField] private TextMeshProUGUI _horrifyText;
    [SerializeField] private TextMeshProUGUI costText;

    [Header("Images")]
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _costBorder;
    [SerializeField] private Image _backgroundBorder;

    public CardInfo Info => _cardInfo;

    private void Awake()
    {
        if (_cardInfo != null)
            Setup(_cardInfo);
    }

    public void Setup(CardInfo info)
    {
        _cardInfo = info;

        if (_fireText != null)
            _fireText.text = $"{info.FirePower}";
        if (_hackerText != null)
            _hackerText.text = $"{info.HackerPower}";
        if (_horrifyText != null)
            _horrifyText.text = $"{info.HorrifyPower}";
        if (costText != null)
            costText.text = $"{info.Cost} $";

        if (_iconImage != null)
            _iconImage.sprite = info.IconSprite;

        if (_costBorder != null)
            _costBorder.color = CardInfo.TierColors[info.Tier];
        if (_backgroundBorder != null)
            _backgroundBorder.color = CardInfo.TierColors[info.Tier];
    }
}
