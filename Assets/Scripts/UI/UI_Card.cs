using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UI_Card : MonoBehaviour
{
    private CardInfo _cardInfo;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _costText;

    [SerializeField] private TextMeshProUGUI _powerText;
    [SerializeField] private TextMeshProUGUI _terrifyingText;
    [SerializeField] private TextMeshProUGUI _intelectText;

    [Header("Images")]
    [SerializeField] private Image _iconImage;
    [SerializeField] private Image _costBorder;
    [SerializeField] private Image _backgroundBorder;

    public CardInfo Info => _cardInfo;

    public void Setup(CardInfo info)
    {
        _cardInfo = info;

        if (_costText != null)
            _costText.text = $"{info.Cost} $";
        if (_nameText != null)
            _nameText.text = $"{info.Name}";
        if (_iconImage != null)
            _iconImage.sprite = info.IconSprite;

        SetupAbilities(info);

        if (_costBorder != null)
            _costBorder.color = CardInfo.TierColors[info.Tier];
        if (_backgroundBorder != null)
            _backgroundBorder.color = CardInfo.TierColors[info.Tier];

    }

    private void SetupAbilities(CardInfo info)
    {
        if (_powerText != null)
        {
            _powerText.transform.parent.gameObject.SetActive(info.Power > 0);
            _powerText.text = $"{info.Power}";
        }
        if (_terrifyingText != null)
        {
            _terrifyingText.transform.parent.gameObject.SetActive(info.Terrifying > 0);
            _terrifyingText.text = $"{info.Terrifying}";
        }
        if (_intelectText != null)
        {
            _intelectText.transform.parent.gameObject.SetActive(info.Intellect > 0);
            _intelectText.text = $"{info.Intellect}";
        }
    }
}
