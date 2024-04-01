using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject objectToSpawn; // The object to spawn
    public int numberOfObjectsToSpawn; // The number of objects to spawn

    public GameObject objectToSpawn2; // The object to spawn
    public int numberOfObjectsToSpawn2; // The number of objects to spawn

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Generate the specified number of objects
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
            // Generate random x, y, and z coordinates
            float x = Random.Range(0, 30);
            float y = 0.5f;
            float z = Random.Range(0, 30);

            // Check if the point is outside the walls
            while (Physics.CheckSphere(new Vector3(x, y, z), 0.1f))
            {
                x = Random.Range(0, 30);
                z = Random.Range(0, 30);
            }

            // Create a new object at the generated point
            Instantiate(objectToSpawn, new Vector3(x, y, z), Quaternion.identity);
        }

        for (int i = 0; i < numberOfObjectsToSpawn2; i++)
        {
            // Generate random x, y, and z coordinates
            float x = Random.Range(0, 30);
            float y = 0.5f;
            float z = Random.Range(0, 30);

            // Check if the point is outside the walls
            while (Physics.CheckSphere(new Vector3(x, y, z), 0.1f))
            {
                x = Random.Range(0, 30);
                z = Random.Range(0, 30);
            }

            // Create a new object at the generated point
            Instantiate(objectToSpawn2, new Vector3(x, y, z), Quaternion.identity);
        }
    }
}