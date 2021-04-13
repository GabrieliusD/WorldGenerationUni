using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StartingResources
{
    public int wood;
    public int stone;
    public int metal;
    public int gold;
}
[System.Serializable]
public struct AIParameters
{
    public float TimeBetweenAttacks;
    public int startingUnitSize;
    public int unitIncreaseBetweenTime;
    public int extraUnitsForDefence;
}

[CreateAssetMenu(fileName = "Difficulty", menuName = "")]
public class Difficulty : ScriptableObject
{
    public string difficultyName;
    public StartingResources playerResources;
    public StartingResources aiResources;
    public AIParameters aIParameters;
    
}
