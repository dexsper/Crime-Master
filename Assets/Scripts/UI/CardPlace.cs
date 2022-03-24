using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardPlace : MonoBehaviour, IDropHandler, ICharacterStats
{
    [Header("Images")]
    [SerializeField] private Image _borderImage;
    [SerializeField] private Image _iconImage;

    [Header("Require Abilities")]
    [Range(1, 30)]
    [SerializeField] private int _firePower;
    [Range(1, 30)]
    [SerializeField] private int _hackerPower;
    [Range(1, 30)]
    [SerializeField] private int _horrifyPower;

    private CardInfo _cardInfo;

    public float Chance
    {
        get
        {
            float chance = 0f;

            if (_cardInfo != null)
            {
                float fire = Mathf.Clamp01(_cardInfo.FirePower / _firePower);
                float hacker = Mathf.Clamp01(_cardInfo.HackerPower / _hackerPower);
                float horrify = Mathf.Clamp01(_cardInfo.HorrifyPower / _horrifyPower);

                chance = Mathf.Clamp01((fire + hacker + horrify) / 3f);
            }

            return chance;
        }
    }
    public int FirePower => _firePower;
    public int HackerPower => _hackerPower;
    public int HorrifyPower => _horrifyPower;

    public bool HasCard => _cardInfo != null;   

    public void OnDrop(PointerEventData eventData)
    {
        UI_Card card = eventData.pointerDrag.GetComponent<UI_Card>();

        if (card != null)
        {
            _cardInfo = card.Info;

            _borderImage.color = CardInfo.TierColors[_cardInfo.Tier];

            _iconImage.sprite = _cardInfo.IconSprite;
            _iconImage.color = Color.white;

            Destroy(card.gameObject);
        }
    }

    public void ResetInfo()
    {
        _cardInfo = null;

        _borderImage.color = Color.white;
        _iconImage.sprite = null;
    }
}
