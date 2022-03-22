
using UnityEngine;

[CreateAssetMenu( menuName = "Characters/Mercenary", fileName = "New Mercenary")]
public class Mercenary : ScriptableObject, ICharacterStats
{

    [Header("Main")]
    [SerializeField] private int _price;

    [Header("Abilities")]
    [SerializeField] private int _firePower;
    [SerializeField] private int _hackerPower;
    [SerializeField] private int _horrifyPower;

    [Header("UI")]
    [SerializeField] private Sprite _iconSprite;

    public int Price => _price;
    public int FirePower => _firePower;
    public int HackerPower => _hackerPower;
    public int HorrifyPower => _horrifyPower;
    public Sprite IconSprite => _iconSprite;
}