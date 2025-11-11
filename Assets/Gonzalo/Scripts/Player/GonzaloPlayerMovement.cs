using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonzaloPlayerMovement : MonoBehaviour
{
    //Atributos editables desde el editor
    [SerializeField]
    private float moveSpeed;
    [SerializeField] 
    private float maxSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private LayerMask capaRequerida;
    [SerializeField]
    private LayerMask capaRequerida2;

    //Atributos predefinidos
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer sprite;
    private float anchoPersonaje;
    private float lastHorizontalSpeed;
    private float speedX;
    private bool IsAllowedToMove = true;
    private bool isGrounded = true;
    RaycastHit2D rayCastGround1;
    RaycastHit2D rayCastGround2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anchoPersonaje = GetComponent<SpriteRenderer>().bounds.size.x;
        sprite = GetComponent<SpriteRenderer>();
        RayDraws(); //MÉTODO PARA VER LOS RAYCAST EN TIEMPO DE EJECUCIÓN, QUITAR EN LA VERSIÓN FINAL
    }

    
    private void FixedUpdate()
    {
        if (!IsAllowedToMove) return;

        speedX = movement.x * moveSpeed;

        if (movement.x != 0)
        {
            // Movimiento directo con input
            rb.velocity = new Vector2(speedX, rb.velocity.y);
        }
        else
        {
            // Si no hay input, detenemos el movimiento horizontal
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        // Limitar velocidad horizontal
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAllowedToMove == true)
        {
            PlayerMotion();
            Jumping();
            RayDraws();
        }

        if (rayCastGround1 || rayCastGround2)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void PlayerMotion() //Movimiento del jugador
    {
        //Lectura de movimiento
        movement.x = Input.GetAxisRaw("Horizontal");
        //Se comprueba la dirección para girar el sprite
        if (rb.velocity.x < 0.001)
        {
            sprite.flipX = true;
        }
        if (rb.velocity.x > 0)
        {
            sprite.flipX = false;
        }

    }

    //Metodo que gestiona el salto de Mario a base de RayCasts, así se comprueba si está en el suelo o no para evitar saltar en el aire
    void Jumping()
    {
        rayCastGround1 = Physics2D.Raycast(new Vector2((this.transform.position.x + anchoPersonaje / 2), this.transform.position.y), Vector2.down, 0.7f, capaRequerida);
        rayCastGround2 = Physics2D.Raycast(new Vector2(this.transform.position.x - anchoPersonaje / 2, this.transform.position.y), Vector2.down, 0.7f, capaRequerida);     
        // If it hits something...
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    //Método de comprobación para simular los Raycast que detectan que el jugador pueda saltar
    private void RayDraws()
    {
        Debug.DrawRay(new Vector2(this.transform.position.x + anchoPersonaje / 2, this.transform.position.y), Vector2.down, Color.red);
        Debug.DrawRay(new Vector2(this.transform.position.x - anchoPersonaje / 2, this.transform.position.y), Vector2.down, Color.red);
    }

}
