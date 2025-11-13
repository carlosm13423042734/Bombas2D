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
    public bool IsAllowedToMove = true;
    public bool IsBeenImpulsed = false;
    private bool isGrounded = true;
    private float lastDirection = 1f;
    RaycastHit2D rayCastGround1;
    RaycastHit2D rayCastGround2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anchoPersonaje = GetComponent<SpriteRenderer>().bounds.size.x;
        sprite = GetComponent<SpriteRenderer>();
    }

    
    private void FixedUpdate()
    {
        //Si el jugador no puede moverse esto no se cumple
        if (!IsAllowedToMove) return;

        speedX = movement.x * moveSpeed;

        if (movement.x != 0)
        {
            // Movimiento directo con input
            rb.velocity = new Vector2(speedX, rb.velocity.y);
        }
        else
        {
            //Si el jugador está siendo impulsado esto no se cumple
            if (IsBeenImpulsed) return;
            // Si no hay input, el movimiento horizontal se detiene para mejorar la experiencia de movimiento
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        // Limitar velocidad horizontal
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        //Acciones de movimiento
        if (IsAllowedToMove == true)
        {
            PlayerMotion();
            Jumping();
            FlipSprite();
        }
        //Comprobar que está en tierra
        if (rayCastGround1 || rayCastGround2)
        {
            isGrounded = true;
            IsBeenImpulsed = false;
        }
        else
        {
            isGrounded = false;
        }
        RayDraws(); //MÉTODO PARA VER LOS RAYCAST EN TIEMPO DE EJECUCIÓN, QUITAR EN LA VERSIÓN FINAL
    }

    //Movimiento del jugador
    private void PlayerMotion() 
    {
        //Lectura de movimiento
        movement.x = Input.GetAxisRaw("Horizontal");
        //Se guarda la última dirección 
        if (movement.x != 0)
        {
            lastDirection = movement.x;
        }
    }

    //Girar el sprite utilizando la última dirección
    private void FlipSprite()
    {
        if (lastDirection == 1)
        {
            sprite.flipX = true;
        }
        else if (lastDirection == -1)
        {
            sprite.flipX = false;
        }
    }

    //Metodo que gestiona el salto del jugador a base de RayCasts, así se comprueba si está en el suelo o no, para evitar saltar en el aire
    void Jumping()
    {
        rayCastGround1 = Physics2D.Raycast(new Vector2((this.transform.position.x + anchoPersonaje / 2), this.transform.position.y), Vector2.down, 0.7f, capaRequerida);
        rayCastGround2 = Physics2D.Raycast(new Vector2((this.transform.position.x - anchoPersonaje / 2), this.transform.position.y), Vector2.down, 0.7f, capaRequerida);     
        // Si el Raycast choca con una capa asignada y el jugador presiona espacio, salta
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    //Inicia corrutinas, es llamado en la bomba cuando impacta con el jugador
    public void PlayerBombed()
    {
        StartCoroutine(PlayerCantMove());
        StartCoroutine(PlayerIsImpulsed());
    }

    //Corrutina para que el jugador no pueda moverse durante poco tiempo
    IEnumerator PlayerCantMove()
    {
        IsAllowedToMove = false;
        yield return new WaitForSeconds(0.5f);
        IsAllowedToMove = true;
    }

    //Corrutina para que el movimiento horizontal no se detenga
    IEnumerator PlayerIsImpulsed()
    {
        IsBeenImpulsed = true;
        yield return new WaitForSeconds(1.5f);
        //playerMovementScript.IsBeenImpulsed = false;
    }

    //Método de comprobación en tiempo de ejecución para simular los Raycast que detectan que el jugador pueda saltar 
    private void RayDraws()
    {
        Debug.DrawRay(new Vector2(this.transform.position.x + anchoPersonaje / 2, this.transform.position.y), Vector2.down, Color.red);
        Debug.DrawRay(new Vector2(this.transform.position.x - anchoPersonaje / 2, this.transform.position.y), Vector2.down, Color.red);
    }

    //Dar la dirección del jugador
    public bool GetPlayerDirection()
    {
        return sprite.flipX;
    }
}
