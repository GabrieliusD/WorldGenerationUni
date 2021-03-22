using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGen
{
    ColorSettings settings;
    Texture2D texture;
    const int textureRes = 100;
    public void UpdateSettings(ColorSettings settings)
    {
        this.settings = settings;
        if(texture == null)
        texture = new Texture2D(textureRes, 1);
    }

    public void UpdateHeight(MinMaxTracker heightTracker)
    {
        settings.terrainMaterial.SetVector("_heightMinMax", new Vector4(heightTracker.Min, heightTracker.Max,0,0));
    }

    public void UpdateColors()
    {
        Color[] colors = new Color[textureRes];
        for (int i = 0; i < textureRes; i++)
        {
            colors[i] = settings.gradient.Evaluate(i / (textureRes - 1f));
        }
        texture.SetPixels(colors);
        texture.Apply();
        settings.terrainMaterial.SetTexture("_texture", texture);
    }
}
