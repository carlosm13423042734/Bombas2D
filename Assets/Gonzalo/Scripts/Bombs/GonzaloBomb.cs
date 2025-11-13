using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GonzaloBomb : MonoBehaviour, IExploitable
{
    [SerializeField]
    private float radius;
    [SerializeField]
    private float explosionPower;
    [SerializeField]
    private ParticleSystem explosionEffect;

    //Explosión de la bomba
    public void Explode()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, radius);

        //Metodo para que la bomba haga PUM visualmente
        if (explosionEffect != null)
        {
            ParticleSystem effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration);
        }

        //Empuja a cada objeto con collider, después se destruye
        foreach (Collider2D collider in objetos)
        {
            Rigidbody2D rb2D = collider.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                Vector2 direction = (collider.transform.position - transform.position).normalized;
                float distance = 1 + direction.magnitude;
                float force = explosionPower / distance;
                rb2D.AddForce(direction * force);
            }
        }
        Destroy(this.gameObject, 0.1f);
    }

    //Método para ver en el editor el radio de la explosión
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
