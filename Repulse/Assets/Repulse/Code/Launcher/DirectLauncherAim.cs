using UnityEngine;
using Zenject;

public class DirectLauncherAim : MonoBehaviour
{
    [SerializeField] private GameObject[] _launchers;

    private PlayerMovement _playerMovement;

    [Inject]
    private void Construct(PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
    }

    private void Start()
    {

    }

    private void Update()
    {
        foreach (var launcher in _launchers)
        {
            Vector3 direction = _playerMovement.transform.position - launcher.transform.position;

            launcher.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
