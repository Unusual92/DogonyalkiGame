using System.Collections;
using UnityEngine;

public class PlayerControllerWASD : MonoBehaviour
{
  
   
    public float speed = 150.0f;


    // переменный для буста скорости
    public float groundDistance = 0.1f;
    public float baseSpeed = 150f;
    public float boostDuration = 5f;

    private Vector3 movement;
    public float friction = 5.0f;
    private SphereCollider sphereCollider;
    private Rigidbody rb;
 

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

        Vector3 movement = new Vector3(moveX, moveY, moveZ) * speed * Time.deltaTime;
        rb.velocity = movement;




    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Vector3 reflection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            transform.forward = reflection;
        }
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