using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    private List<CardInfo> _cards = new List<CardInfo>();

    public UnityEvent<CardInfo> OnCardAdded;
    public UnityEvent<CardInfo> OnCardRemoved;

    public void AddCard(CardInfo info)
    {
        _cards.Add(info);

        OnCardAdded?.Invoke(info);
    }

    public void RemoveCard(CardInfo info)
    {
        if(_cards.Contains(info))
        {
            _cards.Remove(info);

            OnCardRemoved?.Invoke(info);
        }
    }

    public List<CardInfo> Cards => _cards;
}
