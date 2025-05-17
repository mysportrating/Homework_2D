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
        _animator.SetSpeed(Mathf.Abs(_inputReader.Horizontal) + Mathf.Abs(_inputReader.Vertical));

        // Выполняем движение игрока в сторону направления вектора, если вектор существует
        if (_inputReader.MoveDirection != null)
            _mover.Move(_inputReader.MoveDirection, _inputReader.GetIsBoosted());

        // Проверяем взаимодействие с интерактивными предметами
        if (_inputReader.GetIsInteract() && _iInteractable != null)
            _iInteractable.Interact();
    }

    private void OnBridgeReached (IInteractable bridgeBehaviour)
    {
        _iInteractable = bridgeBehaviour;
    }
}
