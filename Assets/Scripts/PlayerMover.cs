using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private float _boostedSpeed = 4.0f;
    [SerializeField] private float _cooldownTimer = 0f;
    [SerializeField] private float _boostDuration = 0.5f;
    [SerializeField] private float _boostCooldown = 2.0f;

    private Rigidbody2D _rigidbody;
    private Vector2 _movement;
    private bool _isBoosted;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Получаем ввод с клавиатуры
        _movement.x = Input.GetAxis(HORIZONTAL_AXIS);
        _movement.y = Input.GetAxis(VERTICAL_AXIS);

        // Нормализуем вектор движения, чтобы диагональное движение не было быстрее
        if (_movement.magnitude > 0)
        {
            _movement.Normalize();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryBoost();
        }

        // Обновление таймеров
        if (_isBoosted)
        {
            _cooldownTimer -= Time.deltaTime;
            if (_cooldownTimer <= 0)
            {
                _isBoosted = false;
                _cooldownTimer = _boostCooldown;
            }
        }
        else if (_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        // Перемещение игрока
        float currentSpeed = _isBoosted ? _boostedSpeed : _playerSpeed;
        _rigidbody.velocity = _movement * currentSpeed;

        /*
        if(_isBoosted)
        {
            _rigidbody.AddForce(_rigidbody.velocity * _boostedSpeed);
            _isBoosted = false;
        }
        */
    }
    private void TryBoost()
    {
        // Проверяем, можно ли использовать ускорение
        if (!_isBoosted && _cooldownTimer <= 0 && _movement.magnitude > 0)
        {
            _isBoosted = true;
            _cooldownTimer = _boostDuration;
        }
    }

}
