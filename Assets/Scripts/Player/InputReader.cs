using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public Vector2 _moveDirection;
    
    private bool _isInteract;

    public bool IsBoosted { get; set; }

    private void Update()
    {
        // Получаем направление перемещения игрока через ввод с клавиатуры
        _moveDirection.x = Input.GetAxis(ConstantsData.InputData.HORIZONTAL_AXIS);
        _moveDirection.y = Input.GetAxis(ConstantsData.InputData.VERTICAL_AXIS);

        // Ускорение игрока (получаем ввод и обрабатываем логику)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsBoosted = true;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            _isInteract = true;
        }
    }

    public bool GetIsInteract() => GetBoolAsTrigger(ref _isInteract);

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
