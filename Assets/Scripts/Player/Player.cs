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
        // Получаем необходимые компоненты объекта
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<PlayerMover>();
        _animator = GetComponent<PlayerAnimator>();
        _collisionHandler = GetComponent<CollisionHandler>();
    }

    // Подписываемся на события
    private void OnEnable()
    {
        _collisionHandler.BridgeReached += OnBridgeReached;
    }

    // Отписываемся от событий
    private void OnDisable()
    {
        _collisionHandler.BridgeReached -= OnBridgeReached;
    }

    private void FixedUpdate()
    {
        // Запуск анимации движения игрока
        _animator.SetSpeed(Mathf.Abs(_inputReader._moveDirection.x) + Mathf.Abs(_inputReader._moveDirection.y));

        // Нормализуем вектор движения, чтобы диагональное движение не было быстрее
        if (_inputReader._moveDirection.magnitude > 0)
            _inputReader._moveDirection.Normalize();

        // Выполняем движение игрока в сторону направления вектора, если вектор существует
        if (_inputReader._moveDirection != null)
            _mover.Move(_inputReader._moveDirection, _inputReader.GetIsBoosted());

        // Проверяем взаимодействие с интерактивными предметами
        if (_inputReader.GetIsInteract() && _iInteractable != null)
            _iInteractable.Interact();
    }

    private void OnBridgeReached (IInteractable bridgeBehaviour)
    {
        _iInteractable = bridgeBehaviour;
    }
}
