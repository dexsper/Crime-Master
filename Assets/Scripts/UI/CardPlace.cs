using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using DG.Tweening;
using System.Collections;

public class CardPlace : MonoBehaviour, IDropHandler
{
    [Header("Require Abilities")]
    [Range(1, 30)]
    [SerializeField] private int _firePower;
    [Range(1, 30)]
    [SerializeField] private int _hackerPower;
    [Range(1, 30)]
    [SerializeField] private int _horrifyPower;

    public int FirePower => _firePower;
    public int HackerPower => _hackerPower;
    public int HorrifyPower => _horrifyPower;

    [Header("Images")]
    [SerializeField] private Image _borderImage;
    [SerializeField] private Image _iconImage;

    [Inject]
    private PlayerInventory _playerInventory;

    private CardInfo _cardInfo;

    public float Chance
    {
        get
        {
            float chance = 0f;

            if (_cardInfo != null)
            {
                float fire = Mathf.Clamp01(_cardInfo.FirePower / FirePower);
                float hacker = Mathf.Clamp01(_cardInfo.HackerPower / HackerPower);
                float horrify = Mathf.Clamp01(_cardInfo.HorrifyPower / HorrifyPower);

                chance = Mathf.Clamp01((fire + hacker + horrify) / 3f);
            }

            return chance;
        }
    }

    public bool HasCard => _cardInfo != null;
    public void OnDrop(PointerEventData eventData)
    {
        UI_Card card = eventData.pointerDrag.GetComponent<UI_Card>();

        if (card != null && _cardInfo == null)
        {
            _cardInfo = card.Info;

            _playerInventory.Cards.Remove(_cardInfo);

            _borderImage.color = CardInfo.TierColors[_cardInfo.Tier];

            _iconImage.transform.localScale = Vector3.zero;
            _iconImage.sprite = _cardInfo.IconSprite;
            _iconImage.transform.DOScale(Vector3.one, 1f).SetEase(Ease.OutBack);

            _iconImage.color = Color.white;
            card.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
            Destroy(card.gameObject, 0.3f);
        }

    }
    public void ResetInfo()
    {
        _cardInfo = null;

        _borderImage.color = Color.white;
        _iconImage.sprite = null;
    }


}
