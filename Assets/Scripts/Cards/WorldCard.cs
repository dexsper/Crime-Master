using UnityEngine;

[RequireComponent(typeof(UI_Card))]
public class WorldCard : BaseInteractable
{
    private UI_Card _uiCard;

    private void Awake()
    {
        _uiCard = GetComponent<UI_Card>();
    }
    public void Setup(CardInfo info)
    {
        _uiCard.Setup(info);
    }
    public override void Use()
    {
        if (_player.Economics.EnoughMoney(_uiCard.Info.Cost))
        {
            _player.Economics.Take(_uiCard.Info.Cost);
            _player.Inventory.AddCard(_uiCard.Info);
        }
        else
        {
            _player.Movement.AddBackForce();
        }

        Destroy(gameObject);
    }
}
