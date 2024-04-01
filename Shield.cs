using System.Collections;
using UnityEngine;

public class ShieldBoost : MonoBehaviour
{  
    public PlayerControllerArrows Player;
    public float shieldDuration = 5f;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.CompareTag("Player")&& Player != null)
        {
            StartCoroutine(ActivateShield(other.gameObject));
            meshRenderer.enabled = false;
            
            StartCoroutine(DestroyObjectAfterDelay(shieldDuration));
        }
    }

    IEnumerator ActivateShield(GameObject player)
    {
        PlayerControllerArrows playerController = player.GetComponent<PlayerControllerArrows>();
        playerController.isShielded = true;
        yield return new WaitForSeconds(shieldDuration);
        playerController.isShielded = false;
        
    }

    IEnumerator DestroyObjectAfterDelay(float delay)
    {   
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        
    }



}
