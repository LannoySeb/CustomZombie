using Godot;
using System;
using System.Threading.Tasks;

public partial class Famas : BaseWeapon
{
     public override void LoadBullet()
    {
        Bullet = GD.Load<PackedScene>("res://Weapon/Bullets/SimpleBullet.tscn");
    }

    public override void _Ready()
    {
        base._Ready();

        AmmoMax = 300;
        AmmoLeft = 300;
        ChargerSize = 30;
        AmmoLeftInCharger = 30;
        ReloadTime = 3;
    }

       public async override void Shoot(Node parent, Vector2 velocity){
        
        if( !IsReloading && AmmoLeftInCharger > 0){
            for(int i =0; i<3; i++){
                
                var bulletInstance = Bullet.Instantiate<SimpleBullet>();
                parent.AddChild(bulletInstance);

                bulletInstance.Position =  Bulletspawner.GlobalPosition;
                bulletInstance.Velocity = velocity;
                bulletInstance.Rotation = Bulletspawner.GlobalRotation;

                AmmoLeftInCharger--;
                GD.Print(AmmoLeftInCharger + "/" + ChargerSize);
                
                await Task.Delay(TimeSpan.FromMilliseconds(100));
        }
        }
        else{
            if(!IsReloading){
                Reload();
            }
        }
    } 
}
