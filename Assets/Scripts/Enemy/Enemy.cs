using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
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
        // Получаем необходимые компоненты объекта
        _rigidbody = GetComponent<Rigidbody2D>();
    }
}
