using Godot;
using System;

public partial class FamasShop : InteractablesBase
{
    [Export]
    private Famas WeaponType{get;set;}

    private PackedScene Weapon{get;set;}

    public override void _Ready()
    {
        base._Ready();
        Weapon = GD.Load<PackedScene>("res://Weapon/Rafale/Famas.tscn");
        SetProcess(false);
        InteractionText.Hide();
    }

    public void OnBodyEntered(Node2D body){
        PlayerEntered(body);
    }

    public void OnBodyExited(Node2D body){
        PlayerExited(body);
    }

    protected override void SetTextLabel()
    {
            InteractionText.Text = Price + " Points";
    }
    protected override void Interact()
    {
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
