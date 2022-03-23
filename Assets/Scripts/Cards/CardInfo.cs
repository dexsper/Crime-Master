
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
public class CardInfo : ScriptableObject, ICharacterStats
{

    [Header("Main")]
    [SerializeField] private int _cost;
    [SerializeField] private CardTier _tier;

    [Header("Abilities")]
    [SerializeField] private int _firePower;
    [SerializeField] private int _hackerPower;
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
        {CardTier.One, new Color(191, 205, 208) },
        {CardTier.Two, new Color(113, 198, 217) },
        {CardTier.Three, new Color(225, 166, 77) },
        {CardTier.Four, new Color(212, 99, 99) },
        {CardTier.Item, new Color(66, 211, 81) }
    };
}