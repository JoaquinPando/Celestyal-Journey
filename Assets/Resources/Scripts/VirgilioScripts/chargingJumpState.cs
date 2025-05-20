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

        // Detectar si el jugador está presionando A o D
        initialHorizontal = Input.GetAxisRaw("Horizontal"); // -1, 0 o 1
    }

    public void Exit()
    {
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

            // Usamos Lerp para dar impulso horizontal incluso en saltos cortos
            float forceScale = Mathf.Lerp(0.5f, 1f, player.jumpValue / maxJumpPower);
            float adjustedHorizontal = horizontalImpulse * forceScale *2f;

            Vector2 jumpVelocity = new Vector2(initialHorizontal * adjustedHorizontal, player.jumpValue * verticalImpulseFactor);
            player.rb2d.linearVelocity = jumpVelocity;

            player.TransitionToState(new jumpingState());
        }
        else
        {
            player.rb2d.linearVelocity = new Vector2(0f, player.rb2d.linearVelocity.y);
        }
    }
}
