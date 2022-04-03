using UnityEngine;
using Zenject;

public class StartScreenInstaller : MonoInstaller
{
    [SerializeField] private LevelStart _levelScreen;

    public override void InstallBindings()
    {
        Container.Bind<LevelStart>().FromInstance(_levelScreen).AsSingle();
    }
}
