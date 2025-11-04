using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BombMovement : MonoBehaviour, IExploitable
{
    [SerializeField] 
    private float timeToExplode;
    [SerializeField] 
    private float explosionForce;
    [SerializeField] 
    private float explosionRadius;
    [SerializeField] 
    private bool destroyAfterExplosion = true;

    private bool hasExploded = false;

    void Start()
    {
        // Inicia la cuenta atrás
        StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        yield return new WaitForSeconds(timeToExplode);
        explode();
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
                Rigidbody2D rb = hit.attachedRigidbody;
                if (rb != null)
                {
                    Vector2 direction = (rb.position - (Vector2)transform.position).normalized;

                    float distance = Vector2.Distance(rb.position, transform.position);
                    float adjustedForce = Mathf.Lerp(explosionForce, 0f, distance / explosionRadius);

                    rb.AddForce(direction * adjustedForce, ForceMode2D.Impulse);
                }
            }
        }


        if (destroyAfterExplosion)
        {
            Destroy(gameObject, 0.2f);
        }
    }


}

