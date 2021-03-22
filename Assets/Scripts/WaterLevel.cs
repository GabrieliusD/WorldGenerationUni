using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    public static WaterLevel Instance{get; private set;}

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public float GetWaterLevel()
    {
        return Instance.transform.position.y;
    }
}
