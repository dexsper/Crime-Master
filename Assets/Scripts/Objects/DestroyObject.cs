using UnityEngine;
using Zenject;

public class DestroyObject : MonoBehaviour
{
    [Header("Destroy Settings")]
    [SerializeField] private bool _destroyEnabled = true;
    [Range(5, 20)]
    [SerializeField] private float _destroyDistance = 7f;

    [Inject]
    protected Player _player;

    protected virtual void Update()
    {
        CheckDestroyDistance();
    }

    private void CheckDestroyDistance()
    {
        if (_destroyEnabled == false) return;

        float distance = _player.transform.position.z - transform.position.z;

        if (distance >= _destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
