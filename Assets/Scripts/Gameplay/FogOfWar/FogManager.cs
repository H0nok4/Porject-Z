using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public interface IFOWUnit {
    
}

public class FOWCache {
    public bool IsDirty;
    public int CurrentIndex;
    public List<IntVec2> CurrentVisiblePos;

    public int CachedIndex;
    public List<IntVec2> CachedVisiblePos;

    public void Update(int index,IEnumerable<IntVec2> updatePos) {
        if (!IsDirty)
        {
            CurrentIndex = index;
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

    private RenderTexture CurMapDataTexture => FogTexture[MapController.Instance.Map.ActiveIndex];

    private Map Map => MapController.Instance.Map;

    private Color32 VisitedColor = new Color32(0, 0, 0, 127);

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
        //TODO:先向之前保存的点刷新成半透明,然后再将现在的点刷新成透明
        RefreshCache();
        RefreshCurrent();
        //TODO:后面建筑物或者装饰需要留在RenderTexture上,可以后面加一张RenderTexture,然后用一个专门的FogCamera将场景渲染到上面,再叠加到Fog上
        //TODO:更新战争迷雾
        FOWMaterial.SetTexture("_RenderTex", CurMapDataTexture);
    }

    private void RefreshCache()
    {
        foreach (var fowCach in _cachedFOWUnit)
        {
            if (!fowCach.Value.IsDirty)
            {
                continue;
            }

            if (fowCach.Value.CachedVisiblePos == null)
            {
                continue;
            }

            foreach (var cachedPos in fowCach.Value.CachedVisiblePos)
            {
                FogColors[fowCach.Value.CachedIndex][ToColorIndex(cachedPos.X, cachedPos.Y)] = VisitedColor;
            }
        }
    }

    private void RefreshNew()
    {
        foreach (var fowCach in _cachedFOWUnit)
        {
            if (!fowCach.Value.IsDirty)
            {
                continue;
            }
        }
    }
    public int ToColorIndex(int x, int y)
    {
        return x + y * _height;
    }
}

