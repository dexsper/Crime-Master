using TMPro;
using UnityEngine;
using Zenject;

public class UI_Money : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textComponent;

    [Inject]
    private Player _player;

    private void Update()
    {
        _textComponent.text = _player.Economics.Money.ToString();
    }
}
