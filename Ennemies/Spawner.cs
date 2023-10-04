using Godot;
using System;

public partial class Spawner : Node2D
{
    [Export]
    private zombie ZombieType {get;set;}

    private PackedScene Zombie{get;set;}

    public override void _Ready()
    {
        Zombie = GD.Load<PackedScene>("res://Ennemies/Zombie/zombie.tscn");
    }
    public override void _Process(double delta)
    {
        
    }

    public void SpawnZombie(){
        var zombie = Zombie.Instantiate<zombie>();
        GetParent().AddChild(zombie);
        zombie.Player = GetParent().GetNode<player_roger>("PlayerRoger");
        zombie.Position = Position;
        zombie._Ready();

    }

    private void OnTimerTimeout(){
        GD.Print("Spawn");
        SpawnZombie();
    }
}
