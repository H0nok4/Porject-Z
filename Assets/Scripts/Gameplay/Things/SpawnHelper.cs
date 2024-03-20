using UnityEngine;
using Object = UnityEngine.Object;

public static class SpawnHelper {
    public static Thing Spawn(Define_Thing define, IntVec2 position, int mapDataIndex)
    {
        return Spawn(ThingMaker.MakeNewThing(define), position, mapDataIndex);
    }

    public static Thing Spawn(Thing newThing, IntVec2 position, int mapDataIndex,WipeMode wipeMode = WipeMode.Vanish)
    {
        var mapData = MapController.Instance.Map.GetMapDataByIndex(mapDataIndex);
        if (mapData == null)
        {
            Debug.LogError($"生成物体到不存在的地图层级上,index = {mapDataIndex}");
            return null;
        }
        //TODO:如果该位置有Thing，需要移动到最近的点上
        switch (wipeMode)
        {
            case WipeMode.Vanish:
                //TODO:直接摧毁这个格子上的物体
                WipeExistingThings(mapData.GetSectionByPosition(position).CreatePathNode(false),newThing.Rotation,newThing.Def,DestroyType.Vanish);
                break;
            case WipeMode.Removal:
                //TODO:尝试将这个格子上的物体移动到其他地方
                TryRemovalThings();
                break;
            case WipeMode.VanishOrRemoval:

                break;
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

    private static void TryRemovalThings() {
        
    }

    public static void WipeExistingThings(PosNode node, Rotation rot, Define_Buildable thingDef, DestroyType destroyType)
    {
        //TODO:后面物体可能会站多个格子，需要每个格子都判断
        foreach (var thing in node.MapData.ThingMap.ThingsAt(node.Pos))
        {
            if (SpawningWipes(thingDef,thing.Def))
            {
                thing.Destroy(destroyType);
            }
        }
    }

    private static bool SpawningWipes(Define_Buildable thingDef, Define_Buildable def)
    {
        //TODO:根据配置类型来决定是否删除该位置的建筑，像是电线，壁灯之类的都可以跟其他建筑共存，

        return true;
    }
}