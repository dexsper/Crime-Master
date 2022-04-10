using UnityEngine;
using Zenject;

public class CameraControllerInstaller : MonoInstaller
{
    [SerializeField] private CameraController _camController;

    public override void InstallBindings()
    {
        Container.Bind<CameraController>().FromInstance(_camController).AsSingle();
    }
}
