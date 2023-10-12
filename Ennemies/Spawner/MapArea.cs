using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class MapArea : Area2D
{
    public bool IsActive { get; set; } = false;

    public bool IsOpen { get; set; }

    [Export]
    private CollisionShape2D Area { get; set; }

    public List<Node2D> SpawnLocations { get; set; }

    public List<InteractablesBase> Doors { get; set; }

    public override void _Ready()
    {
        Area = GetNode<CollisionShape2D>("CollisionShape2D");
        SpawnLocations = GetChildren()
        .Where(child => child is spawner_location)
        .Cast<Node2D>()
        .ToList();

        Doors = GetChildren()
        .Where(child => child.IsInGroup("Door"))
        .Cast<InteractablesBase>()
        .ToList();

        IsOpen = Doors.Count == 0;
    }

    public void OnBodyEntered(Node2D body)
    {

        if (body.IsInGroup("Player") && IsOpen)
        {
            IsActive = true;
        }
    }

    public void OnBodyExited(Node2D body)
    {
        if (body.IsInGroup("Player"))
        {
            IsActive = false;
        }
    }
}
