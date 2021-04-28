using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResourceUI : MonoBehaviour
{
    public Text WoodText;
    public Text StoneText;
    public Text MetalText;
    public Text GoldText;

    ResourceManager rm;
    private void Start()
    {
        rm = ResourceManager.Instance;
    }
    void Update()
    {

        WoodText.text = "Wood: " + rm.GetWood(PlayerTypes.humanPlayer) + "/"+rm.GetMaxWood(PlayerTypes.humanPlayer);
        StoneText.text = "Stone: " +rm.GetStone(PlayerTypes.humanPlayer)+ "/"+rm.GetMaxStone(PlayerTypes.humanPlayer);
        MetalText.text = "Metal: " +rm.GetMetal(PlayerTypes.humanPlayer)+ "/"+rm.GetMaxMetal(PlayerTypes.humanPlayer);
        GoldText.text = "Gold: " +rm.GetGold(PlayerTypes.humanPlayer);
    }
}
