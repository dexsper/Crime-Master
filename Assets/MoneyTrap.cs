using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class MoneyTrap : MonoBehaviour, ITrap
{
    [Range(1, 100)]
    [SerializeField] private float _percentageAmount;
    [Range(1, 5)]
    [SerializeField] private float _destroyTime = 1f;
    [SerializeField] private Color _notifyColor = Color.red;

    public UnityEvent OnActivated;

    public void Activate(Player player)
    {
        OnActivated?.Invoke();

        int amount = (int)Mathf.Lerp(0, player.Economics.Money, _percentageAmount / 100f);

        player.Economics.Take(amount);
        player.TextNotify.Show($"- {amount}$", _notifyColor);

        Destroy(gameObject, _destroyTime);
    }
} 
