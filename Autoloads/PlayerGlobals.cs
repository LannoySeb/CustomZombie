using Godot;
using System;

public partial class PlayerGlobals : Node
{
    public int Score {get;set;} =0;

    public void AddScore(int score){
        Score += score;
    }
}
