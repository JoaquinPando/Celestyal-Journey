using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private Rigidbody2D rb2d;
    private float horizontal;
    private bool grounded;
    private Animator animator;
    void Start()
    {
      rb2d = GetComponent<Rigidbody2D>();  
      animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal < 0.0f)// si el valor de horizontal es menor que 0, se invierte la escala del objeto en x
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (horizontal > 0.0f)// si el valor de horizontal es mayor que 0, se mantiene la escala del objeto en x
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        animator.SetBool("walk", horizontal != 0.0f);// si el valor de horizontal es diferente de 0, se activa la animación de caminar


        Debug.DrawRay(transform.position, Vector3.down * 0.4f, Color.red);// raycast para ver si el objeto está en el suelo
        if(Physics2D.Raycast(transform.position, Vector3.down, 0.4f))
        {
            grounded = true;
        }
        else grounded = false;
        

        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            Jump();
        }
        
    }
    private void FixedUpdate()
    {

        rb2d.linearVelocity = new Vector2(horizontal * speed, rb2d.linearVelocity.y);
    }
    private void Jump(){
        rb2d.AddForce(Vector2.up * jumpForce);// fuerza hacia arriba
    }
}
