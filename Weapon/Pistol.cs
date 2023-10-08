using Godot;
using System;

public partial class Pistol : BaseWeapon
{
    public override void LoadBullet()
    {
        Bullet = GD.Load<PackedScene>("res://Weapon/Bullets/SimpleBullet.tscn");
    }


    public override void _Ready()
    {
        base._Ready();

        AmmoMax = 120;
        AmmoLeft = 120;
        ChargerSize = 10;
        AmmoLeftInCharger = 10;
        ReloadTime = 2;
    }
}
