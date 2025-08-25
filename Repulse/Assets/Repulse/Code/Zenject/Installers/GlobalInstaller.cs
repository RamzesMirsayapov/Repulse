using System;
using Zenject;
using UnityEngine;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField] private SoundManager _soundManager;

    public override void InstallBindings()
    {
        BindInput();

        BindSoundManager();
    }

    private void BindInput()
    {
        Container.BindInterfacesTo<DesktopInput>().AsSingle();
    }

    private void BindSoundManager()
    {
        Container.Bind<SoundManager>().FromInstance(_soundManager).AsSingle();
    }
}
