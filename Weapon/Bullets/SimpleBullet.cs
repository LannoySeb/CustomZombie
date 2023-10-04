using Godot;
using System;

public partial class SimpleBullet : Area2D
{
    public Vector2 Velocity {get;set;} = Vector2.Zero;
    private float BulletSpeed {get;set;} = 400;
    public override void _PhysicsProcess(double delta)
    {
        Position += Velocity.Normalized() * (float)delta * BulletSpeed;
    }

    private void OnProjectileBodyEntered(Node2D body){
        QueueFree();
    }
}
