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
    public void HasWater(bool enable)
    {
        Instance.gameObject.SetActive(enable);
    }
    public void SetWaterLevel(float waterlevel)
    {
        Vector3 pos = Instance.transform.position;
        pos = new Vector3(pos.x, waterlevel, pos.z);
    }

    public float GetWaterLevel()
    {
        return Instance.transform.position.y;
    }
}
