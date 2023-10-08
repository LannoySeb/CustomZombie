using Godot;
using System;

public partial class HideableArea : Node2D
{
    public void OnBodyEntered(Node2D body){
        if(body.IsInGroup("Player")){
            Hide();
        }
    }

    public void OnBodyExited(Node2D body){
        if(body.IsInGroup("Player")){
            Show();
        }
    }
}
