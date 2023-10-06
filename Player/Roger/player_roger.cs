using Godot;
using System;

public partial class player_roger : CharacterBody2D
{
	[Export]
	private float MoveSpeed {get;set;} = 100;

	[Export]
	private Vector2 StartingDirection {get;set;} = new Vector2(0,1);

	[Export]
	private Node2D AimingNode {get;set;}
	private Marker2D Bulletspawner{get;set;}
	private PackedScene Bullet{get;set;}

	private Sprite2D WeaponSkin{get;set;}

	private AnimationTree AnimationTree{get;set;}
	private AnimationNodeStateMachinePlayback StateMachine{get;set;}

    public override void _Process(double delta)
    {
		if(Input.IsActionJustPressed("Shoot")){
			Shoot();
		}
		AimingNode.LookAt(GetGlobalMousePosition());
		
		var gunNeedRotation = GetLocalMousePosition();
		if(gunNeedRotation.X < 0){
			WeaponSkin.FlipV = true;
		}else{
			WeaponSkin.FlipV = false;
		}
		
    }
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

	public void Shoot()
	{
		var bulletInstance = Bullet.Instantiate<SimpleBullet>();
		GetParent().AddChild(bulletInstance);

		bulletInstance.Position = Bulletspawner.GlobalPosition;
		bulletInstance.Velocity = Bulletspawner.GlobalPosition.DirectionTo(GetGlobalMousePosition());
		bulletInstance.Rotation = Bulletspawner.GlobalRotation;
	}

    public override void _Ready()
    {
        AnimationTree = GetNode<AnimationTree>("AnimationTree");
		Bulletspawner = AimingNode.GetNode<Marker2D>("AimingNode");
		WeaponSkin = Bulletspawner.GetNode<Sprite2D>("Weapon");
		AnimationTree.Set("parameters/Idle/blend_position",StartingDirection);

		StateMachine = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");
		Bullet = GD.Load<PackedScene>("res://Weapon/Bullets/SimpleBullet.tscn");
    }

}

