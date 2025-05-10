using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InputReader), typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    private InputReader _inputReader;
    private PlayerMover _playerMover;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _playerMover = GetComponent<PlayerMover>();
    }
    private void FixedUpdate()
    {
        // ��������� �������� � ������� ����������� �������, ���� ������ �����������
        if (_inputReader._moveDirection != null)
        {
            _playerMover.Move(_inputReader._moveDirection);
        }

        // ����������� ������ ��������, ����� ������������ �������� �� ���� �������
        if (_inputReader._moveDirection.magnitude > 0)
        {
            _inputReader._moveDirection.Normalize();
        }

        // ��������� ���������, ���� ��������� �� ���������� ����� cooldown
        //if (_inputReader.GetIsBoosted) { }
        //_playerMover.TryBoost();
    }
}
