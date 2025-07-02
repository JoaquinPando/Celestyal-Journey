using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class ChamucoMonoBehaviourScript : MonoBehaviour
{
    public float speed = 0.5f;
    public float rayLength = 0.2f;
    public string wallTag = "Wall";
    public float idleDelay = 0.5f; // Tiempo de espera antes de girar

    private Rigidbody2D rb2d;
    private bool movingRight = true;
    private Transform spriteTransform;
    private Animator animator;
    private bool isWaiting = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteTransform = transform;
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (isWaiting) return;

        float moveDir = movingRight ? 1f : -1f;
        rb2d.linearVelocity = new Vector2(moveDir * speed, rb2d.linearVelocity.y);

        // Activar animación de caminar si hay velocidad
        if (animator != null)
        {
            animator.SetBool("walk", Mathf.Abs(rb2d.linearVelocity.x) > 0.1f);
        }

        // Raycast para detectar pared
        Vector2 rayOrigin = (Vector2)transform.position + Vector2.down * 0.1f;
        Vector2 rayDirection = movingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayLength);

        Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.red);

        if (hit.collider != null && hit.collider.CompareTag(wallTag))
        {
            StartCoroutine(PauseAndFlip());
        }
    }

    IEnumerator PauseAndFlip()
    {
        isWaiting = true;
        rb2d.linearVelocity = Vector2.zero;

        if (animator != null)
        {
            animator.SetBool("walk", false); // Cambia a idle
        }

        yield return new WaitForSeconds(idleDelay);

        Flip();
        isWaiting = false;
    }

    void Flip()
    {
        movingRight = !movingRight;

        if (spriteTransform != null)
        {
            Vector3 localScale = spriteTransform.localScale;
            localScale.x = Mathf.Abs(localScale.x) * (movingRight ? 1f : -1f);
            spriteTransform.localScale = localScale;
        }
    }
}
