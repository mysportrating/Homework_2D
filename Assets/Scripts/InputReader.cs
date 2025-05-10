using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    public Vector2 _moveDirection;
    private void Update()
    {
        // Получаем ввод с клавиатуры
        _moveDirection.x = Input.GetAxis(HORIZONTAL_AXIS);
        _moveDirection.y = Input.GetAxis(VERTICAL_AXIS);

        // Ускорение игрока (получаем ввод и обрабатываем логику)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //TryBoost();
        }
    }
}
