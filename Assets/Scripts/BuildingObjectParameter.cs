using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingObjectParameter : ObjectParameter
{
    public PlayerTypes playerTypes;

    public int woodCost;
    public int stoneCost;
    public int upgradeCost;
    public int GetWoodCost()
    {
        return woodCost;
    }

    public int GetStoneCost()
    {
        return stoneCost;
    }
}
