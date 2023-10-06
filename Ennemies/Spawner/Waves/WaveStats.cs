using System;
using Godot;

public static class WaveStats{
    public static int WaveCount {get;set;} = 1;

    public static int ZombiesWaveCount{get;set;} = 5;

    public static int ZombieMaxOnScreen {get;set;} = 5;

    public static int ZombieActive {get;set;} = 0;

    public static int ZombiesKilled{get;set;} = 0;

    public static int ZombieHealth {get;set;}

    public static float RoundBreak = 10;
    
    public static void NextWave(){
        
        WaveCount++;
        ZombiesWaveCount = (int)Math.Round(ZombiesWaveCount*1.1);
        ZombiesKilled = 0;

        GD.Print("New current wave : " + WaveCount);
    }

    public static bool CanZombieSpawn(){
        return ZombieActive < ZombieMaxOnScreen && ZombiesKilled + ZombieActive < (ZombiesWaveCount);
    }

    public static void ZombieKilled(){
        ZombiesKilled ++;
        ZombieActive --;

        if(ZombiesKilled == ZombiesWaveCount){
            NextWave();
        }
    }
}