
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



[CreateAssetMenu(menuName = "Crime/Card", fileName = "New Card")]
public class CardInfo : ScriptableObject, IStats
{

    [Header("Main")]
    [SerializeField] private string _name;
    [SerializeField] private int _cost;
    [SerializeField] private CardTier _tier;

    [Header("Abilities")]
    [Range(1, 30)]
    [SerializeField] private int _power;
    [Range(1, 30)]
    [SerializeField] private int _terrifying;
    [Range(1, 30)]
    [SerializeField] private int _intellect;


    [Header("UI")]
    [SerializeField] private Sprite _iconSprite;

    public string Name => _name;
    public int Cost => _cost;
    public Sprite IconSprite => _iconSprite;
    public int Terrifying => _terrifying;
    public int Power => _power;
    public int Intellect { get => _intellect; }

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