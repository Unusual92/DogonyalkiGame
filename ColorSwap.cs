using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwap : MonoBehaviour
{
    private Renderer thisRenderer;

    private void Start()
    {
        thisRenderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Renderer collidedObjectRenderer = collision.gameObject.GetComponent<Renderer>();

            if (collidedObjectRenderer != null)
            {
                Color tempColor = thisRenderer.material.color;
                thisRenderer.material.color = collidedObjectRenderer.material.color;
                collidedObjectRenderer.material.color = tempColor;
            }
        }
    }
}