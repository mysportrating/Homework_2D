using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public Vector2 _moveDirection;
    
    private bool _isInteract;
    private bool _isBoosted;

    private void Update()
    {
        // �������� ����������� ����������� ������ ����� ���� � ����������
        _moveDirection.x = Input.GetAxis(ConstantsData.InputData.HORIZONTAL_AXIS);
        _moveDirection.y = Input.GetAxis(ConstantsData.InputData.VERTICAL_AXIS);

        // ������ ��������� ������ ����� ���� � ����������
        if (Input.GetKeyDown(KeyCode.Space))
            _isBoosted = true;

        // ��������������� � �������������� ���������� ����� ���� � ����������
        if (Input.GetKeyDown(KeyCode.F))
            _isInteract = true;
    }

    // ������ �������� ������� ���������� ����� ������
    public bool GetIsBoosted() => GetBoolAsTrigger(ref _isBoosted);
    public bool GetIsInteract() => GetBoolAsTrigger(ref _isInteract);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
