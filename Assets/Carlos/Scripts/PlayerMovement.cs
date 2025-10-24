using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float velocity;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private LayerMask ground;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        isGrounded = CheckGrounded();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * velocity, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    //Metodo para comprobar si está en el suelo y evitar el doble salto(2 raycast, en la parte derecha y en la parte izquierda
    private bool CheckGrounded()
    {
        float halfWidth = GetComponent<Collider2D>().bounds.extents.x;
        float offsetY = 0.6f;

        Vector2 leftRayOrigin = new Vector2(transform.position.x - (halfWidth - 0.15f), transform.position.y);
        Vector2 rightRayOrigin = new Vector2(transform.position.x + (halfWidth - 0.15f), transform.position.y);

        RaycastHit2D leftHit = Physics2D.Raycast(leftRayOrigin, Vector2.down, offsetY, ground);
        RaycastHit2D rightHit = Physics2D.Raycast(rightRayOrigin, Vector2.down, offsetY, ground);

        return leftHit.collider != null || rightHit.collider != null;
    }
    //Metodo para ver los rayos al probarlo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float halfWidth = GetComponent<Collider2D>() != null ? GetComponent<Collider2D>().bounds.extents.x : 0.5f;
        float offsetY = 0.6f;

        Vector2 leftRayOrigin = new Vector2(transform.position.x - (halfWidth - 0.15f), transform.position.y);
        Vector2 rightRayOrigin = new Vector2(transform.position.x + (halfWidth - 0.15f), transform.position.y);

        Gizmos.DrawRay(leftRayOrigin, Vector2.down * offsetY);
        Gizmos.DrawRay(rightRayOrigin, Vector2.down * offsetY);
    }
}
