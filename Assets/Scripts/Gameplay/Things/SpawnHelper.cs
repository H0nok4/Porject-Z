using System.Collections.Generic;
using ConfigType;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Object = UnityEngine.Object;

public static class SpawnHelper
{

    private static List<Thing> _helper = new List<Thing>();
    public static Thing Spawn(ThingDefine thingDefine, PosNode node)
    {
        return Spawn(ThingMaker.MakeNewThing(thingDefine),node,Rotation.North);
    }

    public static Thing Spawn(ThingDefine thingDefine, PosNode node, int count)
    {
        return Spawn(ThingMaker.MakeNewThing(thingDefine, count), node, Rotation.North);
    }

    public static Thing Spawn(Thing newThing, PosNode node,Rotation rot, WipeMode wipeMode = WipeMode.Vanish)
    {
        var mapData = node.MapData;
        if (mapData == null)
        {
            Debug.LogError($"生成物体到不存在的地图层级上,index = {node.MapDataIndex}");
            return null;
        }
        //TODO:如果该位置有Thing，需要移动到最近的点上
        switch (wipeMode)
        {
            case WipeMode.Vanish:
                //TODO:直接摧毁这个格子上的物体
                WipeExistingThings(mapData.GetSectionByPosition(node.Pos).CreatePathNode(false),newThing.Rotation,newThing.Def,DestroyType.Vanish);
                break;
            case WipeMode.Removal:
                //TODO:尝试将这个格子上的物体移动到其他地方
                TryRemovalThings(mapData.GetSectionByPosition(node.Pos).CreatePathNode(false), newThing.Rotation, newThing.Def, DestroyType.Vanish);
                break;
            case WipeMode.VanishOrRemoval:

                break;
        }
        //TODO:如果是物品，需要判断该位置是否已经有其他物品了，或者同类物品堆叠，如果堆叠数量超过上限，需要使用BFS找到最近的一个空位置放置超过上限的东西
        newThing.GameObject = new ThingObject(Object.Instantiate(DataManager.Instance.ThingObject));
        newThing.SetPosition(node.Pos,node.MapDataIndex);
        if (newThing.HoldingOwner != null)
        {
            newThing.HoldingOwner.Remove(newThing);
        }
        newThing.SpawnSetup(mapData);

        return newThing;
    }

    private static void TryRemovalThings(PosNode node, Rotation rot, ThingDefine mainDef, DestroyType destroyType) {
        //TODO:后面物体可能会站多个格子，需要每个格子都判断
        foreach (var thing in node.MapData.ThingMap.ThingsAt(node.Pos)) {
            if (!SpawningWipes(mainDef, thing.Def)) {
                continue;
            }

            if (thing.Def.Category == ThingCategory.Item)
            {
                //移动物品
                thing.DeSpawn();
                if (!PlaceUtility.TryPlaceThing(thing,node,ThingPlaceMode.Near,null,null))
                {
                    thing.Destroy();
                }
            }
            else
            {
                thing.Destroy();
            }
        }
    }

    public static void WipeExistingThings(PosNode node, Rotation rot, ThingDefine thingDef, DestroyType destroyType)
    {
        //TODO:后面物体可能会站多个格子，需要每个格子都判断
        _helper.Clear();
        _helper.AddRange(node.MapData.ThingMap.ThingsListAt(node.Pos));
        foreach (var thing in _helper)
        {
            if (SpawningWipes(thingDef,thing.Def))
            {
                thing.Destroy(destroyType);
            }
        }
        _helper.Clear();
    }

    public static bool SpawningWipes(ThingDefine thingDef, ThingDefine def)
    {
        //TODO:根据配置类型来决定是否删除该位置的建筑，像是电线，壁灯之类的都可以跟其他建筑共存，
        var wantPlaceThingDef = thingDef;
        var existThingDef = def;
        //if (wantPlaceThingDef.Category == ThingCategory.Building && wantPlaceThingDef.IsFrame)
        //{

        //}

        return false;
    }
}