using System;
using UnityEngine;

[RequireComponent(typeof(BirdMover))]
[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(ScoreCounter))]
[RequireComponent (typeof(Attaker))]
[RequireComponent(typeof(Animator))]
public class Bird : MonoBehaviour
{
    private const string AttackTrigger = nameof(AttackTrigger);

    private BirdMover _mover;
    private CollisionHandler _handler;
    private ScoreCounter _scoreCounter;
    private Attaker _attaker;
    private Animator _animator;

    public event Action GameOver;

    private void Awake()
    {
        _handler = GetComponent<CollisionHandler>();
        _scoreCounter = GetComponent<ScoreCounter>();
        _mover = GetComponent<BirdMover>();
        _attaker = GetComponent<Attaker>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _attaker.CanAttack)
            _animator.SetTrigger(AttackTrigger);
    }

    public void AddScore() => _scoreCounter.Add();

    private void AttackToggle() => _attaker.Attack();

    private void ProcessCollision() => GameOver?.Invoke();

    public void Reset()
    {
        _scoreCounter.Reset();
        _mover.Reset();
        _attaker.Reset();
    }
}
