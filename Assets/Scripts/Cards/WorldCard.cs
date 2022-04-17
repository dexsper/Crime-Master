using UnityEngine;

public class WorldCard : BaseInteractable
{
    private UI_Card _uiCard;
    private CanvasGroup _canvasGroup;
    private Color _notEnoughColor = Color.red;

    private void Awake()
    {
        _uiCard = GetComponentInChildren<UI_Card>();
        _canvasGroup = GetComponent<CanvasGroup>();
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
            _player.PlayGoodMoodParticles();
        }
        else
        {
            _player.TextNotify.Show("Not enough money", _notEnoughColor);
        }

        Destroy(gameObject);
    }

    protected override void Update()
    {
        base.Update();

        if (_uiCard.Info != null)
        {
            if (_player.Economics.EnoughMoney(_uiCard.Info.Cost))
            {
                _canvasGroup.alpha = 1f;
            }
            else
            {
                _canvasGroup.alpha = 0.7f;
            }
        }
    }
}
