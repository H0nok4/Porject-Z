using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public int ActiveIndex = 0;
    //TODO:地图有多层，0位置为地面一层，1位置为地面二层，以此类推，每层地图的大小是一样的，上下层就是在同一个位置放出口
    private List<MapData> _mapDatas = new List<MapData>();

    public ListThings ListThings;

    public Map()
    {
        ListThings = new ListThings();
    }

    public MapData CurrentActiveMap
    {
        get
        {
            return _mapDatas[ActiveIndex];
        }

    }

    public IEnumerable<MapData> MapDatas()
    {
        foreach (var mapData in _mapDatas)
        {
            yield return mapData;
        }
    }
    public MapData AddMapData(int sizeX = 1000, int sizeY = 1000)
    {
        MapData data = new MapData(_mapDatas.Count,null,sizeX,sizeY);

        _mapDatas.Add(data);

        return data;
    }

    public MapData AddMapData(GameObject handlerObject,int sizeX,int sizeY)
    {
        MapData data = new MapData(_mapDatas.Count, handlerObject, sizeX, sizeY);

        _mapDatas.Add(data);

        return data;
    }

    public MapData GetMapDataByIndex(int layer)
    {
        if (layer < 0)
        {
            Debug.LogError($"想取的地图层级索引小于0，LayerIndex = {layer}");
            return null;
        }

        if (layer >= _mapDatas.Count)
        {
            Debug.LogError($"想取的地图层级索引大于当前地图最高层数，LayerIndex = {layer}，当前最高地图层数 = {_mapDatas.Count}");
            return null;
        }

        return _mapDatas[layer];
    }


    public PosNode GetPathNodeByUnit(Thing_Unit pawn)
    {

        for (int i = 0; i < _mapDatas.Count; i++)
        {
            //TODO:找到Pawn的位置，返回
            if (_mapDatas[i].HandleThings.Contains(pawn))
            {
                return new PosNode() { MapDataIndex = i,Pos = pawn.Position.Pos.Copy()};
            }
        }

        return null;

    }
}

