using UnityEngine;
using Zenject;

public class RobberyInstaller : MonoInstaller
{
    [SerializeField] private Robbery _robberyPanel;

    public override void InstallBindings()
    {
        Container.Bind<Robbery>().FromInstance(_robberyPanel).AsSingle();
    }
}
