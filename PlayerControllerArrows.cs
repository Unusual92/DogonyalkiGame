using System.Collections;
using UnityEngine;

public class PlayerControllerArrows : MonoBehaviour
{
   
   
    public float speed = 15.0f;
    public int materialChangeCount = 0;

    private Vector3 movement;

    public float friction = 5.0f;
    private SphereCollider sphereCollider;

    private Rigidbody rb;
  
    public bool isShielded = false;
    private MeshRenderer meshRenderer;

    private Renderer thisRenderer;
    void Start()

    {
        thisRenderer = GetComponent<Renderer>();
        meshRenderer = GetComponent<MeshRenderer>();
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

        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveZ = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveZ = -1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveX = 1;
        }

       
        Vector3 movement = new Vector3(moveX, moveY, moveZ) * speed * Time.deltaTime;
        rb.velocity = movement;


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && isShielded == false)
        {
           // Debug.Log("MEEE ");
            Renderer collidedObjectRenderer = collision.gameObject.GetComponent<Renderer>();

            if (collision.gameObject.CompareTag("Player") && collidedObjectRenderer != null)
            {
               // Debug.Log("Mesh ");
                Material tempMaterial = thisRenderer.material;
                thisRenderer.material = collidedObjectRenderer.material;
                collidedObjectRenderer.material = tempMaterial;
                Debug.Log(materialChangeCount++);
                materialChangeCount++;
            }
        }
        if (collision.gameObject.tag == "Wall")
        {
            
            Vector3 reflection = Vector3.Reflect(transform.forward, collision.contacts[0].normal);
            transform.forward = reflection;
        }
    }
}

