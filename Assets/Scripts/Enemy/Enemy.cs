using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(Attaker))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    private const string AttackTrigger = nameof(AttackTrigger);

    private CollisionHandler _collisionHandler;
    private Attaker _attacker;
    private Animator _animator;
    private ObjectPool _pool;

    private void Awake()
    {
        _collisionHandler = GetComponent<CollisionHandler>();
        _attacker = GetComponent<Attaker>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _collisionHandler.CollisionDetected += Die;
    }

    private void OnDisable()
    {
        _collisionHandler.CollisionDetected -= Die;
    }

    private void Update()
    {
        if (_attacker.CanAttack)
            _animator.SetTrigger(AttackTrigger);
    }

    private void AttackToggle() => _attacker.Attack(); 

    public void SetPool(ObjectPool pool) => _pool = pool;

    private void Die() => _pool.PutObject(gameObject);
}
