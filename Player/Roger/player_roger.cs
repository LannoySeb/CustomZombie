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

	private BaseWeapon Weapon{get;set;}

	private ReloadBar ReloadBar{get;set;}

	private AnimationTree AnimationTree{get;set;}
	private AnimationNodeStateMachinePlayback StateMachine{get;set;}

    public override void _Process(double delta)
    {
		if(Input.IsActionJustPressed("Reloading")){
			Weapon.Reload();
		}
		if(Input.IsActionJustPressed("Shoot")){
			Shoot();
		}
		AimingNode.LookAt(GetGlobalMousePosition());
		
		Weapon.GunOrientation(GetLocalMousePosition());
		
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

	/// <summary>
	/// Set Animation parameters.
	/// </summary>
	/// <param name="MoveInput">User</param>
	public void UpdateAnimationParameters(Vector2 MoveInput){
		if(MoveInput != Vector2.Zero){
			AnimationTree.Set("parameters/Walk/blend_position", MoveInput);
			// Will be used if a add other idle animation.
			AnimationTree.Set("parameters/Idle/blend_position", MoveInput);
		}
	}

	/// <summary>
	/// Set animation states
	/// </summary>
	public void PickNewState(){
		if(Velocity != Vector2.Zero){
			StateMachine.Travel("Walk");
		}else{
			StateMachine.Travel("Idle");
		}
	}

	/// <summary>
	/// Shoot a bullet.
	/// </summary>
	public void Shoot()
	{
		var position = Bulletspawner.GlobalPosition;
		var velocity = GlobalPosition.DirectionTo(GetGlobalMousePosition());
		var rotation = Bulletspawner.GlobalRotation;

		Weapon.Shoot(GetParent(), position,velocity,rotation);
	}

    public override void _Ready()
    {
        AnimationTree = GetNode<AnimationTree>("AnimationTree");
		Bulletspawner = AimingNode.GetNode<Marker2D>("AimingNode");

		ReloadBar = GetNode<ReloadBar>("ReloadBar");

		Weapon = (Pistol)Bulletspawner.GetNode<Node2D>("Pistol");
		Weapon.SetLoader(ReloadBar);
		AnimationTree.Set("parameters/Idle/blend_position",StartingDirection);

		StateMachine = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");
		Bullet = GD.Load<PackedScene>("res://Weapon/Bullets/SimpleBullet.tscn");
    }
}

