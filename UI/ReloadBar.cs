using Godot;
using System;

public partial class ReloadBar : Control
{
    public Timer Timer{get;set;}

    public ProgressBar ProgressBar{get;set;}

    public override void _Ready()
    {
        Timer = GetNode<Timer>("Timer");
        ProgressBar = GetNode<ProgressBar>("ProgressBar");
        ProgressBar.Hide();
    }

    public void ReloadAnimation(int timeNeeded){
        ProgressBar.Show();
        ProgressBar.MaxValue = timeNeeded;
        Timer.WaitTime = timeNeeded;
        Timer.Start();
    }

    public void OnTimerTimeout(){
        Timer.Stop();
        ProgressBar.Hide();
    }

    public override void _Process(double delta)
    {
        
        ProgressBar.Value = ProgressBar.MaxValue - Timer.TimeLeft;
    }
}
