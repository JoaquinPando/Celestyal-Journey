using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb2d;
    public Animator animator;
    public float jumpValue = 0f;
    public bool grounded = false;
    public bool canJump = true;
    public float checkDistance = 0.4f;
    public float horizontal;

    private IPlayerState currentState;
    public PhysicsMaterial2D bounceMat, normalMat;

    // ÚNICA referencia al diálogo
    private JumpDialogue jumpDialogue;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        TransitionToState(new IdleState());
        speed = 1f;

        // Se asume que JumpDialogue está añadido en el Inspector
        jumpDialogue = GetComponent<JumpDialogue>();
        if (jumpDialogue == null)
            jumpDialogue = gameObject.AddComponent<JumpDialogue>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        FlipSprite();
        CheckGrounded();
        // Si estás caminando, asegúrate que NO se quede cargando
        if (horizontal != 0 && !Input.GetKey(KeyCode.W))
            SetAnimation("charge", false);
        currentState.Update();
        animator.SetBool("walk", horizontal != 0.0f);
    }

    public void TransitionToState(IPlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter(this);
    }

    public void SetAnimation(string name, bool state)
        => animator?.SetBool(name, state);

    void FlipSprite()
    {
        if (horizontal != 0)
            transform.localScale = new Vector3(Mathf.Sign(horizontal), 1, 1);
    }

    void CheckGrounded()
    {
        grounded = Physics2D.Raycast(transform.position, Vector3.down, checkDistance);
    }

    void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            Vector2 dir = (transform.position - collision.transform.position).normalized;
            rb2d.linearVelocity = new Vector2(Mathf.Sign(dir.x) * 0.5f, rb2d.linearVelocity.y);
            Debug.Log("Rebote manual contra pared");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall") &&
            Input.GetAxisRaw("Horizontal") != 0)
        {
            rb2d.linearVelocity = new Vector2(
                -Input.GetAxisRaw("Horizontal") * 1.5f,
                rb2d.linearVelocity.y
            );
            Debug.Log("Rebote jajaja");
        }
    }

    // Llama a ShowDialogue (método existente) en lugar de ShowNextDialogue
    public void TriggerJumpDialogue()
    {
        jumpDialogue?.ShowDialogue();
    }
}
