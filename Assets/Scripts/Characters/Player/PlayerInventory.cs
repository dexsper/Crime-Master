using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PlayerInventory : MonoBehaviour
{
    private List<CardInfo> _cards = new List<CardInfo>();

    public UnityEvent<CardInfo> OnCardAdded;
    public UnityEvent<CardInfo> OnCardRemoved;

    [Inject]
    private LevelManager _levelManager;

    [Inject]
    private Robbery _robbery;

    private List<CardInfo> _tempCards = new List<CardInfo>();

    private void Start()
    {
        _robbery.OnLose.AddListener(RemoveTempCards);
        _robbery.OnSuccess.AddListener(ClearTempCards);
    }

    private void ClearTempCards()
    {
        _tempCards.Clear();
    }

    private void RemoveTempCards()
    {
        for (int i = 0; i < _tempCards.Count; i++)
        {
            var card = _tempCards[i];

            RemoveCard(card);
        }

        _tempCards.Clear();
    }

    public void AddCard(CardInfo info)
    {
        _cards.Add(info);

        OnCardAdded?.Invoke(info);

        _tempCards.Add(info);
    }

    public void RemoveCard(CardInfo info)
    {
        if (_cards.Contains(info))
        {
            _cards.Remove(info);

            OnCardRemoved?.Invoke(info);
        }
    }

    public List<CardInfo> Cards => _cards;
}
