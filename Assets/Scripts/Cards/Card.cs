using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public partial class Card : MonoBehaviour
{
    [Inject]
    private Player _player;

    [SerializeField] private TextMeshProUGUI _fireText;
    [SerializeField] private TextMeshProUGUI _hackerText;
    [SerializeField] private TextMeshProUGUI _horrifyText;
    [SerializeField] private TextMeshProUGUI _priceText;

    [SerializeField] private Image _iconImage;

    private CardInfo _info;

    public void Setup(CardInfo info)
    {
        _info = info;

        if (_fireText != null)
            _fireText.text = _info.FirePower.ToString();
        if (_hackerText != null)
            _hackerText.text = _info.HackerPower.ToString();
        if (_horrifyText != null)
            _horrifyText.text = _info.HackerPower.ToString();
        if (_priceText != null)
            _priceText.text = _info.Price.ToString();
        if (_iconImage != null)
            _iconImage.sprite = _info.IconSprite;
    }

    public void Use()
    {
        _player.PlayerCards.AddCard(_info);
        Destroy(gameObject);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player.gameObject)
        {
            Use();
        }
    }
}


public partial class Card
{

    [CustomEditor(typeof(Card))]
    public class CardsEditor : Editor
    {
        private Card _card;
        private bool _isItem;

        private float labelWidth = 150f;

        private void OnEnable()
        {
            _card = target as Card;
        }

        public override void OnInspectorGUI()
        {
            GUILayout.Space(20f);
            GUILayout.Label("Card settings", EditorStyles.boldLabel);

            GUILayout.Space(10f);

            _isItem = EditorGUILayout.Toggle("Is Item", _isItem);

            if (_isItem == false)
            {
                GUILayout.Label("Player Preferences");

                _card._fireText = EditorGUILayout.ObjectField("Fire", _card._fireText, typeof(TextMeshProUGUI), true) as TextMeshProUGUI;

                _card._hackerText = EditorGUILayout.ObjectField("Hacker", _card._hackerText, typeof(TextMeshProUGUI), true) as TextMeshProUGUI;

                _card._horrifyText = EditorGUILayout.ObjectField("Horrify", _card._horrifyText, typeof(TextMeshProUGUI), true) as TextMeshProUGUI;

                _card._priceText = EditorGUILayout.ObjectField("Price text component", _card._priceText, typeof(TextMeshProUGUI), true) as TextMeshProUGUI;
            }

            GUILayout.Space(10f);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Card image component", GUILayout.Width(labelWidth));
            _card._iconImage = EditorGUILayout.ObjectField(_card._iconImage, typeof(Image), true) as Image;
            GUILayout.EndHorizontal();

            base.serializedObject.ApplyModifiedProperties();
        }
    }
}
