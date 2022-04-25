using DG.Tweening;
using UnityEngine;

public class UI_RobberyCards : MonoBehaviour
{
    [Header("Abilities")]
    [SerializeField] private Ability _power;
    [SerializeField] private Ability _terrifying;
    [SerializeField] private Ability _intelect;

    private CanvasGroup _canvasGroup;
    private Robbery _robbery;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _robbery = GetComponentInParent<Robbery>();

        _robbery.OnChanceChanged.AddListener(UpdateUI);

    }

    private void OnEnable()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        var places = _robbery.Places;

        int power = 0;
        int terrifying = 0;
        int intelect = 0;

        int requiredPower = 0;
        int requiredTerrifying = 0;
        int requiredIntelect = 0;


        for (int i = 0; i < places.Count; i++)
        {
            CardPlace cardPlace = places[i];

            if(cardPlace.CardInfo != null)
            {
                power += cardPlace.CardInfo.Power;
                intelect += cardPlace.CardInfo.Intellect;
                terrifying += cardPlace.CardInfo.Terrifying;
            }

            requiredIntelect += cardPlace.Intellect;
            requiredTerrifying += cardPlace.Terrifying;
            requiredPower += cardPlace.Power;
        }

        float powerValue = Mathf.Clamp01((float)power / (float)requiredPower);
        float intelectValue = Mathf.Clamp01((float)intelect / (float)requiredIntelect);
        float terrifyingValue = Mathf.Clamp01((float)terrifying / (float)requiredTerrifying);

        _power.Text.text = $"Team power: {power} / {requiredPower}";
        _terrifying.Text.text = $"Team terrifying: {terrifying} / {requiredTerrifying}";
        _intelect.Text.text = $"Team intelect: {intelect} / {requiredIntelect}";

        _power.Image.DOFillAmount(powerValue, 0.3f).SetEase(Ease.OutBack);
        _terrifying.Image.DOFillAmount(intelectValue, 0.3f).SetEase(Ease.OutBack);
        _intelect.Image.DOFillAmount(terrifyingValue, 0.3f).SetEase(Ease.OutBack);

        _power.Image.transform.parent.gameObject.SetActive(requiredPower > 0);
        _terrifying.Image.transform.parent.gameObject.SetActive(requiredTerrifying > 0);
        _intelect.Image.transform.parent.gameObject.SetActive(requiredIntelect > 0);


    }
}
