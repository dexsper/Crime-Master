using System;
using UnityEngine;
using Zenject;

public class CardSpawner : MonoBehaviour
{
    [Header("Main")]
    [Range(0, 5)]
    [SerializeField] private float _horizontalSpacing = 1f;

    [Header("Transforms")]
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;
    
    [Header("Prefabs")]
    [SerializeField] private Card _cardPrefab;

    [Header("Debug")]
    [SerializeField] private bool _debug;
    [SerializeField] private float _sphereSize = .2f;
    [SerializeField] private Color _lineColor = Color.white;
    [SerializeField] private Color _startColor = Color.green;
    [SerializeField] private Color _endColor = Color.red;

    [Inject]
    private LevelManager _levelManager;

    [Inject]
    private DiContainer _container;

    private void Awake()
    {
        _levelManager.OnLevelChanged.AddListener(SpawnCards);
    }

    private void SpawnCards(Level level)
    {

        for (int i = 0; i < level.Cards.Count; i++)
        {
            var cardSpawn = level.Cards[i];

            Vector3 position = _start.position;
            position.z += level.CardSpace * i;
            position.x = _horizontalSpacing;

            if (position.z > _end.position.z)
                continue;

            SpawnCard(cardSpawn.CardOne, position);

            position.x = -position.x;

            SpawnCard(cardSpawn.CardTwo, position);
        }
    }

    private void SpawnCard(CardInfo info, Vector3 position)
    {
        var card = _container.InstantiatePrefabForComponent<Card>(_cardPrefab.gameObject);

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
