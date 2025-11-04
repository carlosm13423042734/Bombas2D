using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask ground;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private float moveInput;

    // knockback temporal
    private Vector2 externalForce = Vector2.zero;
    private float externalForceDuration = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        isGrounded = CheckGrounded();

        if (Input.GetButtonDown("Jump") && isGrounded)
            Jump();
    }

    void FixedUpdate()
    {
        // Mientras haya knockback, se ignora el control lateral
        if (externalForceDuration > 0f)
        {
            rb.velocity = externalForce;
            externalForceDuration -= Time.fixedDeltaTime;
        }
        else
        {
            rb.velocity = new Vector2(moveInput * velocity, rb.velocity.y);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

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

    // Método público para que la bomba llame
    public void ApplyKnockback(Vector2 direction, float force, float duration = 0.3f)
    {
        externalForce = direction.normalized * force;
        externalForceDuration = duration;
    }
}
