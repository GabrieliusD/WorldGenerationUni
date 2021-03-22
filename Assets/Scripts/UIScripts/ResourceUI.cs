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

    // Update is called once per frame
    void Update()
    {
        WoodText.text = "Wood: " + ResourceManager.Instance.GetWood(PlayerTypes.humanPlayer);
        StoneText.text = "Stone: " + ResourceManager.Instance.GetStone(PlayerTypes.humanPlayer);
        MetalText.text = "Metal: " + ResourceManager.Instance.GetMetal(PlayerTypes.humanPlayer);
        GoldText.text = "Gold: " + ResourceManager.Instance.GetGold(PlayerTypes.humanPlayer);
    }
}
