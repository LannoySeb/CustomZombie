using Godot;
using System;

public partial class player_roger : CharacterBody2D
{
	[Export]
	private float MoveSpeed {get;set;} = 100;

	[Export]
	private Vector2 StartingDirection {get;set;} = new Vector2(0,1);

	private AnimationTree AnimationTree{get;set;}
	private AnimationNodeStateMachinePlayback StateMachine{get;set;}

	public override void _PhysicsProcess(double delta){
		// get user direction.
        var inputDirection = new Vector2(
			Input.GetActionStrength("right") - Input.GetActionStrength("left"),
			Input.GetActionStrength("down") - Input.GetActionStrength("up"));

		UpdateAnimationParameters(inputDirection);

        Velocity = inputDirection * MoveSpeed;

        MoveAndSlide();

		PickNewState();
	}

	public void UpdateAnimationParameters(Vector2 MoveInput){
		if(MoveInput != Vector2.Zero){
			AnimationTree.Set("parameters/Walk/blend_position", MoveInput);
			// Will be used if a add other idle animation.
			AnimationTree.Set("parameters/Idle/blend_position", MoveInput);
		}
	}

	public void PickNewState(){
		if(Velocity != Vector2.Zero){
			StateMachine.Travel("Walk");
		}else{
			StateMachine.Travel("Idle");
		}
	}

    public override void _Ready()
    {
        AnimationTree = GetNode<AnimationTree>("AnimationTree");
		AnimationTree.Set("parameters/Idle/blend_position",StartingDirection);

		StateMachine = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");
    }

}

