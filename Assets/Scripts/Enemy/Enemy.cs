using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private float _maxSqrDistance = 0.05f;

    private Rigidbody2D _rigidbody;
    private bool _isTurnedRight = true;
    private int _wayPointIndex;
    private Transform _target;

    private void Start()
    {
        // Получаем необходимые компоненты объекта
        _rigidbody = GetComponent<Rigidbody2D>();

        _target = _wayPoints[_wayPointIndex].transform;
    }

    private void FixedUpdate()
    {
        Move();

        if (IsTargetReached())
            ChangeTarget();
    }
    private void Move()
    {
        Vector2 newPosition = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        _rigidbody.MovePosition(newPosition);
    }

    private bool IsTargetReached()
    {
        float sqrDistance = (transform.position - _target.position).sqrMagnitude;

        return sqrDistance <= _maxSqrDistance;
    }

    private void ChangeTarget()
    {
        _wayPointIndex++;

        if (_wayPointIndex >= _wayPoints.Length)
            _wayPointIndex = 0;

        _target = _wayPoints[_wayPointIndex].transform;

        // Смена направления взгляда врага
        if ((transform.position.x < _target.position.x && !_isTurnedRight) || (transform.position.x > _target.position.x && _isTurnedRight))
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
}
