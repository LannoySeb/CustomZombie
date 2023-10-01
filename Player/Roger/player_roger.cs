using Godot;
using System;

public partial class player_roger : CharacterBody2D
{
	[Export]
	private float MoveSpeed {get;set;} = 100;

	public override void _PhysicsProcess(double delta){
		// get user direction.
        var inputDirection = new Vector2(
			Input.GetActionStrength("right") - Input.GetActionStrength("left"),
			Input.GetActionStrength("down") - Input.GetActionStrength("up"));
    
        Velocity = inputDirection * MoveSpeed;

        MoveAndSlide();
		
	}
}
