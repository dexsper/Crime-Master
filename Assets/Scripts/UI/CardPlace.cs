using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardPlace : MonoBehaviour, IDropHandler
{
    [Header("Images")]
    [SerializeField] private Image _borderImage;
    [SerializeField] private Image _iconImage;

    private CardInfo _cardInfo;
    private PlaceInfo _placeInfo;

    public float Chance
    {
        get
        {
            float chance = 0f;

            if (_cardInfo != null && _placeInfo != null)
            {
                float fire = Mathf.Clamp01(_cardInfo.FirePower / _placeInfo.FirePower);
                float hacker = Mathf.Clamp01(_cardInfo.HackerPower / _placeInfo.HackerPower);
                float horrify = Mathf.Clamp01(_cardInfo.HorrifyPower / _placeInfo.HorrifyPower);

                chance = Mathf.Clamp01((fire + hacker + horrify) / 3f);
            }

            return chance;
        }
    }

    public void Setup(PlaceInfo info)
    {
        _placeInfo = info;
    }

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
