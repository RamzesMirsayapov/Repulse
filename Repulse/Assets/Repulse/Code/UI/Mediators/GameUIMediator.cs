using UnityEngine;
using Zenject;
using TMPro;

public class GameUIMediator : MonoInstaller
{
    [SerializeField] private TMP_Text _timerText;

    public override void InstallBindings()
    {
        
    }
}
