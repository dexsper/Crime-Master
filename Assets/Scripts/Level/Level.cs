using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardSpawn
{
    public CardInfo CardOne;
    public CardInfo CardTwo;

}

public enum PlacesType
{
    Three,
    Five,
    Seven
}

[CreateAssetMenu(fileName = "New Level", menuName = "Crime/Level")]
public class Level : ScriptableObject
{
    [Header("Cards")]
    [Range(10, 20)]
    [SerializeField] private float _cardSpace = 10f;
    [SerializeField] private List<CardSpawn> _cards;

    [Header("Money")]
    [Range(10, 30)]
    [SerializeField] private float _moneySpace = 5f;

    [Header("Robbery")]
    [SerializeField] private PlacesType _places;

    public List<CardSpawn> Cards => _cards;
    public float CardSpace => _cardSpace;
    public float MoneySpace => _moneySpace;
    public PlacesType Places => _places;
}
