using UnityEngine;

public class ChargingJumpState : IPlayerState
{
    private PlayerController player;
    private bool jumpTriggered = false;
    private float initialHorizontal;
    private float startChargeTime;

    public float maxJumpPower = 4f;
    public float maxChargeTime = 0.60f;
    public float horizontalImpulse = 1f;
    public float verticalImpulseFactor = 2f;

    public void Enter(PlayerController player)
    {
        this.player = player;
        player.jumpValue = 0f;
        player.canJump = true;
        jumpTriggered = false;
        startChargeTime = Time.time;

        initialHorizontal = Input.GetAxisRaw("Horizontal");

        player.SetAnimation("walk", false);   // desactiva caminata
        
    }

    public void Exit()
    {
        jumpTriggered = false;
        player.SetAnimation("charge", false); // termina animación de carga
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.W) && player.grounded)
        {
            player.SetAnimation("charge", true);  // inicia animación de carga
            float heldTime = Time.time - startChargeTime;
            player.jumpValue = Mathf.Clamp(heldTime, 0f, maxChargeTime) * maxJumpPower;
        }

        if (Input.GetKeyUp(KeyCode.W) && player.grounded)
        {
            player.SetAnimation("charge", false); // ← Este activa la animación
            jumpTriggered = true;
        }
    }

    public void FixedUpdate()
    {
        if (jumpTriggered)
        {
            player.rb2d.linearVelocity = Vector2.zero;
            float forceScale = Mathf.Lerp(0.5f, 1f, player.jumpValue / maxJumpPower);
            float adjustedHorizontal = horizontalImpulse * forceScale * 2f;

            Vector2 jumpVelocity = new Vector2(
                initialHorizontal * adjustedHorizontal,
                player.jumpValue * verticalImpulseFactor
            );

            player.rb2d.linearVelocity = jumpVelocity;

            player.TransitionToState(new jumpingState());
        }
        else
        {
            // Evita que se deslice durante la carga
            player.rb2d.linearVelocity = new Vector2(0f, player.rb2d.linearVelocity.y);
        }
    }
}
