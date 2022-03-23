using System.Collections.Generic;
using UnityEngine;

public class PlayerCards : MonoBehaviour
{
    private List<CardInfo> _cards = new List<CardInfo>();

    public void AddCard(CardInfo info)
    {
        _cards.Add(info);
    }

    public List<CardInfo> Cards => _cards;
}
