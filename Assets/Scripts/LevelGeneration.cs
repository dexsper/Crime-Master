using System;
using UnityEngine;
using Zenject;

public class LevelGeneration : MonoBehaviour
{
    [Header("Card Settings")]
    [SerializeField] private Vector2 _cardSpacing;
    [SerializeField] private WorldCard _cardPrefab;
    [Range(1, 20)]
    [SerializeField] private int _moneyAlteration = 5;
    [Range(1, 5)]
    [SerializeField] private float _cardYOffset = 1;

    [Header("Money Settings")]
    [SerializeField] private GameObject _moneyPrefab;
    [SerializeField] private Vector2 _moneySpacing;
    [Range(0, 5)]
    [SerializeField] private float _moneyYOffset = 1;

    [Header("Transforms")]
    [SerializeField] protected Transform _start;
    [SerializeField] protected Transform _end;

    [Header("Debug")]
    [SerializeField] protected bool _debug;
    [SerializeField] protected float _sphereSize = .2f;
    [SerializeField] protected Color _lineColor = Color.white;
    [SerializeField] protected Color _startColor = Color.green;
    [SerializeField] protected Color _endColor = Color.red;

    [Inject]
    protected DiContainer _container;

    [Inject]
    private LevelManager _levelManager;
    private void Awake()
    {
        _levelManager.LevelChanged.AddListener(Generate);
    }

    private void Generate(Level level)
    {
        int count = (int)(Vector3.Distance(_start.position, _end.position) / _moneySpacing.y);
        var cards = _levelManager.CurrentLevel.Cards;
        int currentCard = 0;

        Vector3 pos = _start.position;

        for (int i = 0; i < count; i++)
        {

            if ((i > 0 && i % _moneyAlteration == 0) && currentCard < cards.Count)
            {
                var card = cards[currentCard];
                currentCard++;

                pos.z += _cardSpacing.y;
                pos.x = _cardSpacing.x;
                pos.y = _cardYOffset;

                SpawnCard(card.CardOne, pos);

                pos.x = -pos.x;

                SpawnCard(card.CardTwo, pos);
            }
            else
            {
                pos.z += _moneySpacing.y;
                pos.x = _moneySpacing.x;
                pos.y = _moneyYOffset;

                SpawnMoney(pos);

                pos.x = -pos.x;

                SpawnMoney(pos);
            }
        }
    }

    private void SpawnMoney(Vector3 pos)
    {
        _container.InstantiatePrefab(_moneyPrefab, pos, Quaternion.identity, transform);
    }

    private void SpawnCard(CardInfo info, Vector3 position)
    {
        var card = _container.InstantiatePrefabForComponent<WorldCard>(_cardPrefab.gameObject);

        card.transform.position = position;
        card.Setup(info);

        card.gameObject.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        if (_start != null && _end != null && _debug)
        {
            Debug.DrawLine(_start.position, _end.position, _lineColor);

            Gizmos.color = _startColor;
            Gizmos.DrawSphere(_start.position, _sphereSize);

            Gizmos.color = _endColor;
            Gizmos.DrawSphere(_end.position, _sphereSize);
        }
    }
}
