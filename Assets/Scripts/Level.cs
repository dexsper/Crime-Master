using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardSpawn
{
    public CardInfo CardOne;
    public CardInfo CardTwo;

}
[CreateAssetMenu(fileName = "New Level", menuName = "Crime/Level")]
public class Level : ScriptableObject
{
    [Range(5, 20)]
    [SerializeField] private float _cardSpace = 10f;

    [SerializeField] private List<CardSpawn> _cardSpawns;

    public List<CardSpawn> CardSpawns => _cardSpawns;
    public float CardSpace => _cardSpace;
}
