using Godot;
using System;

public partial class FamasShop : Node2D
{
    [Export]
    private Famas WeaponType{get;set;}

    private PackedScene Weapon{get;set;}

    private Label InteractionText{get;set;}

    private player_roger Player{get;set;}

    private PlayerGlobals PlayerGlobals{get;set;}

    private int Price{get;set;} = 100;

    public override void _Ready()
    {
        base._Ready();
        Weapon = GD.Load<PackedScene>("res://Weapon/Rafale/Famas.tscn");
        InteractionText = GetNode<Label>("InteractionText");
        PlayerGlobals = GetNode<PlayerGlobals>("/root/PlayerGlobals");
        
        InteractionText.Text = Price + " Points";

        SetProcess(false);
        InteractionText.Hide();
    }

    public void OnBodyEntered(Node2D body){
        if(body.IsInGroup("Player")){
            SetProcess(true);
            InteractionText.Show();
            Player = (player_roger)body;
        }
    }

    public void OnBodyExited(Node2D body){
         if(body.IsInGroup("Player")){
            SetProcess(false);
            Player = null;
            InteractionText.Hide();
            InteractionText.Text = Price + " Points";
        }
    }

    public override void _Process(double delta)
    {
        if(Input.IsActionJustPressed("Interaction")  ){

            if(PlayerGlobals.CanPay(Price)){
                var famas = Weapon.Instantiate<Famas>();
                PlayerGlobals.SpendPoints(Price);
                Player.SwapWeapon(famas);
            }
            else{
                InteractionText.Text = "Not enought Points";
            }
        }

    }

}
