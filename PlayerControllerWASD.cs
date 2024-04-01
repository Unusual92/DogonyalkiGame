using System.Collections;
using UnityEngine;

public class PlayerControllerWASD : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            // Предотвратить движение юнита через стену
            Vector3 reflection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            transform.forward = reflection;
        }
    }
    // Скорость движения персонажа
    public float speed = 150.0f;

    // Ускорение персонажа
    public float acceleration = 0.0f;

    // Максимальная скорость персонажа
    public float maxSpeed = 160.0f;

    // Ссылка на поверхность, по которой движется персонаж
    public Transform ground;

    // Расстояние от персонажа до поверхности
    public float groundDistance = 0.1f;
    public float baseSpeed = 150f;
    public float boostDuration = 5f;

    private Vector3 movement;

    public Transform groundCheck;
    public LayerMask groundLayer;
    public Transform[] respawnPoints;
    public float friction = 5.0f;
    private SphereCollider sphereCollider;
    private Rigidbody rb;
    private bool isGrounded;
  
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.material.dynamicFriction = friction;
        sphereCollider.material.staticFriction = friction;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float moveX = 0;
        float moveY = 0;
        float moveZ = 0;

        // Управление первым юнитом с помощью WASD
        if (Input.GetKey(KeyCode.W))
        {
            moveZ = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveZ = -1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 1;
        }

        // Перемещение юнита в зависимости от нажатых клавиш
        Vector3 movement = new Vector3(moveX, moveY, moveZ) * speed * Time.deltaTime;
        rb.velocity = movement;



       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Untagged"))
        {
            speed = 300.0f;
            StartCoroutine(ResetSpeed());
        }

    }

    IEnumerator ResetSpeed()
    {
        yield return new WaitForSeconds(boostDuration);
        speed = baseSpeed;
    }
}