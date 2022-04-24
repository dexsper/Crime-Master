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
    [SerializeField] private List<CardSpawn> _cards;

    public List<CardSpawn> Cards => _cards;
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
