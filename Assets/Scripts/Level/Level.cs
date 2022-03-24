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

[System.Serializable]
public class PlaceInfo : IStats
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
}


[CreateAssetMenu(fileName = "New Level", menuName = "Crime/Level")]
public partial class Level : ScriptableObject
{
    [Header("Places")]
    [SerializeField] private PlacesType _placesType = PlacesType.Three;
    [SerializeField] private PlaceInfo[] _places = new PlaceInfo[3];

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

    public PlaceInfo[] Places => _places;

    private void OnValidate()
    {
        int size = (int)_placesType;

        if (_places.Length != size)
        {
            Array.Resize(ref _places, size);
        }
    }

    public enum PlacesType
    {
        Three = 3,
        Five = 5,
        Seven = 7
    }
}
