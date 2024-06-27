using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    public GameObject objectToSpawn; 
    public int numberOfObjectsToSpawn; 

    public GameObject objectToSpawn2; 
    public int numberOfObjectsToSpawn2; 

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

       
        for (int i = 0; i < numberOfObjectsToSpawn; i++)
        {
           
            float x = Random.Range(0, 30);
            float y = 0.5f;
            float z = Random.Range(0, 30);

           
            Collider[] hitColliders = Physics.OverlapSphere(new Vector3(x, y, z), 0.1f);
            while (hitColliders.Length > 0)
            {
                bool isFinish = false;
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.gameObject.CompareTag("Finish"))
                    {
                        isFinish = true;
                        break;
                    }
                }

                if (isFinish)
                {
                    x = Random.Range(0, 30);
                    z = Random.Range(0, 30);
                    hitColliders = Physics.OverlapSphere(new Vector3(x, y, z), 0.1f);
                }
                else
                {
                    x = Random.Range(0, 30);
                    z = Random.Range(0, 30);
                }
            }

           
            Instantiate(objectToSpawn, new Vector3(x, y, z), Quaternion.identity);
        }

        for (int i = 0; i < numberOfObjectsToSpawn2; i++)
        {
            
            float x = Random.Range(0, 30);
            float y = 0.5f;
            float z = Random.Range(0, 30);

            
            Collider[] hitColliders = Physics.OverlapSphere(new Vector3(x, y, z), 0.1f);
            while (hitColliders.Length > 0)
            {
                bool isFinish = false;
                foreach (var hitCollider in hitColliders)
                {
                    if (hitCollider.gameObject.CompareTag("Finish"))
                    {
                        isFinish = true;
                        break;
                    }
                }

                if (isFinish)
                {
                    x = Random.Range(0, 30);
                    z = Random.Range(0, 30);
                    hitColliders = Physics.OverlapSphere(new Vector3(x, y, z), 0.1f);
                }
                else
                {
                    x = Random.Range(0, 30);
                    z = Random.Range(0, 30);
                }
            }

          
            Instantiate(objectToSpawn2, new Vector3(x, y, z), Quaternion.identity);
        }
    }

}