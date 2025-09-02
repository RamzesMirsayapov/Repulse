using UnityEngine;
using Zenject;

public class DefeatPanelInstaller : MonoInstaller
{
    [SerializeField] private UIDefeatPanel _defeatPanel;

    public override void InstallBindings()
    {
        BindDefeatPanel();
        BindDefeatPanelMediator();
    }
    private void BindDefeatPanel()
    {
        Container.Bind<UIDefeatPanel>().FromInstance(_defeatPanel).AsSingle();
    }

    private void BindDefeatPanelMediator()
    {
        Container.BindInterfacesAndSelfTo<DefeatPanelMediator>().AsSingle();
    }
}
