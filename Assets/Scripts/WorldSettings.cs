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

}
