using UnityEngine;
using Zenject;

public class CardSpawner : BaseSpawner
{
    [Header("Main")]
    [Range(0, 5)]
    [SerializeField] protected float _horizontalSpacing = 1f;

    [Header("Prefabs")]
    [SerializeField] private WorldCard _cardPrefab;

    protected override void Spawn(Level level)
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
        var card = _container.InstantiatePrefabForComponent<WorldCard>(_cardPrefab.gameObject);

        card.transform.position = position;
        card.Setup(info);

        card.gameObject.SetActive(true);
    }

}
