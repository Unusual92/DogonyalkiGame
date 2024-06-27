using UnityEngine;

public class MaterialSwap : MonoBehaviour
{
    public bool isShielded = false;
    private Renderer thisRenderer;

    private void Start()
    {
        thisRenderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isShielded)
        {
            Renderer collidedObjectRenderer = collision.gameObject.GetComponent<Renderer>();

            if (collidedObjectRenderer != null)
            {
                Material tempMaterial = thisRenderer.material;
                thisRenderer.material = collidedObjectRenderer.material;
                collidedObjectRenderer.material = tempMaterial;
            }
        }
    }
}