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

        if(collision.GetComponent<Bullet>() == false)
            _pool.PutObject(gameObject);
    }

    public void Init(ObjectPool pool, GameObject sender)
    {
        _pool = pool;
        _sender = sender;
    }
}