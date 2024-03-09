using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    //TODO:��ͼ�ж�㣬0λ��Ϊ����һ�㣬1λ��Ϊ������㣬�Դ����ƣ�ÿ���ͼ�Ĵ�С��һ���ģ����²������ͬһ��λ�÷ų���
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
            Debug.LogError($"��ȡ�ĵ�ͼ�㼶����С��0��LayerIndex = {layer}");
            return null;
        }

        if (layer >= _mapDatas.Count)
        {
            Debug.LogError($"��ȡ�ĵ�ͼ�㼶�������ڵ�ǰ��ͼ��߲�����LayerIndex = {layer}����ǰ��ߵ�ͼ���� = {_mapDatas.Count}");
            return null;
        }

        return _mapDatas[layer];
    }


    public PathNode GetPathNodeByPawn(Pawn pawn)
    {

        for (int i = 0; i < _mapDatas.Count; i++)
        {
            //TODO:�ҵ�Pawn��λ�ã�����
            if (_mapDatas[i].HandleThings.Contains(pawn))
            {
                return new PathNode() { MapDataIndex = i,Pos = pawn.Position.Copy()};
            }
        }

        return null;

    }
}

