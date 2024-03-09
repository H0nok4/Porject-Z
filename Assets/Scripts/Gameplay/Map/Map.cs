using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    //TODO:地图有多层，0位置为地面一层，1位置为地面二层，以此类推，每层地图的大小是一样的，上下层就是在同一个位置放出口
    private List<MapData> _mapDatas = new List<MapData>();

    public IEnumerable<MapData> MapDatas()
    {
        foreach (var mapData in _mapDatas)
        {
            yield return mapData;
        }
    }
    public MapData AddMapData()
    {
        MapData data = new MapData(_mapDatas.Count,null);

        _mapDatas.Add(data);

        return data;
    }

    public MapData AddMapData(GameObject handlerObject)
    {
        MapData data = new MapData(_mapDatas.Count, handlerObject);

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


    public PathNode GetPathNodeByPawn(Pawn pawn)
    {

        for (int i = 0; i < _mapDatas.Count; i++)
        {
            //TODO:找到Pawn的位置，返回
            if (_mapDatas[i].HandleThings.Contains(pawn))
            {
                return new PathNode() { MapDataIndex = i,Pos = pawn.Position.Copy()};
            }
        }

        return null;

    }
}

