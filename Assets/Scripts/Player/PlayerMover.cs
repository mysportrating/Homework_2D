using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private float _boostedSpeed = 4.0f;
    [SerializeField] private float _cooldownTimer = 0f;
    [SerializeField] private float _boostDuration = 0.5f;
    [SerializeField] private float _boostCooldown = 2.0f;

    private Rigidbody2D _rigidbody;
    private bool _isTurnedRight = true;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 moveDirection, bool IsBoosted)
    {
        // ќсуществл€ем перемещение игрока с учетом скорости в зависимости от состо€ни€ IsBoosted (ускоренно или обычное)
        float currentSpeed = IsBoosted ? _boostedSpeed : _playerSpeed;
        _rigidbody.velocity = moveDirection * currentSpeed;

        // —мена направлени€ взгл€да игрока
        if ((moveDirection.x > 0 && !_isTurnedRight) || (moveDirection.x < 0 && _isTurnedRight))
        {
            Flip();
        }
    }

    public void Flip()
    {
        _isTurnedRight = !_isTurnedRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void CooldownTimerUpdate(bool IsBoosted)
    {
        // ќбновление таймеров
        if (IsBoosted)
        {
            _cooldownTimer -= Time.deltaTime;
            if (_cooldownTimer <= 0)
            {
                IsBoosted = false;
                _cooldownTimer = _boostCooldown;
            }
        }
        else if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }

    public void TryBoost(Vector2 moveDirection, bool IsBoosted)
    {
        // ѕровер€ем, можно ли использовать ускорение
        if (IsBoosted && _cooldownTimer <= 0 && moveDirection.magnitude > 0)
        {
            _cooldownTimer = _boostDuration;
        }
    }
}
