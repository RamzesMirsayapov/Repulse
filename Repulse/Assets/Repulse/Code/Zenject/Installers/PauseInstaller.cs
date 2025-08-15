using UnityEngine;
using Zenject;

public class PauseInstaller : MonoInstaller
{
    [SerializeField] private UIPausePanel _pausePanel;

    public override void InstallBindings()
    {
        BindPauseManager();
        BindPausePanel();
        BindPausePanelMediator();
    }

    private void BindPauseManager()
    {
        Container.BindInterfacesAndSelfTo<PauseManager>().AsSingle();
    }

    private void BindPausePanel()
    {
        Container.Bind<UIPausePanel>().FromInstance(_pausePanel).AsSingle();
    }

    private void BindPausePanelMediator()
    {
        Container.BindInterfacesAndSelfTo<PausePanelMediator>().AsSingle();
    }
}
