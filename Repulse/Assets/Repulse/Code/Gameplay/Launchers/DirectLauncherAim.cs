using UnityEngine;
using Zenject;

public class DirectLauncherAim : MonoBehaviour
{
    [SerializeField] private GameObject[] _launchers;

    private Player _player;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    private void Update()
    {
        foreach (var launcher in _launchers)
        {
            Vector3 direction = _player.PlayerPosition - launcher.transform.position;

            launcher.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
