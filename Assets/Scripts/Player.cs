using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InputReader), typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerMover _playerMover;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _playerMover = GetComponent<PlayerMover>();
    }
    private void FixedUpdate()
    {
        // ¬ыполн€ем движение в сторону направлени€ вектора, если вектор существуует
        if (_inputReader._moveDirection != null)
        {
            _playerMover.Move(_inputReader._moveDirection);
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
