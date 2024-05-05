using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private float _lowerBound;
    [SerializeField] private float _upperBound;
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private ObjectPool _bulletsPool;

    private void Start()
    {
        StartCoroutine(GeneratePipes());
    }

    private IEnumerator GeneratePipes()
    {
        var wait = new WaitForSeconds(_repeatRate);

        while (enabled)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        float spawnPositionY = Random.Range(_upperBound, _lowerBound);
        Vector3 spawnPoint = new(transform.position.x, spawnPositionY, transform.position.z);

        var enemy = _pool.GetObject().GetComponent<Enemy>();
        enemy.SetPool(_pool);
        enemy.GetComponent<Attaker>().SetPool(_bulletsPool);

        enemy.gameObject.SetActive(true);
        enemy.transform.position = spawnPoint;
    }
}
