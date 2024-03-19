using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

public static class SpawnHelper {
    public static Thing Spawn(Define_Thing define, IntVec2 position, int mapDataIndex)
    {
        return Spawn(ThingMaker.MakeNewThing(define), position, mapDataIndex);
    }

    public static Thing Spawn(Thing newThing, IntVec2 position, int mapDataIndex)
    {
        var mapData = MapController.Instance.Map.GetMapDataByIndex(mapDataIndex);
        if (mapData == null)
        {
            Debug.LogError($"生成物体到不存在的地图层级上,index = {mapDataIndex}");
            return null;
        }

        //TODO:如果是物品，需要判断该位置是否已经有其他物品了，或者同类物品堆叠，如果堆叠数量超过上限，需要使用BFS找到最近的一个空位置放置超过上限的东西
        newThing.GameObject = new ThingObject(Object.Instantiate(DataTableManager.Instance.ThingObject));
        newThing.SetPosition(position);
        if (newThing.HoldingOwner != null)
        {
            newThing.HoldingOwner.Remove(newThing);
        }
        newThing.SpawnSetup(mapData);

        return newThing;
    }
}