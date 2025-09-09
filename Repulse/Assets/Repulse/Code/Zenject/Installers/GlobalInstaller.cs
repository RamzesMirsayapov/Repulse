using Zenject;
using UnityEngine;
using YG;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private SoundManager _soundManager;

    public override void InstallBindings()
    {
        BindInput();

        BindSoundManager();

        ActivateStickyADV();
    }

    private void BindInput()
    {
        Container.BindInterfacesTo<DesktopInput>().AsSingle();
    }

    private void BindSoundManager()
    {
        Container.Bind<SoundManager>().FromInstance(_soundManager).AsSingle();
    }

    private void ActivateStickyADV()
    {
        YG2.StickyAdActivity(true);
    }
}