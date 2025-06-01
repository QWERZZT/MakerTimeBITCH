using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
     public float speed = 5f;              // скорость ходьбы
    public float gravity = -9.81f;        // гравитация
    private Vector3 velocity;             // скорость для вертикального движения (гравитация)
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Получаем входные данные с клавиатуры (WASD или стрелочки)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Создаем вектор направления движения в локальных координатах персонажа
        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // Перемещаем персонажа
        controller.Move(move * speed * Time.deltaTime);

        // Обработка гравитации
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Если персонаж стоит на земле — сбрасываем вертикальную скорость
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // небольшое отрицательное значение, чтобы персонаж прочно стоял на земле
        }
    }
}
