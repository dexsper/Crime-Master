using UnityEngine;

public class MoneySpawner : BaseSpawner
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _moneyPrefab;


    public override void Spawn(Level level)
    {
        int count = (int)(Vector3.Distance(_start.position, _end.position) / level.MoneySpace);

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = _start.position;
            pos.z += level.MoneySpace * i;

            _container.InstantiatePrefab(_moneyPrefab, pos,  Quaternion.identity, transform);
        }
    }
}
