using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class FogManager : Singleton<FogManager>
{

    public RenderTexture[] FogTexture;
    public bool[][] VisibleFog;

    public void Init(int mapLayerCount)
    {

    }
}

