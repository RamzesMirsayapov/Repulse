using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private PointDisplayer _pointDisplayer;

    [SerializeField] private PointerIcon _pointerIcon;  //ну будущее

    public override void InstallBindings()
    {
        BindPointDisplayer();

        BindPointerIcon();
    }

    private void BindPointDisplayer()
    {
        Container.Bind<PointDisplayer>().FromInstance(_pointDisplayer).AsSingle();
    }

    private void BindPointerIcon()
    {
        Container.Bind<PointerIcon>().FromInstance(_pointerIcon).AsSingle();
    }
}