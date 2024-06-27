using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBonuses : MonoBehaviour
{
    public GameObject[] bonusesToSpawn; 
    public int numberOfBonusesToSpawn;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(SpawnBonusesCoroutine());
    }

    IEnumerator SpawnBonusesCoroutine()
    {
        while (true)
        {
            for (int i = 0; i < numberOfBonusesToSpawn; i++)
            {
                
                GameObject bonusToSpawn = bonusesToSpawn[Random.Range(0, bonusesToSpawn.Length)];

                
                float x = Random.Range(0, 30);
                float y = 0.5f;
                float z = Random.Range(0, 30);

             
                while (Physics.CheckSphere(new Vector3(x, y, z), 0.1f))
                {
                    x = Random.Range(0, 30);
                    z = Random.Range(0, 30);
                }

                Instantiate(bonusToSpawn, new Vector3(x, y, z), Quaternion.identity);
            }

            yield return new WaitForSeconds(8);
        }
    }
}