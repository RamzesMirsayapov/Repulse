using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 1.5f;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }
}
