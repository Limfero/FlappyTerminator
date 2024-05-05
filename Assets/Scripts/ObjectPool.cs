using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject[] _prefabs;

    private Queue<GameObject> _pool;

    private List<GameObject> _activeObjects = new();

    public IEnumerable<GameObject> PooledObjects => _pool;

    private void Awake()
    {
        _pool = new Queue<GameObject>();
    }

    public GameObject GetObject()
    {
        if (_pool.Count == 0)
        {
            var gameObject = Instantiate(_prefabs[Random.Range(0, _prefabs.Length)]);
            gameObject.transform.parent = _container;
            _activeObjects.Add(gameObject);

            return gameObject;
        }

        return _pool.Dequeue();
    }

    public void PutObject(GameObject gameObject)
    {
        _pool.Enqueue(gameObject);
        gameObject.gameObject.SetActive(false);
    }

    public void Reset()
    {
        _activeObjects.ForEach(gameObject => Destroy(gameObject));
        _pool.Clear();
    }
}
