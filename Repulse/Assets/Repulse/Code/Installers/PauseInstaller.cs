using Zenject;

public class PauseInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindPauseManager();
        BindPausePanelMediator();
    }

    private void BindPauseManager()
    {
        Container.BindInterfacesAndSelfTo<PauseManager>().AsSingle();
    }

    private void BindPausePanelMediator()
    {
        Container.Bind<PausePanelMediator>().AsSingle();
    }
}
