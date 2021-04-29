using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Building", menuName = "ScriptableObjects/Building", order = 1)]
public class Building : ScriptableObject
{
    public GameObject buildingType;
    new public string name;
    public int Health;
    public int WoodCost;
    public int StoneCost;

}
