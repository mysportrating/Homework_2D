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
    private InputReader _inputReader;
    private bool _isTurnedRight = true;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputReader = GetComponent<InputReader>();
    }

    public void Move(Vector2 moveDirection)
    {
        // Перемещение игрока
        float currentSpeed = _inputReader.IsBoosted ? _boostedSpeed : _playerSpeed;
        _rigidbody.velocity = moveDirection * currentSpeed;

        // Смена направления взгляда игрока
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

    public void CooldownTimerUpdate()
    {
        // Обновление таймеров
        if (_inputReader.IsBoosted)
        {
            _cooldownTimer -= Time.deltaTime;
            if (_cooldownTimer <= 0)
            {
                _inputReader.IsBoosted = false;
                _cooldownTimer = _boostCooldown;
            }
        }
        else if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }

    public void TryBoost(Vector2 moveDirection)
    {
        // Проверяем, можно ли использовать ускорение
        if (_inputReader.IsBoosted && _cooldownTimer <= 0 && moveDirection.magnitude > 0)
        {
            _cooldownTimer = _boostDuration;
        }
    }
}
