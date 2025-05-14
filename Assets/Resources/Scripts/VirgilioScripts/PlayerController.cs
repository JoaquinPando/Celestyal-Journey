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

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        TransitionToState(new IdleState());
        speed = 1f; // Asigna un valor inicial a la velocidad


    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        FlipSprite();
        CheckGrounded();
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
    {
        if (animator) animator.SetBool(name, state);
    }

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
        currentState.FixedUpdate(); // 👈 Aquí se llama el de cada estado
    }
    

}
