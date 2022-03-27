using UnityEngine;
using Zenject;

public class FinalScreenInstaller : MonoInstaller
{
    [SerializeField] private FinalScreen _finalScreen;

    public override void InstallBindings()
    {
        Container.Bind<FinalScreen>().FromInstance(_finalScreen).AsSingle();
    }
}
