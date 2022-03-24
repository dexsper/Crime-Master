
using System.Collections.Generic;
using UnityEngine;

public enum CardTier
{
    One,
    Two,
    Three,
    Four,
    Item
}



[CreateAssetMenu( menuName = "Crime/Card", fileName = "New Card")]
public class CardInfo : ScriptableObject, IStats
{

    [Header("Main")]
    [SerializeField] private int _cost;
    [SerializeField] private CardTier _tier;

    [Header("Abilities")]
    [Range(1, 30)]
    [SerializeField] private int _firePower;
    [Range(1, 30)]
    [SerializeField] private int _hackerPower;
    [Range(1, 30)]
    [SerializeField] private int _horrifyPower;

    [Header("UI")]
    [SerializeField] private Sprite _iconSprite;

    public int Cost => _cost;
    public int FirePower => _firePower;
    public int HackerPower => _hackerPower;
    public int HorrifyPower => _horrifyPower;
    public Sprite IconSprite => _iconSprite;

    public CardTier Tier => _tier;

    public static Dictionary<CardTier, Color> TierColors = new Dictionary<CardTier, Color>
    {
        {CardTier.One, new Color32(191, 205, 208, 255) },
        {CardTier.Two, new Color32(113, 198, 217, 255) },
        {CardTier.Three, new Color32(225, 166, 77, 255) },
        {CardTier.Four, new Color32(212, 99, 99, 255) },
        {CardTier.Item, new Color32(66, 211, 81, 255) }
    };
}