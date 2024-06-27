using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotScript : MonoBehaviour
{
    // Скорость передвижения бота
    public float speed = 3.0f;

    // Скорость поворота бота
    public float turnSpeed = 90.0f;

    // Гравитация для бота
    public float gravity = -9.81f;

    // Переменная для хранения CharacterController бота
    private CharacterController controller;

    // Переменная для хранения вертикальной скорости
    private float verticalVelocity = 0.0f;

    // Флаг, указывающий, находится ли бот на земле
   // private bool isGrounded;

    void Start()
    {
        // Получаем CharacterController из компонентов бота
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Проверяем, находится ли бот на земле
       // CheckGrounded();

        // Вызываем функцию передвижения бота вперёд
        MoveForward();

        // Вызываем функцию проверки столкновения с препятствием
        CheckForObstacles();

        // Применяем гравитацию и обновляем вертикальную скорость
        verticalVelocity += gravity * Time.deltaTime;

        // Ограничиваем вертикальную скорость
        verticalVelocity = Mathf.Clamp(verticalVelocity, -10.0f, 0.0f);

        // Передвигаем бота вертикально
        controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    void MoveForward()
    {
        // Создаём вектор движения вперёд, учитывая скорость и время
        Vector3 movement = transform.forward * speed * Time.deltaTime;

        // Убедимся, что бот не может перемещаться по вертикали
        movement.y = 0;

        // Передвигаем бота с помощью CharacterController.Move()
        controller.Move(movement);
    }

    void CheckForObstacles()
    {
        // Создаём RaycastHit для хранения информации о столкновении
        RaycastHit hit;

        // Создаём вектор направления вперёд от бота
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        // Делаем Raycast вперёд на расстояние 1 единица
        if (Physics.Raycast(transform.position, fwd, out hit, 1.0f))
        {
            // Если Raycast попадает в объект с тегом "Wall"
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                // Вызываем функцию поворота от стены
                TurnAwayFromWall(hit.normal);
            }
        }
    }

    void TurnAwayFromWall(Vector3 normal)
    {
        // Определяем направление поворота в зависимости от нормали стены
        float turnDirection = Vector3.Dot(normal, transform.right) > 0 ? 1 : -1;

        // Поворачиваем бота вокруг оси Y с учётом скорости поворота и времени
        transform.Rotate(0, turnDirection * turnSpeed * Time.deltaTime, 0);
    }

    /*void CheckGrounded()
    {
        // Проверяем, находится ли бот на земле, делая Raycast вниз
      //  RaycastHit hit;
        Vector3 down = transform.TransformDirection(Vector3.down);

        if (Physics.Raycast(transform.position, down, out hit, controller.height * 0.5f + 0.1f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }*/
}