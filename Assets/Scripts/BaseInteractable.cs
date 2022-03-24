using UnityEngine;
using Zenject;

public abstract class BaseInteractable : DestroyObject
{
    public abstract void Use();

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _player.gameObject)
        {
            Use();
        }
    }
}
