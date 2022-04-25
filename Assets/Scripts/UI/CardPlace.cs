using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;
using UnityEngine.Events;

public class CardPlace : MonoBehaviour, IDropHandler, IStats
{
    [Header("Require Abilities")]
    [Range(1, 30)]
    [SerializeField] private int _requiredPower;
    [Range(1, 30)]
    [SerializeField] private int _requiredTerrifying;
    [Range(1, 30)]
    [SerializeField] private int _requiredIntelect;

    [Header("Images")]
    [SerializeField] private Image _borderImage;
    [SerializeField] private Image _iconImage;

    [SerializeField] private Color _borderColor;

    [Inject]
    private Player _player;
    private CardInfo _cardInfo;

    public float Chance { get; private set; }
    public CardInfo CardInfo => _cardInfo;
    public bool HasCard => _cardInfo != null;

    public int Terrifying => _requiredTerrifying;

    public int Power => _requiredPower;

    public int Intellect => _requiredIntelect;

    public UnityEvent OnPlaced;


    public void OnDrop(PointerEventData eventData)
    {
        UI_Card card = eventData.pointerDrag.GetComponent<UI_Card>();

        if (card != null && _cardInfo == null)
        {
            _cardInfo = card.Info;

            _player.Inventory.RemoveCard(_cardInfo);

            _borderImage.color = CardInfo.TierColors[_cardInfo.Tier];

            _iconImage.transform.localScale = Vector3.zero;
            _iconImage.sprite = _cardInfo.IconSprite;
            _iconImage.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack);

            _iconImage.color = Color.white;
            // card.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
            Destroy(card.gameObject, 0.3f);

            Chance = GetChance();
            OnPlaced?.Invoke();
        }

    }
    public void ResetInfo()
    {
        _cardInfo = null;

        _borderImage.color = _borderColor;
        _iconImage.sprite = null;
    }


    private float GetChance()
    {
        float chance = 0f;

        if (_cardInfo != null)
        {
            var powerChance = (float)_cardInfo.Power / (float)Power;
            var terrifyingChance = (float)_cardInfo.Terrifying / (float)Terrifying;
            var intelectChance = (float)_cardInfo.Terrifying / (float)Intellect;

            chance =  Mathf.Clamp01((powerChance + terrifyingChance + intelectChance) / 3f);
        }

        return chance;
    }
}
