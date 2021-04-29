using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class ChangeSceneToWorld : MonoBehaviour
{
    public InputField WorldName;
    public InputField worldSeed;
    public Dropdown difficulty;
    public Dropdown mapType;

    public Dropdown worldType;

    public Difficulty[] difficulties;
    public List<NoiseSettings> noiseSettings;

    WorldSettings worldSettings;
    void Start()
    {
        
        worldSettings = WorldSettings.Instance;
        System.Random random = new System.Random(DateTime.Now.Millisecond);
        worldSeed.text = random.Next(1, 99999999).ToString();
        SetupWorldTypeDropdown();
    }

    void SetupWorldTypeDropdown()
    {
        List<string> options = new List<string>();
        foreach (var setting in noiseSettings)
        {
            options.Add(setting.terrainName);
        }
        worldType.AddOptions(options);
    }

    NoiseSettings GetNameFromWorldList()
    {
        foreach (var setting in noiseSettings)
        {
            if(setting.terrainName == worldType.options[worldType.value].text)
            {
                return setting;
            }
        }
        return noiseSettings[0];
    }

    public Difficulty GetDifficultyFromArray(string difficulty)
    {
        foreach (var diff in difficulties)
        {
            if(diff.name == difficulty)
            {
                return diff;
            }
        }
        return difficulties[0];
    }
    public void CreateWorld()
    {
        worldSettings.setWorldName(WorldName.text);
        int seed = Int32.Parse(worldSeed.text);
        worldSettings.setWorldSeed(seed);
        worldSettings.setGameDifficulty(GetDifficultyFromArray(difficulty.options[difficulty.value].text));
        worldSettings.setWorldType(GetNameFromWorldList());

        SceneManager.LoadScene("Scenes/SampleScene");
        
    }
}
