using System;
using Godot;

public static class WaveStats{
    public static int WaveCount {get;set;} = 1;

    public static int ZombiesWaveCount{get;set;} = 10;

    public static int ZombieMaxOnScreen {get;set;} = 10;

    public static int ZombieActive {get;set;} = 0;

    public static int ZombiesKilled{get;set;} = 0;

    public static int ZombieHealth {get;set;} = 1;

    public static float RoundBreak = 10;
    
    /// <summary>
    /// SetUp the NextWave
    /// </summary>
    public static void NextWave(){
        
        WaveCount++;
        
        if(ZombieHealth<50){
            ZombieHealth ++;
        }
        ZombiesWaveCount = (int)Math.Round(ZombiesWaveCount*1.1);
        if(ZombieMaxOnScreen <150){
            ZombieMaxOnScreen = (int)Math.Round(1.1);
        }
        ZombiesKilled = 0;
    }

    /// <summary>
    /// Can we make spawn a new Zombie.
    /// </summary>
    /// <returns></returns>
    public static bool CanZombieSpawn(){
        return ZombieActive < ZombieMaxOnScreen && ZombiesKilled + ZombieActive < (ZombiesWaveCount);
    }

    /// <summary>
    /// Actualize data on kill. 
    /// </summary>
    public static void ZombieKilled(){
        ZombiesKilled ++;
        ZombieActive --;

        if(ZombiesKilled == ZombiesWaveCount){
            NextWave();
        }
    }
}