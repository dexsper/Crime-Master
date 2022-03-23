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
    [Header("Cards")]
    [Range(5, 20)]
    [SerializeField] private float _cardSpace = 10f;
    [SerializeField] private List<CardSpawn> _cards;

    [Header("Track")]
    [Range(1, 10)]
    [SerializeField] private float _trackScale = 1f;

    public List<CardSpawn> Cards => _cards;
    public float CardSpace => _cardSpace;
    public float TrackScale => _trackScale;
}
