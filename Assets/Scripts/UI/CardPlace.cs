using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;
using System.Collections;
using UnityEngine.Events;

public class CardPlace : MonoBehaviour, IDropHandler
{
    [Header("Require Abilities")]
    [Range(1, 30)]
    [SerializeField] private int _requiredPower;

    public int RequiredPower => _requiredPower;

    [Header("Images")]
    [SerializeField] private Image _borderImage;
    [SerializeField] private Image _iconImage;

    [SerializeField] private Color _borderColor;
    [Inject]
    private PlayerInventory _playerInventory;
    private CardInfo _cardInfo;

    public float Chance { get; private set; }
    public bool HasCard => _cardInfo != null;
    public UnityEvent OnChanceChanged;


    public void OnDrop(PointerEventData eventData)
    {
        UI_Card card = eventData.pointerDrag.GetComponent<UI_Card>();

        if (card != null && _cardInfo == null)
        {
            _cardInfo = card.Info;

            _playerInventory.RemoveCard(_cardInfo);

            _borderImage.color = CardInfo.TierColors[_cardInfo.Tier];

            _iconImage.transform.localScale = Vector3.zero;
            _iconImage.sprite = _cardInfo.IconSprite;
            _iconImage.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack);

            _iconImage.color = Color.white;
            // card.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
            Destroy(card.gameObject, 0.3f);

            Chance = GetChance();
            OnChanceChanged?.Invoke();
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
            chance = (float)_cardInfo.Power / (float)RequiredPower;
        }

        return chance;
    }
}
