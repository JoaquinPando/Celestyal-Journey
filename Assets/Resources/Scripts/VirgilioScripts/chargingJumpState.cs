using UnityEngine;

public class ChargingJumpState : IPlayerState
{
    private PlayerController player;
    private bool jumpTriggered = false;
    private float initialHorizontal;
    private float startChargeTime;

    public float maxJumpPower = 5f;
    public float maxChargeTime = 0.65f;
    public float horizontalImpulse = 15f;
    public float verticalImpulseFactor = 3.5f;

    public void Enter(PlayerController player)
    {
        this.player = player;
        player.jumpValue = 0f;
        player.canJump = true;
        jumpTriggered = false;
        startChargeTime = Time.time;

        // Detectar si el jugador está presionando A o D
        initialHorizontal = Input.GetAxisRaw("Horizontal"); // -1, 0 o 1
    }

    public void Exit()
    {
        player.jumpValue = 0f;
        jumpTriggered = false;
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.W) && player.grounded)
        {
            float heldTime = Time.time - startChargeTime;
            player.jumpValue = Mathf.Clamp(heldTime, 0f, maxChargeTime) * maxJumpPower;
        }

        if (Input.GetKeyUp(KeyCode.W) && player.grounded)
        {
            jumpTriggered = true;
        }
    }

    public void FixedUpdate()
    {
        

        if (jumpTriggered)
        {
            player.rb2d.linearVelocity = Vector2.zero; // Evita acumulación

            float forceScale = Mathf.Pow(player.jumpValue / maxJumpPower, 1.2f); // efecto curva
            float adjustedHorizontal = horizontalImpulse * forceScale;
            Vector2 jumpForce = new Vector2(initialHorizontal * adjustedHorizontal, player.jumpValue * verticalImpulseFactor);

            player.rb2d.AddForce(jumpForce, ForceMode2D.Impulse);

            player.TransitionToState(new jumpingState());
        }
        else
        {
            player.rb2d.linearVelocity = new Vector2(0f, player.rb2d.linearVelocity.y);
        }
    }
}
