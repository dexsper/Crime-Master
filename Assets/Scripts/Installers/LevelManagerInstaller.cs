using Zenject;
using UnityEngine;

public class LevelManagerInstaller : MonoInstaller
{
    [SerializeField] private LevelManager _levelManager;
    public override void InstallBindings()
    {
        Container.Bind<LevelManager>().
            FromInstance(_levelManager).
            AsSingle().
            NonLazy();
    }
}
