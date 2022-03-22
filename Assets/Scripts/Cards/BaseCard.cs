using UnityEngine;
using Zenject;

public abstract class BaseCard<T> : MonoBehaviour
{
    [Inject]
    protected Player _player;

    protected abstract void Use();

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player.gameObject)
            Use();
    }

    public abstract void Setup(T info);
}
