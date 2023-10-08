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

    protected bool IsReloading{get;set;} = false;

    private ReloadBar ReloadBar{get;set;}

    public Sprite2D Skin{get;set;}

	protected Marker2D Bulletspawner{get;set;}


    public override void _Ready()
    {
        Skin = GetNode<Sprite2D>("Skin");
		Bulletspawner = GetNode<Marker2D>("AimingNode");

        LoadBullet();
    }
    public abstract void Shoot(Node parent, Vector2 velocity);

    public async void Reload(){
        if(AmmoLeft>0){
            IsReloading = true;

            GD.Print("RELOAD");
            
            ReloadBar.ReloadAnimation(ReloadTime);
            await Task.Delay(TimeSpan.FromSeconds(ReloadTime));
            
            AmmoLeft= AmmoLeft-(ChargerSize-AmmoLeftInCharger);
            if((ChargerSize-AmmoLeftInCharger)<AmmoLeft){
                AmmoLeftInCharger = AmmoLeftInCharger + (ChargerSize - AmmoLeftInCharger);
            }else{
                AmmoLeftInCharger = AmmoLeft;
            }
            
            GD.Print("RELOADED");
            
            IsReloading = false;
        }
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