using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InputReader), typeof(PlayerMover))]
[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerMover _mover;
    private PlayerAnimator _animator;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _mover = GetComponent<PlayerMover>();
        _animator = GetComponent<PlayerAnimator>();
    }
    private void FixedUpdate()
    {
        // ������ �������� �������� ������
        _animator.SetSpeed(Mathf.Abs(_inputReader._moveDirection.x) + Mathf.Abs(_inputReader._moveDirection.y));

        // ����������� ������ ��������, ����� ������������ �������� �� ���� �������
        if (_inputReader._moveDirection.magnitude > 0)
        {
            _inputReader._moveDirection.Normalize();
        }

        // ��������� �������� � ������� ����������� �������, ���� ������ �����������
        if (_inputReader._moveDirection != null)
        {
            _mover.Move(_inputReader._moveDirection);
        }

        // ����������� ��������� ����� cooldown
        if (_inputReader.IsBoosted)
        {
            //_mover.TryBoost(_inputReader._moveDirection);
            _mover.CooldownTimerUpdate();
        }
    }
}
