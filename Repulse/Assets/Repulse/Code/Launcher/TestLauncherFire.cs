using UnityEngine;

public class TestLauncherFire : MonoBehaviour
{
    [SerializeField] private Transform _missileSpawnPoint;
    [SerializeField] private GameObject _missile;

    private MissileCreator missileCreator;

    [SerializeField] private float _missileSpeed = 40;

    private void Start()
    {
        //missileCreator = new HomingMissileCreator();
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.C))
        {
            Missile missile = missileCreator.CreateMissile(_missileSpeed, _missileSpawnPoint);

            //missile.transform.rotation = _missileSpawnPoint.rotation;
            //missile.transform.position = _missileSpawnPoint.position;
            //Instantiate(_missile, _missileSpawnPoint.position, gameObject.transform.rotation);
        }
    }
}
