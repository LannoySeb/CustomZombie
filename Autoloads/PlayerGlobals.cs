using Godot;
using System;

public partial class PlayerGlobals : Node
{
    public int Score {get;private set;} =0;
    public int ScoreLeft {get;private set;} = 0;

    public void AddScore(int score){
        Score += score;
        ScoreLeft+= score;
    }

    public void SpendPoints(int points){
        ScoreLeft-= points;
    }

    public bool CanPay(int points){
        GD.Print(points);
        return ScoreLeft>= points;
    }
}
