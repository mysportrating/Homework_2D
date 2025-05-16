using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InputReader), typeof(PlayerMover), typeof(CollisionHandler))]
[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerMover _mover;
    private PlayerAnimator _animator;
    private CollisionHandler _collisionHandler;

    private IInteractable _iInteractable;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<PlayerMover>();
        _animator = GetComponent<PlayerAnimator>();
        _collisionHandler = GetComponent<CollisionHandler>();
    }

    private void OnEnable()
    {
        _collisionHandler.BridgeReached += OnBridgeReached;
    }

    private void OnDisable()
    {
        _collisionHandler.BridgeReached -= OnBridgeReached;
    }

    private void FixedUpdate()
    {
        // «апуск анимации движени€ игрока
        _animator.SetSpeed(Mathf.Abs(_inputReader._moveDirection.x) + Mathf.Abs(_inputReader._moveDirection.y));

        // Ќормализуем вектор движени€, чтобы диагональное движение не было быстрее
        if (_inputReader._moveDirection.magnitude > 0)
        {
            _inputReader._moveDirection.Normalize();
        }

        // ¬ыполн€ем движение игрока в сторону направлени€ вектора, если вектор существует
        if (_inputReader._moveDirection != null)
        {
            _mover.Move(_inputReader._moveDirection, _inputReader.IsBoosted);
        }

        // ќграничение ускорени€ игрока через cooldown
        if (_inputReader.IsBoosted)
        {
            //_mover.TryBoost(_inputReader._moveDirection, _inputReader.IsBoosted);
            _mover.CooldownTimerUpdate(_inputReader.IsBoosted);
        }

        // ѕровер€ем взаимодействие с интерактивными предметами
        if (_inputReader.GetIsInteract() && _iInteractable != null)
        {
            _iInteractable.Interact();
        }
    }

    private void OnBridgeReached (IInteractable bridgeBehaviour)
    {
        _iInteractable = bridgeBehaviour;
    }
}
