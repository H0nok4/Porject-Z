using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public interface IFOWUnit {
    
}

public class FOWCache {
    public bool IsDirty;
    public List<IntVec2> CurrentVisiblePos;
    public List<IntVec2> CachedVisiblePos;

    public void Update(IEnumerable<IntVec2> updatePos) {
        if (!IsDirty) {
            CachedVisiblePos.Clear();
            CachedVisiblePos.AddRange(CurrentVisiblePos);
            CurrentVisiblePos.AddRange(updatePos);
            IsDirty = true;
        }
        else {
            //TODO:同一Tick中刷新多次视野，不知道会不会有这种情况，先预防
            CurrentVisiblePos.Clear();
            CurrentVisiblePos.AddRange(updatePos);
        }
    }
}
public class FogManager : Singleton<FogManager>
{
    private SpriteRenderer SpriteRenderer;
    private Material FOWMaterial;
    private int _width;
    private int _height;
    private RenderTexture[] FogTexture;
    public Texture2D[] TextureBuffer;
    private Color32[][] FogColors;



    private bool IsDirty;
    private readonly Dictionary<IFOWUnit, FOWCache> _cachedFOWUnit = new Dictionary<IFOWUnit, FOWCache>(); 
    public void Init(int mapLayerCount,int width,int height) {
        SpriteRenderer = GameObject.Find("Fog").GetComponent<SpriteRenderer>();
        FOWMaterial = SpriteRenderer.material;
        _width = width;
        _height = height;
        //后面会有多层地图,需要多层迷雾
        FogTexture = new RenderTexture[mapLayerCount];
        FogColors = new Color32[mapLayerCount][];
        for (int i = 0; i < mapLayerCount ;i++)
        {
            FogTexture[i] = RenderTexture.GetTemporary(width, height, 0);
            FogColors[i] = new Color32[width * height];
            TextureBuffer[i] = new Texture2D(width, height,TextureFormat.ARGB32,false);
            for (int j = 0; j < FogColors[i].Length; j++) {
                FogColors[i][j] = new Color32(0,0,0,255);
            }
            
        }

        
    }

    public void Tick() {
        //TODO:被动式的去刷新
        if (!IsDirty) {
            return;
        }
        //TODO:更新战争迷雾
    }

    public int ToColorIndex(int x, int y)
    {
        return x + y * _height;
    }
}

