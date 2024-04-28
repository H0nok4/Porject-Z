using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class FogManager : Singleton<FogManager>
{
    private int _width;
    private int _height;
    public RenderTexture[] FogTexture;
    public Color32[][] FogColors;

    public void Init(int mapLayerCount,int width,int height)
    {
        _width = width;
        _height = height;
        //后面会有多层地图,需要多层迷雾
        FogTexture = new RenderTexture[mapLayerCount];
        FogColors = new Color32[mapLayerCount][];
        for (int i = 0; i < mapLayerCount ;i++)
        {
            FogTexture[i] = RenderTexture.GetTemporary(width, height, 0);
            FogColors[i] = new Color32[width * height];
        }
    }

    public int ToColorIndex(int x, int y)
    {
        return x + y * _height;
    }
}

