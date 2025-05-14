using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;
    private float horizontal;
    private bool grounded;
    private Animator animator;
    public bool canJump = true;
    public float jumpValue = 0.0f;
    public PhysicsMaterial2D bounceMat, normalMat;
        void Start()
    {
      rb2d = GetComponent<Rigidbody2D>();  
      animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        horizontal = Input.GetAxis("Horizontal");
        if(jumpValue == 0.0f && grounded)
        {
            rb2d.linearVelocity = new Vector2(horizontal * speed, rb2d.linearVelocity.y);// se establece la velocidad del objeto en x y y, donde x es el valor de horizontal multiplicado por la velocidad y y es la velocidad actual en y
        }
        
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


        if(jumpValue > 0)
        {
            rb2d.sharedMaterial = bounceMat;
        }
        else
        {
            rb2d.sharedMaterial = normalMat;
        }
        
        if (Input.GetKey(KeyCode.W) && grounded && canJump)
        {
            jumpValue += 0.1f;
        }
        if(Input.GetKeyDown(KeyCode.W) && grounded && canJump)
        {
            rb2d.linearVelocity = new Vector2(0.0f, rb2d.linearVelocity.y);
        }

        if (jumpValue >=20f && grounded){
            float tempx = horizontal * speed;
            float tempy = jumpValue;
            rb2d.linearVelocity = new Vector2(tempx, tempy); 
            Invoke("ResetJumpValue", 0.2f);// se invoca la función ResetJumpValue después de 0.2 segundos
        }

        if(Input.GetKeyUp(KeyCode.W) )
        {
            if(grounded)
            {
                rb2d.linearVelocity = new Vector2(horizontal * speed,jumpValue);
                jumpValue = 0.0f;
            }
            canJump = true;
        }
        
    }

    void ResetJumpValue()
    {
        canJump = false;
        jumpValue = 0.0f;
    }

   
}
