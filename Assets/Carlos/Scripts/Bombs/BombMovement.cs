using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombMovement : MonoBehaviour, IExploitable
{
    [SerializeField] 
    private float explosionForce;
    [SerializeField] 
    private float explosionRadius;
    [SerializeField] 
    private bool destroyAfterExplosion;

    private bool hasExploded = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) {
            explode();
        }
    }

    

    public void explode()
    {
        if (hasExploded) return;
        hasExploded = true;
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direction = hit.transform.position - transform.position;

                    float distance = 1 + direction.magnitude;
                    float adjustedForce = explosionForce / distance;

                    rb.AddForce(direction * adjustedForce);
                }
            }
        }


        if (destroyAfterExplosion)
        {
            Destroy(gameObject, 0.2f);
        }
    }


}

