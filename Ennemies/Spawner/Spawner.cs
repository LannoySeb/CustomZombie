using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Spawner : Node2D
{
    [Export]
    private zombie ZombieType {get;set;}

    private PackedScene Zombie{get;set;}

    private List<Node2D> SpawnerLocations{get;set;}= new List<Node2D>();

    private WavesGlobals WavesGlobal{get;set;}



    public override void _Ready()
    {
        WavesGlobal = GetNode<WavesGlobals>("/root/WavesGlobals");
        Zombie = GD.Load<PackedScene>("res://Ennemies/Zombie/zombie.tscn");

    }

    public override void _Process(double delta)
    {
        LoadActiveSpawner();
    }

    public void SpawnZombie(){

        if(WavesGlobal.CanZombieSpawn() && SpawnerLocations.Count>0){
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

    private void LoadActiveSpawner(){
        var spawners  =  new List<Node2D>();
        var areas = GetChildren()
        .Where(child => child.IsInGroup("MapArea"))
        .Where(child => ((MapArea)child).IsActive);
        GD.Print(GetChildren().Count);
        foreach (var child in areas)
        {
            var area = (MapArea) child;
            spawners.AddRange( area.SpawnLocations);
        GD.Print(spawners.Count);

        }
        

        SpawnerLocations = spawners;
    }
}
