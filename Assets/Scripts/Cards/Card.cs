using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Card : MonoBehaviour
{
    [Inject]
    private Player _player;

    [SerializeField] private TextMeshProUGUI _fireText;
    [SerializeField] private TextMeshProUGUI _hackerText;
    [SerializeField] private TextMeshProUGUI _horrifyText;
    [SerializeField] private TextMeshProUGUI _priceText;

    [SerializeField] private Image _iconImage;

    private CardInfo _info;

    public void Setup(CardInfo info)
    {
        _info = info;

        if (_fireText != null)
            _fireText.text = _info.FirePower.ToString();
        if (_hackerText != null)
            _hackerText.text = _info.HackerPower.ToString();
        if (_horrifyText != null)
            _horrifyText.text = _info.HackerPower.ToString();
        if (_priceText != null)
            _priceText.text = _info.Price.ToString();
        if (_iconImage != null)
            _iconImage.sprite = _info.IconSprite;
    }

    public void Use()
    {
        _player.PlayerCards.AddCard(_info);
        Destroy(gameObject);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player.gameObject)
        {
            Use();
        }
    }
}
