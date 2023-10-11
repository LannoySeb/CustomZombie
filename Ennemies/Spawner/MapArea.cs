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

        GD.Print(Name + " door count" + Doors.Count);
        IsOpen = Doors.Count == 0;
        GD.Print(IsOpen);
    }

    public void OnBodyEntered(Node2D body)
    {

        if (body.IsInGroup("Player") && IsOpen)
        {
            GD.Print(Name + " Area Active");
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
