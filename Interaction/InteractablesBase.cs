using Godot;
using System;

public abstract partial class InteractablesBase : Node2D
{
    protected player_roger Player { get; set; }

    protected PlayerGlobals PlayerGlobals { get; set; }

    protected Label InteractionText { get; set; }

    [Export]
    protected MapArea Area { get; set; }

    protected int Price { get; set; } = 100;

    public override void _Ready()
    {
        InteractionText = GetNode<Label>("InteractionText");
        PlayerGlobals = GetNode<PlayerGlobals>("/root/PlayerGlobals");

        SetTextLabel();
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Interaction"))
        {
            Interact();
        }

    }

    protected abstract void Interact();
    protected abstract void SetTextLabel();
    protected void PlayerEntered(Node2D body)
    {
        if (body.IsInGroup("Player"))
        {
            SetProcess(true);
            InteractionText.Show();
            Player = (player_roger)body;
        }
    }

    protected void PlayerExited(Node2D body)
    {
        if (body.IsInGroup("Player"))
        {
            SetProcess(false);
            Player = null;
            InteractionText.Hide();
            SetTextLabel();
        }
    }

}