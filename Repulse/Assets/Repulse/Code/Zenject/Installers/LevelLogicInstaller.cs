using UnityEngine;
using Zenject;

public class LevelLogicInstaller : MonoInstaller
{
    [SerializeField] private Level _level;

    [SerializeField] private Timer _timer;

    public override void InstallBindings()
    {
        BindDifficultLevelConfig();

        BindLevel();

        BindTimer();
    }

    private void BindDifficultLevelConfig()
    {
        if (SelectedConfigHolder.SelectedConfig != null)
        {
            Container.Bind<DifficultyLevelConfig>()
                .FromInstance(SelectedConfigHolder.SelectedConfig)
                .AsSingle();
        }
    }

    private void BindLevel()
    {
        Container.Bind<Level>().FromInstance(_level).AsSingle();
    }

    private void BindTimer()
    {
        Container.Bind<Timer>().FromInstance(_timer).AsSingle();
    }
}