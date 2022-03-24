using UnityEngine;
using Zenject;

public class PlayerDataInstaller : MonoInstaller
{
    [SerializeField] private PlayerEconomics _playerEconomics;
    [SerializeField] private PlayerInventory _playerInventory;


    public override void InstallBindings()
    {
        Container.Bind<PlayerEconomics>().FromInstance(_playerEconomics).AsSingle().NonLazy();
        Container.Bind<PlayerInventory>().FromInstance(_playerInventory).AsSingle().NonLazy();
    }
}
