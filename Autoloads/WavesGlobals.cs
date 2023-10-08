using Godot;
using System;

public partial class WavesGlobals : Node
{
    public int WaveCount {get;set;} = 1;

    public int ZombiesWaveCount{get;set;} = 10;

    public int ZombieMaxOnScreen {get;set;} = 10;

    public int ZombieActive {get;set;} = 0;

    public int ZombiesKilled{get;set;} = 0;

    public int ZombieHealth {get;set;} = 1;

    public float RoundBreak = 10;
    
    /// <summary>
    /// SetUp the NextWave
    /// </summary>
    public void NextWave(){
        WaveCount++;
        
        if(ZombieHealth<50){
            ZombieHealth ++;
        }
        ZombiesWaveCount = (int)Math.Round(ZombiesWaveCount*1.1);
        if(ZombieMaxOnScreen <150){
            ZombieMaxOnScreen = (int)Math.Round(1.1*ZombieMaxOnScreen);
        }
        ZombiesKilled = 0;
        GD.Print("Wave: " + WaveCount + "Max on screen: " + ZombieMaxOnScreen );

    }

    /// <summary>
    /// Can we make spawn a new Zombie.
    /// </summary>
    /// <returns></returns>
    public bool CanZombieSpawn(){
        return ZombieActive < ZombieMaxOnScreen && ZombiesKilled + ZombieActive < (ZombiesWaveCount);
    }

    /// <summary>
    /// Actualize data on kill. 
    /// </summary>
    public void ZombieKilled(){
        ZombiesKilled ++;
        ZombieActive --;

        if(ZombiesKilled == ZombiesWaveCount){
            NextWave();
        }
    }
}
