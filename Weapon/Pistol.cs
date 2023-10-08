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
        CommonSetup();

        AmmoMax = 120;
        AmmoLeft = 120;
        ChargerSize = 10;
        AmmoLeftInCharger = 10;
        ReloadTime = 2;
    }

    public override void Shoot(Node parent, Vector2 velocity){
        
        if( !IsReloading && AmmoLeftInCharger > 0){
            var bulletInstance = Bullet.Instantiate<SimpleBullet>();
            parent.AddChild(bulletInstance);

            bulletInstance.Position = Bulletspawner.GlobalPosition;
            bulletInstance.Velocity = velocity;
            bulletInstance.Rotation = Bulletspawner.GlobalRotation;

            AmmoLeftInCharger--;
            GD.Print(AmmoLeftInCharger + "/" + ChargerSize);
        }else{
            if(!IsReloading){
                Reload();
            }
        }
    }
}
