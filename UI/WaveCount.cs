using Godot;
using System;

public partial class WaveCount : Label
{
    private WavesGlobals WavesGlobals{get;set;}

    public override void _Ready()
    {
        WavesGlobals = GetNode<WavesGlobals>("/root/WavesGlobals");
    }
    public override void _Process(double delta)
    {
        Text = "Wave\n" + WavesGlobals.WaveCount.ToString();
    }
}
