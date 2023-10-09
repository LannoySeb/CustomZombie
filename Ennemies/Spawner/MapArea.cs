using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class MapArea : Area2D
{
    public bool IsActive { get; set; } = false;

    [Export]
    private CollisionShape2D Area { get; set; }

    public List<Node2D> SpawnLocations { get; set; }

    public override void _Ready()
    {
        Area = GetNode<CollisionShape2D>("CollisionShape2D");
        SpawnLocations = GetChildren()
        .Where(child => child is spawner_location)
        .Cast<Node2D>()
        .ToList();
    }

    public void OnBodyEntered(Node2D body)
    {
        if (body.IsInGroup("Player"))
        {
            GD.Print("Area Active");
            IsActive = true;
        }
    }

    public void OnBodyExited(Node2D body)
    {
        if (body.IsInGroup("Player"))
        {
            GD.Print("Area Inactive");

            IsActive = false;
        }
    }
}
