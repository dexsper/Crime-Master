using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Card : MonoBehaviour
{
    [Inject]
    protected Player _player;

    [SerializeField] private TextMeshProUGUI _fireText;
    [SerializeField] private TextMeshProUGUI _hackerText;
    [SerializeField] private TextMeshProUGUI _horrifyText;
    [SerializeField] private TextMeshProUGUI _priceText;

    [SerializeField] private Image _iconImage;

    private CardInfo _mercenary;

    public void Setup(CardInfo info)
    {
        _mercenary = info;

        if (_fireText != null)
            _fireText.text = _mercenary.FirePower.ToString();
        if (_hackerText != null)
            _hackerText.text = _mercenary.HackerPower.ToString();
        if (_horrifyText != null)
            _horrifyText.text = _mercenary.HackerPower.ToString();
        if (_priceText != null)
            _priceText.text = _mercenary.Price.ToString();

        _iconImage.sprite = _mercenary.IconSprite;
    }

    public void Use()
    {
        Destroy(gameObject);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player.gameObject)
            Use();
    }
}
