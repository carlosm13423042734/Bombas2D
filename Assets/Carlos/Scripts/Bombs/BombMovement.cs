using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMovement : MonoBehaviour, IExploitable
{
    [SerializeField]
    private float timeToExplode = 2f;
    [SerializeField]
    private float explosionForce = 10f; // fuerza fija
    [SerializeField]
    private float explosionRadius = 3f;
    [SerializeField]
    private bool destroyAfterExplosion = true;

    private bool hasExploded = false;

    void Start()
    {
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
                PlayerMovement player = hit.GetComponent<PlayerMovement>();
                if (player != null)
                {
                    Vector2 direction = (player.transform.position - transform.position).normalized;
                    player.ApplyKnockback(direction, explosionForce);
                }
            }

        }

        if (destroyAfterExplosion)
            Destroy(gameObject, 0.2f);
    }

   

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
