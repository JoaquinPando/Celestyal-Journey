using UnityEngine;

public class jumpingState : IPlayerState
{
    private PlayerController player;


    public void Enter(PlayerController player)
    {
        this.player = player;
    }

    public void FixedUpdate()
    {
        // Nada aquí por ahora
    }

    public void Update()
    {
        if (player.rb2d.linearVelocity.y > 0.1f)
        {
            player.rb2d.sharedMaterial = player.bounceMat;
        }
        else if (player.rb2d.linearVelocity.y <= 0)
        {
            player.rb2d.sharedMaterial = player.normalMat;
        }

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
