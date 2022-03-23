using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : BaseInteractable
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _fireText;
    [SerializeField] private TextMeshProUGUI _hackerText;
    [SerializeField] private TextMeshProUGUI _horrifyText;
    [SerializeField] private TextMeshProUGUI costText;

    [Header("Images")]
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _costBorder;
    [SerializeField] private Image _backgroundBorder;

    private CardInfo _info;

    public void Setup(CardInfo info)
    {
        _info = info;

        if (_fireText != null)
            _fireText.text = $"{info.FirePower}";
        if (_hackerText != null)
            _hackerText.text = $"{info.HackerPower}";
        if (_horrifyText != null)
            _horrifyText.text = $"{info.HorrifyPower}";
        if (costText != null)
            costText.text = $"{info.Cost} $";

        if (_iconImage != null)
            _iconImage.sprite = _info.IconSprite;

        if (_costBorder != null)
            _costBorder.color = CardInfo.TierColors[info.Tier];        
        if (_backgroundBorder != null)
            _backgroundBorder.color = CardInfo.TierColors[info.Tier];

    }

    public override void Use()
    {
        _player.Inventory.AddCard(_info);
        Destroy(gameObject);
    }
}
