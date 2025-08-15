using Zenject;

public class MissileInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindHomingMissileCreator();
        BindDirectMissileCreator();
        BindBallisticMissile();
    }

    private void BindHomingMissileCreator()
    {
        Container.
            Bind<HomingMissileCreator>().
            AsSingle();

        Container.
            Bind<DecoyHomingMissileCreator>().
            AsSingle();
    }

    private void BindDirectMissileCreator()
    {
        Container.
            Bind<DirectMissileCreator>().
            AsSingle();

        Container.
            Bind<DecoyDirectMissileCreator>().
            AsSingle();
    }

    private void BindBallisticMissile()
    {
        Container.
            Bind<BallisticMissileCreator>().
            AsSingle();

        Container.
            Bind<FlightCalculationBezier>().
            FromNew().
            AsTransient();

        Container.
            Bind<SettingsCalculationBezier>().
            FromComponentInParents().
            AsTransient();
    }
}
