using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InputReader), typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAnimator), typeof(CollisionHandler))]
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

        // ¬ыполн€ем движение в сторону направлени€ вектора, если вектор существуует
        if (_inputReader._moveDirection != null)
        {
            _mover.Move(_inputReader._moveDirection);
        }

        // ќграничение ускорени€ через cooldown
        if (_inputReader.IsBoosted)
        {
            //_mover.TryBoost(_inputReader._moveDirection);
            _mover.CooldownTimerUpdate();
        }

        // ѕровер€ем взаимодействие с мостом
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
