using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonzaloBomb : MonoBehaviour
{
    [SerializeField]
    private float radius;
    [SerializeField]
    private float explosionPower;
    [SerializeField]
    private ParticleSystem explosionEffect;

    public void Explosion()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, radius);

        //Metodo para que la bomba haga PUM visualmente
        if (explosionEffect != null)
        {
            ParticleSystem effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            effect.Play();
            Destroy(effect.gameObject, effect.main.duration);
        }

        foreach (Collider2D colisionador in objetos)
        {
            Rigidbody2D rb2D = colisionador.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                Vector2 direccion = colisionador.transform.position - transform.position;
                float distancia = 1 + direccion.magnitude;
                float fuerzaFinal = explosionPower / distancia;
                rb2D.AddForce(direccion * fuerzaFinal);
            }
        }
        Destroy(this.gameObject, 0.1f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
