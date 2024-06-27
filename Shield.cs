using System.Collections;
using UnityEngine;

public class ShieldBoost : MonoBehaviour
{
    public PlayerControllerArrows Player;
    public float shieldDuration = 5f;
    private MeshRenderer meshRenderer;
    float speed = 20f;
    void Update()
    {
        // Вращение вокруг оси Y
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Player != null)
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
        Debug.Log("Mesh renderer disabled");
    }
}