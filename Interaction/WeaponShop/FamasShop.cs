using Godot;
using System;

public partial class FamasShop : Node2D
{
    [Export]
    private Famas WeaponType{get;set;}

    private PackedScene Weapon{get;set;}

    player_roger Player{get;set;}

    public override void _Ready()
    {
        base._Ready();
        Weapon = GD.Load<PackedScene>("res://Weapon/Rafale/Famas.tscn");

        SetProcess(false);
    }

    public void OnBodyEntered(Node2D body){
        if(body.IsInGroup("Player")){
            SetProcess(true);
            GD.Print("Can buy famas");
            Player = (player_roger)body;
        }
    }

    public void OnBodyExited(Node2D body){
         if(body.IsInGroup("Player")){
            SetProcess(false);
            GD.Print("Leave shop");
            Player = null;
        }
    }

    public override void _Process(double delta)
    {
        if(Input.IsActionJustPressed("Interaction")){
            var famas = Weapon.Instantiate<Famas>();
            Player.SwapWeapon(famas);
        }
    }

}
