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
    }
}
