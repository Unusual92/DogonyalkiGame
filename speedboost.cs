using System.Collections;
using System.IO;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public PlayerControllerWASD Player;
    float speed = 20f;

    public float boostDuration = 5f;
    private MeshRenderer meshRenderer;


    void Update()
    {
        // Вращение вокруг оси Y
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }


    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        if (other.gameObject.CompareTag("Player") && Player != null)
        {   if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
                Debug.Log("Mesh renderer disabled");
            }
            
            
            StartCoroutine(ResetSpeed());

           
        }
    }

    IEnumerator ResetSpeed()
    {   Debug.Log("M");
        yield return new WaitForSeconds(boostDuration);
        Destroy(gameObject);
        Debug.Log("Mesh ");
    }

}
