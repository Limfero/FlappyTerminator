using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _direction;

    private ObjectPool _pool;
    private GameObject _sender;

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * _direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() && _sender.TryGetComponent(out Bird bird))
            bird.AddScore();

        _pool.PutObject(gameObject);
    }

    public void SetPool(ObjectPool pool) => _pool = pool;

    public void SetSender(GameObject sender) => _sender = sender;
}
