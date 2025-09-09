using UnityEngine;
using Zenject;

public class VictoryPanelInstaller : MonoInstaller
{
    [SerializeField] private UIVictoryPanel _victoryPanel;

    public override void InstallBindings()
    {
        BindVictoryPanel();
        BindVictoryPanelMediator();
    }
    private void BindVictoryPanel()
    {
        Container.Bind<UIVictoryPanel>().FromInstance(_victoryPanel).AsSingle();
    }

    private void BindVictoryPanelMediator()
    {
        Container.BindInterfacesAndSelfTo<VictoryPanelMediator>().AsSingle();
    }
}