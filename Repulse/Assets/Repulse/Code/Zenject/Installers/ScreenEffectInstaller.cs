using UnityEngine;
using Zenject;

public class ScreenEffectInstaller : MonoInstaller
{
    [SerializeField] private ScreenEffectController _edgeGlowController;

    public override void InstallBindings()
    {
        BindGlowController();

        BindScreenEffectMediator();
    }

    private void BindGlowController()
    {
        Container.Bind<ScreenEffectController>().FromInstance(_edgeGlowController).AsSingle();
    }

    private void BindScreenEffectMediator()
    {
        Container.BindInterfacesAndSelfTo<ScreenEffectMediator>().AsSingle();
    }
}
