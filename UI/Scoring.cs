using Godot;
using System;

public partial class Scoring : Label
{
    private PlayerGlobals PlayerGlobal{get;set;}

    public override void _Ready()
    {
        PlayerGlobal = GetNode<PlayerGlobals>("/root/PlayerGlobals");
    }
    public override void _Process(double delta)
    {
        Text = PlayerGlobal.ScoreLeft.ToString();
    }
}
