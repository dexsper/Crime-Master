using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Robbery : MonoBehaviour
{
    [SerializeField] private Transform _placesParent;

    [Header("Visual Settings")]
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private Button _startButton;

    [Header("Cards")]
    [SerializeField] private UI_Card _cardPrefab;
    [SerializeField] private Transform _cardsParent;

    public float Chance { get; private set; }

    private List<CardPlace> _places = new List<CardPlace>();

    [Inject]
    private LevelManager _levelManager;

    [Inject]
    private PlayerInventory _playerInventory;

    [Inject]
    private FinalScreen _finalScreen;

    [Inject]
    private DiContainer _container;

    private bool isStart = false;

    private void Awake()
    {
        if(_startButton != null)
        {
            _startButton.onClick.AddListener(StartRobbery);
        }
    }
    private void OnEnable()
    {
        SetupPlaces();
        SetupCards();
    }
    private void OnDisable()
    {
        isStart = false;

        _places.Clear();

        for (int i = 0; i < _placesParent.childCount; i++)
        {
            Destroy(_placesParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < _cardsParent.childCount; i++)
        {
            Destroy(_cardsParent.GetChild(i).gameObject);
        }


        _startButton.interactable = true;
    }
    private void Update()
    {
        if (isStart == false)
        {
            UpdateChance();

            if (_progressBar != null)
            {
                _progressBar.SetProgress(Chance);
            }

            UpdateStartButton();
        }
    }

    private void StartRobbery()
    {
        isStart = true;
        _startButton.interactable = false;

        float randomChange = Random.Range(0, 1f);

        if(Chance >= randomChange)
        {
            _finalScreen.ShowSuccess();
            gameObject.SetActive(false);
        }
        else
        {
            _finalScreen.ShowLose();
            gameObject.SetActive(false);
        }
    }
    private void SetupCards()
    {
        for (int i = 0; i < _playerInventory.Cards.Count; i++)
        {
            var card = Instantiate(_cardPrefab);
            card.Setup(_playerInventory.Cards[i]);

            card.transform.SetParent(_cardsParent);
        }
    }
    private void SetupPlaces()
    {
        var placeField = _container.InstantiatePrefab(_levelManager.CurrentLevel.PlacesField, _placesParent);

        var places = placeField.GetComponentsInChildren<CardPlace>();

        for (int i = 0; i < places.Length; i++)
        {      
            var place = places[i];

            place.ResetInfo();
            place.gameObject.SetActive(true);

            _places.Add(place);
        }
    }
    private void UpdateStartButton()
    {
        if (_startButton == null) return;

        bool enabled = false;

        for (int i = 0; i < _places.Count; i++)
        {
            if (_places[i].HasCard)
            {
                enabled = true;
                break;
            }
        }

        _startButton.interactable = enabled;
    }
    private void UpdateChance()
    {
        float chance = 0f;

        if (_places.Count > 0)
        {
            for (int i = 0; i < _places.Count; i++)
            {
                chance += _places[i].Chance;
            }

            chance = Mathf.Clamp01(chance / _places.Count);
        }

        Chance = chance;
    }

}
