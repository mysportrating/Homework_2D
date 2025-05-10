using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InputReader), typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerMover _mover;
    private PlayerAnimator _animator;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<PlayerMover>();
        _animator = GetComponent<PlayerAnimator>();
    }
    private void FixedUpdate()
    {
        // «апуск анимации движени€ игрока
        _animator.SetSpeed(Mathf.Abs(_inputReader._moveDirection.x) + Mathf.Abs(_inputReader._moveDirection.y));

        // ¬ыполн€ем движение в сторону направлени€ вектора, если вектор существуует
        if (_inputReader._moveDirection != null)
        {
            _mover.Move(_inputReader._moveDirection);
        }

        // Ќормализуем вектор движени€, чтобы диагональное движение не было быстрее
        if (_inputReader._moveDirection.magnitude > 0)
        {
            _inputReader._moveDirection.Normalize();
        }

        // ѕримен€ем ускорение, если ускорение не ограничено через cooldown
        //if (_inputReader.GetIsBoosted) { }
        //_playerMover.TryBoost();
    }
}
