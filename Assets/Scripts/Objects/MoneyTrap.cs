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

    [SerializeField] private GameObject _destroyEffect;

    public void Activate(Player player)
    {
        int amount = (int)Mathf.Lerp(0, player.Economics.Money, _percentageAmount / 100f);

        player.Economics.Take(amount);
        player.TextNotify.Show($"- {amount}$", _notifyColor);

        if (_destroyEffect != null)
        {
            var effect = Instantiate(_destroyEffect, transform.position + new Vector3(0, .5f, 0), Quaternion.identity);
            Destroy(effect, _destroyTime);
        }

        Destroy(gameObject, _destroyTime / 2f);
    }
} 
