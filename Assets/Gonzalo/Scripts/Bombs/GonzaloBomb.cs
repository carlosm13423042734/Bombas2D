using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonzaloBomb : MonoBehaviour
{
    [SerializeField]
    private float radius;
    [SerializeField]
    private float explosionPower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Explosion();
        }
    }

    public void Explosion()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(transform.position, radius);

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

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
