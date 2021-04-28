using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientMap
{
    static float[,] circleMap;
    public static void GenerateCircleGradient(int width, int height, float maskThreshold)
    {
        circleMap = new float[width, height];
        Vector2 maskCentre = new Vector2(width/ 2.0f, height/2.0f);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float distance = Vector2.Distance(maskCentre, new Vector2(x, y));
                float pixel = (0.5f - (distance / width)) * maskThreshold;
                pixel = 1.0f - pixel;

                circleMap[x,y] = pixel;
            }
        }

    }

    public static void GenerateIslandMask(int width, int height, float maskThreshold)
    {
        circleMap = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float distanceX = Mathf.Abs(x - width * 0.5f );
                float distanceY = Mathf.Abs(y - height * 0.5f);

                float distance = Mathf.Sqrt(distanceX*distanceX + distanceY*distanceY);
                float maxWidth = width * 0.5f + maskThreshold;
                float delta = distance / maxWidth;
                float gradient = delta*delta;

                circleMap[x, y] = 1 - gradient;
            }
        }


        
    }

    public static void GenerateSquareIslandMask(int width, int height, float maskThreshold)
    {
        circleMap = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float distanceX = Mathf.Abs(x - width * 0.5f );
                float distanceY = Mathf.Abs(y - height * 0.5f);

                float distance = Mathf.Max(distanceX, distanceY);

                float maxWidth = width * 0.5f + maskThreshold;
                float delta = distance / maxWidth;
                float gradient = delta*delta;

                circleMap[x, y] = 1 - gradient;
                
            }
        }
    }

    public static float sampleGradient(float pointX, float pointY)
    {
        //0..1
        int samplePointX = (int)(pointX * (circleMap.GetLength(0)-1));
        int samplePointY = (int)(pointY * (circleMap.GetLength(1)-1));
        return circleMap[samplePointX, samplePointY];
    }

    public static float[,] GetMask()
    {
        return circleMap;
    }
}
