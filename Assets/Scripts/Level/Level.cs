using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class CardSpawn
{
    public CardInfo CardOne;
    public CardInfo CardTwo;

}


[CreateAssetMenu(fileName = "New Level", menuName = "Crime/Level")]
public partial class Level : ScriptableObject
{
    [Header("Info")]
    [SerializeField] private string _levelName;

    [Header("UI")]
    [SerializeField] private Sprite _safeSprite;
    [SerializeField] private GameObject _rabberyField; 

    [Header("Cards")]
    [Range(10, 20)]
    [SerializeField] private float _cardSpace = 10f;
    [SerializeField] private List<CardSpawn> _cards;

    [Header("Money")]
    [Range(10, 30)]
    [SerializeField] private float _moneySpace = 5f;

    public List<CardSpawn> Cards => _cards;
    public float CardSpace => _cardSpace;
    public float MoneySpace => _moneySpace;
    public string LevelName => _levelName;
    public CardPlace[] Places
    {
        get
        {
            if (_rabberyField == null)
                return new CardPlace[0];

            return _rabberyField.GetComponentsInChildren<CardPlace>();
        }
    }
    public Sprite SafeSprite => _safeSprite;
    public GameObject PlacesField => _rabberyField;

}
