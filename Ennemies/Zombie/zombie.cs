using Godot;
using System;

public partial class zombie : CharacterBody2D
{    
    [Export]
	private float MoveSpeed {get;set;} = 100;

    [Export]
    public Node2D Player {get;set;}

    [Export]
	private Vector2 StartingDirection {get;set;} = new Vector2(0,1);

    [Export]
    private int Health {get;set;}

    private Timer PathFindTimer {get;set;}


    private NavigationAgent2D NavAgent{get;set;}

	private AnimationTree AnimationTree{get;set;}
	private AnimationNodeStateMachinePlayback StateMachine{get;set;}

    private PlayerGlobals PlayerGlobal{get;set;}
    private WavesGlobals WavesGlobal{get;set;}


    public override void _Ready()
    {
        PlayerGlobal = GetNode<PlayerGlobals>("/root/PlayerGlobals");
        WavesGlobal = GetNode<WavesGlobals>("/root/WavesGlobals");

        NavAgent = GetNode<NavigationAgent2D>("NavigationAgent2D");

        AnimationTree = GetNode<AnimationTree>("AnimationTree");
		AnimationTree.Set("parameters/Idle/blend_position",StartingDirection);

		StateMachine = (AnimationNodeStateMachinePlayback)AnimationTree.Get("parameters/playback");

        MakePath();
    }

    public override void _PhysicsProcess(double delta)
    {
        var direction = ToLocal(NavAgent.GetNextPathPosition()).Normalized();
        
        Velocity = direction * MoveSpeed;

        UpdateAnimationParameters(direction);

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

    public void SetupZombie(){
        Health = WavesGlobal.ZombieHealth;
    }

    public void TakeDamage(int damage){
        Health-=damage;

        if(Health<=0){
            WavesGlobal.ZombieKilled();

            QueueFree();
            
            PlayerGlobal.AddScore(10);
        }
    } 

    private void MakePath(){

        if(Player!= null){
            NavAgent.TargetPosition = Player.GlobalPosition;
        }
    }

    private void OnTimerTimeout(){
        MakePath();
    }

    public void ReceiveKnockBack(Vector2 sourcePos, float knStrength){
        var knDirection = sourcePos.DirectionTo(GlobalPosition);
        var kn = knDirection*knStrength;

        GlobalPosition += kn;
    }
}
