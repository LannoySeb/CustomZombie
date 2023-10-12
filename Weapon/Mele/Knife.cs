using Godot;
using System;

public partial class Knife : BaseWeapon
{

        public int Damage = 1;
        public override void _Ready()
    {
        CommonSetup();
        
        GD.Print("Setup");
        
        AmmoMax = 1;
        AmmoLeft = 1;
        ChargerSize = 1;
        AmmoLeftInCharger = 1;
        ReloadTime = 1;
    }
    public override void LoadBullet()
    {
        // throw new NotImplementedException();
    }

    public override void Shoot(Node parent, Vector2 velocity)
    {
        //
    }

    public void OnBodyEntered(Node node){
        if(node.IsInGroup("Ennemies")){
            var zombie = (zombie)node;
            zombie.TakeDamage(Damage);
        }
    }

}
