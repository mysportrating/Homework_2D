using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(PlayerMover), typeof(CollisionHandler))]
[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerMover _mover;
    private PlayerAnimator _animator;
    private CollisionHandler _collisionHandler;

    private IInteractable _interactable;

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
        _collisionHandler.ObjectReached += OnObjectReached;
    }

    // Отписываемся от событий
    private void OnDisable()
    {
        _collisionHandler.ObjectReached -= OnObjectReached;
    }

    private void FixedUpdate()
    {
        // Запуск анимации движения игрока
        _animator.SetSpeed(Mathf.Abs(_inputReader.Horizontal) + Mathf.Abs(_inputReader.Vertical));

        // Выполняем движение игрока в сторону направления вектора, если вектор существует
        if (_inputReader.MoveDirection != null)
            _mover.Move(_inputReader.MoveDirection, _inputReader.GetIsBoosted());

        // Проверяем взаимодействие с интерактивными предметами
        if (_inputReader.GetIsInteract() && _interactable != null)
            _interactable.Interact();
    }

    private void OnObjectReached (IInteractable interactable)
    {
        _interactable = interactable;
    }
}