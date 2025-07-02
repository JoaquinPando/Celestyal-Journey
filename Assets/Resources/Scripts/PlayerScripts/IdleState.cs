using UnityEngine;

public class IdleState : IPlayerState
{
    private PlayerController player;

    public void Enter(PlayerController player)
    {
        this.player = player;
        player.SetAnimation("walk", false);
        player.SetAnimation("charge", false); // aseguramos que no quede activa
        player.speed = 1f;
    }

    public void Exit()
    {
        player.SetAnimation("walk", false);
        player.SetAnimation("charge", false);
    }

    public void FixedUpdate()
    {
        float h = player.horizontal;
        

        // Movimiento horizontal si está en el suelo y no está cargando salto
        if (player.grounded && player.jumpValue == 0f)
        {
            player.rb2d.linearVelocity = new Vector2(h * player.speed, player.rb2d.linearVelocity.y);
            player.SetAnimation("walk", h != 0);
            
        }
       
    }

    public void Update()
    {

        // Transición a cargar salto
        if (Input.GetKeyDown(KeyCode.W) && player.grounded)
        {
            player.SetAnimation("walk", false); // desactiva animación walk al cargar
            player.TransitionToState(new ChargingJumpState());
        }
    }
}
