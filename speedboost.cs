using System.Collections;
using System.IO;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public PlayerControllerWASD Player;
    

    public float boostDuration = 5f;
    private MeshRenderer meshRenderer;
  
    
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
