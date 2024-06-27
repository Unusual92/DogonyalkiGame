using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotScript : MonoBehaviour
{
    // �������� ������������ ����
    public float speed = 3.0f;

    // �������� �������� ����
    public float turnSpeed = 90.0f;

    // ���������� ��� ����
    public float gravity = -9.81f;

    // ���������� ��� �������� CharacterController ����
    private CharacterController controller;

    // ���������� ��� �������� ������������ ��������
    private float verticalVelocity = 0.0f;

    // ����, �����������, ��������� �� ��� �� �����
   // private bool isGrounded;

    void Start()
    {
        // �������� CharacterController �� ����������� ����
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // ���������, ��������� �� ��� �� �����
       // CheckGrounded();

        // �������� ������� ������������ ���� �����
        MoveForward();

        // �������� ������� �������� ������������ � ������������
        CheckForObstacles();

        // ��������� ���������� � ��������� ������������ ��������
        verticalVelocity += gravity * Time.deltaTime;

        // ������������ ������������ ��������
        verticalVelocity = Mathf.Clamp(verticalVelocity, -10.0f, 0.0f);

        // ����������� ���� �����������
        controller.Move(Vector3.up * verticalVelocity * Time.deltaTime);
    }

    void MoveForward()
    {
        // ������ ������ �������� �����, �������� �������� � �����
        Vector3 movement = transform.forward * speed * Time.deltaTime;

        // ��������, ��� ��� �� ����� ������������ �� ���������
        movement.y = 0;

        // ����������� ���� � ������� CharacterController.Move()
        controller.Move(movement);
    }

    void CheckForObstacles()
    {
        // ������ RaycastHit ��� �������� ���������� � ������������
        RaycastHit hit;

        // ������ ������ ����������� ����� �� ����
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        // ������ Raycast ����� �� ���������� 1 �������
        if (Physics.Raycast(transform.position, fwd, out hit, 1.0f))
        {
            // ���� Raycast �������� � ������ � ����� "Wall"
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                // �������� ������� �������� �� �����
                TurnAwayFromWall(hit.normal);
            }
        }
    }

    void TurnAwayFromWall(Vector3 normal)
    {
        // ���������� ����������� �������� � ����������� �� ������� �����
        float turnDirection = Vector3.Dot(normal, transform.right) > 0 ? 1 : -1;

        // ������������ ���� ������ ��� Y � ������ �������� �������� � �������
        transform.Rotate(0, turnDirection * turnSpeed * Time.deltaTime, 0);
    }

    /*void CheckGrounded()
    {
        // ���������, ��������� �� ��� �� �����, ����� Raycast ����
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