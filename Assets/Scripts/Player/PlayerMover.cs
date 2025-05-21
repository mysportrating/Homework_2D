using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _normalSpeed = 3.0f;
    [SerializeField] private float _boostedSpeed = 5.0f;
    [SerializeField] private float _currentSpeed = 3.0f;
    [SerializeField] private float _cooldownTimer = 0.0f;
    [SerializeField] private float _boostDuration = 1.0f;
    [SerializeField] private float _boostCooldown = 4.0f;

    private Rigidbody2D _rigidbody;
    private bool _isTurnedRight = true;

    private void Start()
    {
        // �������� ����������� ���������� �������
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 moveDirection, bool isBoosted)
    {
        // ������������ ����������� ������ � ����������� �������
        _rigidbody.velocity = moveDirection * _currentSpeed;

        // ���������� ������� ��������� � ��������� �������� ������
        if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
        }
        if (isBoosted && _cooldownTimer <= 0)
        {
            _currentSpeed = _boostedSpeed;
            _cooldownTimer = _boostCooldown;
        }
        if (_boostCooldown >= _cooldownTimer + _boostDuration)
        {
            _currentSpeed = _normalSpeed;
        }

        // ����� ����������� ������� ������
        if ((moveDirection.x > 0 && !_isTurnedRight) || (moveDirection.x < 0 && _isTurnedRight))
        {
            _isTurnedRight = !_isTurnedRight;
            transform.Flip();
        }
    }
}
