using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public Material FOWMaterial;

    public Texture2D texBuffer;
    public RenderTexture FogRenderTexture;

    public Color32[] FogColors;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        FOWMaterial = SpriteRenderer.material;

        FogColors = new Color32[262144];

        for (int i = 0; i < 262144; i++)
        {
            FogColors[i] = new Color32(0, 0, 0, 255);
        }

        for (int i = 0; i < 10000; i++)
        {
            FogColors[i] = new Color32(1, 1, 1, 0);
        }
        texBuffer = new Texture2D(512, 512, TextureFormat.ARGB32, false);
        texBuffer.SetPixels32(FogColors);
        texBuffer.Apply();


        FogRenderTexture = RenderTexture.GetTemporary(512,512,0);
        Graphics.Blit(texBuffer,FogRenderTexture);
    }

    // Update is called once per frame
    void Update()
    {
        if (FogRenderTexture != null)
        {
            FOWMaterial.SetTexture("_RenderTex", FogRenderTexture);
        }
    }
}
