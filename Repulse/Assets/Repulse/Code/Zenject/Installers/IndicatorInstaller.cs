using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class IndicatorInstaller : MonoInstaller
{
    [SerializeField] private IndicatorSystem _indicatorSystem;

    public override void InstallBindings()
    {
        BintIndicatorSystem();
    }

    private void BintIndicatorSystem()
    {
        Container.Bind<IndicatorSystem>().FromInstance(_indicatorSystem).AsSingle();
    }
}
