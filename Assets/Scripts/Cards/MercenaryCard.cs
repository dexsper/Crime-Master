using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MercenaryCard : BaseCard<Mercenary>
{
    [SerializeField] private TextMeshProUGUI _fireText;
    [SerializeField] private TextMeshProUGUI _hackerText;
    [SerializeField] private TextMeshProUGUI _horrifyText;
    [SerializeField] private Image _iconImage;

    private Mercenary _mercenary;

    public override void Setup(Mercenary info)
    {
       _mercenary = info;

        _fireText.text = _mercenary.FirePower.ToString();
        _hackerText.text = _mercenary.HackerPower.ToString();
        _horrifyText.text = _mercenary.HackerPower.ToString();
    }

    protected override void Use()
    {
        
    }
}
