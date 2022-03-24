using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Robbery : MonoBehaviour
{
    [Header("Places Groups")]
    [SerializeField] private List<CardPlace> _allPlaces;

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

    private bool isStart = false;

    private void Awake()
    {
        SetupPlaces();
        SetupCards();

        if(_startButton != null)
        {
            _startButton.onClick.AddListener(StartRobbery);
        }
    }
    private void StartRobbery()
    {
        isStart = true;
        _startButton.interactable = false;

        float randomChange = Random.Range(0, 1f);

        if(Chance >= randomChange)
        {
            Debug.Log("Win");
        }
        else
        {
            Debug.Log("Lose");
        }
    }
    private void SetupCards()
    {
        for (int i = 0; i < _cardsParent.childCount; i++)
        {
            Destroy(_cardsParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < _playerInventory.Cards.Count; i++)
        {
            var card = Instantiate(_cardPrefab);
            card.Setup(_playerInventory.Cards[i]);

            card.transform.SetParent(_cardsParent);
        }
    }
    private void SetupPlaces()
    {
        for (int i = 0; i < _allPlaces.Count; i++)
        {
            _allPlaces[i].gameObject.SetActive(false);
        }

        _places.Clear();

        for (int i = 0; i < _levelManager.CurrentLevel.Places.Length; i++)
        {
            if (i > _allPlaces.Count) break;

            _places.Add(_allPlaces[i]);

            var place = _places[i];
            place.ResetInfo();

            place.Setup(_levelManager.CurrentLevel.Places[i]);

            place.gameObject.SetActive(true);
        }
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
