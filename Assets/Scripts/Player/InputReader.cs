using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public Vector2 MoveDirection { get; private set; }
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    
    private bool _isInteract;
    private bool _isBoosted;

    private void Update()
    {
        // �������� ����������� ����������� ������ ����� ���� � ����������
        Horizontal = Input.GetAxis(ConstantsData.InputData.HORIZONTAL_AXIS);
        Vertical = Input.GetAxis(ConstantsData.InputData.VERTICAL_AXIS);
        MoveDirection = new Vector2(Horizontal, Vertical).normalized;  // + ����������� ������ ��������, ����� ������������ �������� �� ���� �������

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
