using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSettings
{
    public string worldName;
    public int worldSeed;

    public Difficulty gameDifficulty;

    public string mapType;

    static WorldSettings dataInstance = new WorldSettings();
    public static WorldSettings Instance{get {return dataInstance;}}
    NoiseSettings settings;

    WorldSettings()
    {
        worldSeed = 1337;
        mapType = "Continues";
        gameDifficulty = (Difficulty)ScriptableObject.CreateInstance("Difficulty");
        StartingResources sr = new StartingResources();
        sr.gold = 1000;
        sr.metal = 1000;
        sr.stone = 1000;
        sr.wood = 1000;
        gameDifficulty.aiResources = sr;
        gameDifficulty.playerResources = sr;

    }
    public void setWorldName(string name)
    {
        worldName = name;
    }

    public void setWorldSeed(int seed)
    {
        worldSeed = seed;
    }

    public void setGameDifficulty(Difficulty difficulty)
    {
        gameDifficulty = difficulty;
    }

    public void setmapType(string type)
    {
        mapType = type;
    }

    public void setWorldType(NoiseSettings setting)
    {
        settings = setting;
    }

    public int getSeed()
    {
        return worldSeed;
    }

    public Difficulty GetDifficulty()
    {
        return gameDifficulty;
    }

    public string GetMapType()
    {
        return mapType;
    }

    public NoiseSettings GetNoiseSettings()
    {
        return settings;
    }

}
