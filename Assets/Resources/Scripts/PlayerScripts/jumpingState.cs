using UnityEngine;

public class jumpingState : IPlayerState
{
    private PlayerController player;

public void Enter(PlayerController player)
{
    this.player = player;
    // 1) aplica la física del salto
    player.rb2d.linearVelocity = new Vector2(player.rb2d.linearVelocity.x, player.jumpValue * 2f);
    // 2) dispara la animación
    player.animator.SetTrigger("Jump");
    // 3) muestra el diálogo y reproduce el audio
    player.TriggerJumpDialogue();
}


    public void FixedUpdate()
    {
        // Nada aquí por ahora
    }

    public void Update()
    {


        // Si ya aterrizó, restaurar material y pasar a Idle
        if (player.grounded)
        {
            player.TransitionToState(new IdleState());
        }
    }

    public void Exit()
    {
        player.jumpValue = 0f;
        player.speed = 1f;
        player.rb2d.sharedMaterial = player.normalMat;
    }
}
