using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Spawner : Node2D
{
    [Export]
    private zombie ZombieType {get;set;}

    private PackedScene Zombie{get;set;}

    private List<Node2D> SpawnerLocations{get;set;}

    private WavesGlobals WavesGlobal{get;set;}



    public override void _Ready()
    {
        WavesGlobal = GetNode<WavesGlobals>("/root/WavesGlobals");
        Zombie = GD.Load<PackedScene>("res://Ennemies/Zombie/zombie.tscn");
        SpawnerLocations = GetChildren()
        .Where(child => child is spawner_location)
        .Cast<Node2D>()
        .ToList();
    }

    public override void _Process(double delta)
    {
        
    }

    public void SpawnZombie(){

        if(WavesGlobal.CanZombieSpawn()){
            var zombie = Zombie.Instantiate<zombie>();

            GetParent().AddChild(zombie);

            zombie.Player = GetParent().GetNode<player_roger>("PlayerRoger");
            zombie.Position = GetSpawn();
            zombie.SetupZombie();
            zombie._Ready();
            
            WavesGlobal.ZombieActive++;
        }
    }

    public Vector2 GetSpawn(){
        var rnd = new Random().Next(SpawnerLocations.Count);
    
        return SpawnerLocations[rnd].GlobalPosition;
    }

    private void OnTimerTimeout(){
        SpawnZombie();
    }
}
