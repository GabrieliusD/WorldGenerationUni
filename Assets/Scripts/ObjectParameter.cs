using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectParameter : MonoBehaviour
{
    public float health;
    public int woodCost;
    public int stoneCost;
    public int upgradeCost;

    void Update()
    {
        if(health <= 0)
        GameObject.Destroy(this);
    }

    public void takeDamage(int amount)
    {
        health -= amount;
    }

    public int GetWoodCost()
    {
        return woodCost;
    }

    public int GetStoneCost()
    {
        return stoneCost;
    }
}
