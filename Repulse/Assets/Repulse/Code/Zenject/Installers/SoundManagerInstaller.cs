using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SoundManagerInstaller : MonoInstaller
{
    [SerializeField] private SoundManager _sceneManager;

    public override void InstallBindings()
    {
        BindSceneManager();
    }

    private void BindSceneManager()
    {
        Container.Bind<SoundManager>().FromInstance(_sceneManager).AsSingle();
    }

}
