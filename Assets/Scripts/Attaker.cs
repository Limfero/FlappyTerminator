using System.Collections;
using UnityEngine;

public class Attaker : MonoBehaviour
{
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private float _reloadTime;
    [SerializeField] private Transform _spawnPoint;

    private bool _canAttack = false;
    private Coroutine _coroutine;

    public bool CanAttack => _canAttack;

    private void OnEnable()
    {
        _coroutine = StartCoroutine(Reload());
    }

    private void OnDisable()
    {
        StopCoroutine(_coroutine);
    }

    private void Start()
    {
        _coroutine = StartCoroutine(Reload());
    }

    public void SetPool(ObjectPool pool) => _pool = pool;

    public void Attack()
    {
        if (_canAttack)
        {
            _canAttack = false;

            Bullet bullet = _pool.GetObject().GetComponent<Bullet>();
            bullet.SetPool(_pool);
            bullet.SetSender(gameObject);

            bullet.gameObject.SetActive(true);
            bullet.transform.SetPositionAndRotation(_spawnPoint.position, transform.rotation);

            _coroutine = StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(_reloadTime);

        _canAttack = true;
    }

    public void Reset()
    {
        _canAttack = false;
        _coroutine = StartCoroutine(Reload());
    }
}
