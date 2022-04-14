using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class Robbery : MonoBehaviour
{
    [SerializeField] private CityMarkers _cityMarkers;

    [Header("Interface")]
    [SerializeField] private Transform _placesParent;

    [Header("Visual Settings")]
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private Button _startButton;

    [Header("Animation")]
    [Range(1, 5)]
    [SerializeField] private float _animationTime = 2f;
    [SerializeField] private List<GameObject> _animationObjects;

    [Header("Cards")]
    [SerializeField] private UI_Card _cardPrefab;
    [SerializeField] private Transform _cardsParent;

    [Header("Events")]
    public UnityEvent OnAnimation;
    public UnityEvent OnLose;
    public UnityEvent OnSuccess;

    public float Chance { get; private set; }

    [Inject]
    private LevelManager _levelManager;

    [Inject]
    private PlayerInventory _playerInventory;

    [Inject]
    private FinalScreen _finalScreen;

    [Inject]
    private DiContainer _container;

    [Inject]
    private CameraController _cameraController;

    private List<CardPlace> _places = new List<CardPlace>();
    private bool isStart = false;

    private void Awake()
    {
        if (_startButton != null)
        {
            _startButton.onClick.AddListener(StartRobbery);
        }
    }
    private void OnEnable()
    {
        SetChildsEnable(true);

        SetupPlaces();
        SetupCards();
        UpdateChance();

    }

    private void OnDisable()
    {
        isStart = false;

        for (int i = 0; i < _places.Count; i++)
        {
            _places[i].OnChanceChanged.RemoveAllListeners();
        }

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

    private void StartRobbery()
    {
        isStart = true;
        _startButton.interactable = false;

        StartCoroutine(RobberyAnimation());
    }

    private IEnumerator RobberyAnimation()
    {
        float rndChance = Random.Range(0, 1f);

        bool win = false;

        if (Chance >= rndChance)
            win = true;

        SetChildsEnable(false);
        _progressBar.enabled = true;

        for (int i = 0; i < _animationObjects.Count; i++)
        {
            _animationObjects[i].SetActive(true);
        }

        OnAnimation?.Invoke();

        yield return new WaitForSeconds(_animationTime);

        _cameraController.ShowCity();

        if (win)
        {
            _finalScreen.ShowSuccess();
        }
        else
        {
            _finalScreen.ShowLose();
        }

        gameObject.SetActive(false);

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

            place.OnChanceChanged.AddListener(UpdateChance);
            place.OnChanceChanged.AddListener(UpdateStartButton);
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

        if (_startButton.interactable == enabled) return;

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

        if (_progressBar != null)
        {
            _progressBar.SetProgress(Chance);
        }
    }


    private void SetChildsEnable(bool enable)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(enable);
        }
    }
}
