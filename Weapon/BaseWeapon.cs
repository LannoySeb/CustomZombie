using System;
using System.Threading.Tasks;
using Godot;

public abstract partial class BaseWeapon : Node2D{
    
	public PackedScene Bullet{get;set;}

    public int AmmoMax{get;set;}

    public int ChargerSize {get;set;}

    public int AmmoLeft{get;set;}

    public int AmmoLeftInCharger{get;set;}
    public int ReloadTime{get;set;}

    private bool IsReloading{get;set;} = false;

    private ReloadBar ReloadBar{get;set;}

    public Sprite2D Skin{get;set;}

    public override void _Ready()
    {
        Skin = GetNode<Sprite2D>("Skin");
        LoadBullet();
    }
    public void Shoot(Node parent, Vector2 position, Vector2 velocity, float rotation){
        
        if( AmmoLeftInCharger > 0){
            var bulletInstance = Bullet.Instantiate<SimpleBullet>();
            parent.AddChild(bulletInstance);

            bulletInstance.Position = position;
            bulletInstance.Velocity = velocity;
            bulletInstance.Rotation = rotation;

            AmmoLeftInCharger--;
            GD.Print(AmmoLeftInCharger + "/" + ChargerSize);
        }else{
            if(!IsReloading){
                Reload();
            }
        }
    }

    public async void Reload(){
        IsReloading = true;
        GD.Print("RELOAD");
        ReloadBar.ReloadAnimation(ReloadTime);
        await Task.Delay(TimeSpan.FromSeconds(ReloadTime));
        AmmoLeft-=ChargerSize;
        AmmoLeftInCharger = ChargerSize;
        GD.Print("RELOADED");
        IsReloading = false;
    }

    /// <summary>
	/// Flip the gun skin according to the mouse position around the char.
	/// </summary>
	public void GunOrientation(Vector2 aimingPosition){
		if(aimingPosition.X < 0){
			Skin.FlipV = true;
		}else{
			Skin.FlipV = false;
		}
	}

    public void SetLoader(ReloadBar reloadBar){
        ReloadBar = reloadBar;
    }

    public abstract void LoadBullet();

}