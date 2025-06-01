using UnityEngine;

[RequireComponent(typeof(InputReader), typeof(PlayerMover), typeof(CollisionHandler))]
[RequireComponent(typeof(PlayerAnimator), typeof(Flipper))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerMover _mover;
    private PlayerAnimator _animator;
    private CollisionHandler _collisionHandler;
    private Flipper _flipper;

    private IInteractable _interactable;

    private void Awake()
    {
        // �������� ����������� ���������� �������
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<PlayerMover>();
        _animator = GetComponent<PlayerAnimator>();
        _collisionHandler = GetComponent<CollisionHandler>();
        _flipper = GetComponent<Flipper>();
    }

    // ������������� �� �������
    private void OnEnable()
    {
        _collisionHandler.ObjectReached += OnObjectReached;
    }

    // ������������ �� �������
    private void OnDisable()
    {
        _collisionHandler.ObjectReached -= OnObjectReached;
    }

    private void FixedUpdate()
    {
        // ������ �������� �������� ������
        _animator.SetSpeed(Mathf.Abs(_inputReader.Horizontal) + Mathf.Abs(_inputReader.Vertical));

        // ��������� �������� ������ � ������� ����������� �������, ���� ������ ����������
        if (_inputReader.MoveDirection != null)
        {
            _mover.Move(_inputReader.MoveDirection, _inputReader.GetIsBoosted());
            // ����� ����������� ������� ������
            _flipper.LookAtTarget(transform.position + Vector3.right * _inputReader.MoveDirection.x);
        }

        // ��������� �������������� � �������������� ����������
        if (_inputReader.GetIsInteract() && _interactable != null)
            _interactable.Interact();
    }

    private void OnObjectReached (IInteractable interactable)
    {
        _interactable = interactable;
    }
}