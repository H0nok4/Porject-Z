using Assets.Scripts.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ConfigType;
using UnityEngine;

public static class PlaceUtility
{

    private const int MinPalcePriorityFindRadius = 3;

    private const int MediumPlacePriorityFindRadius = 6;

    private const int MaxPlacePriorityFindRadius = 12;

    private static List<Thing> helperThingList = new List<Thing>();
    public static bool TryPlaceThing(Thing placeThing, PosNode pos, ThingPlaceMode placeMode, Action<Thing,int> onPlaceAction = null, Predicate<PosNode> validator = null, Rotation rotation = default(Rotation))
    {
        return TryPlaceThing(placeThing, pos, placeMode, out Thing resultThing,rotation, onPlaceAction, validator );
    }

    public static bool TryPlaceThing(Thing placeThing, PosNode pos, ThingPlaceMode placeMode,out Thing placedThing, Rotation rotation = default(Rotation), Action<Thing, int> onPlaceAction = null, Predicate<PosNode> validator = null)
    {
        var mapData = pos.MapData;

        switch (placeMode)
        {
            case ThingPlaceMode.Direct:
                return TryPlaceThingDirect(placeThing, pos, out placedThing,rotation, onPlaceAction);
            case ThingPlaceMode.Near:
                placedThing = null;
                int placeCount = -1;
                do
                {
                    placeCount = placeThing.Count;
                    if (!TryFindPlaceNear(pos,rotation,placeThing,allowStack : true,out PosNode bestPos,validator))
                    {
                        //找不到地方放了
                        return false;
                    }

                    if (TryPlaceThingDirect(placeThing,bestPos,out placedThing,rotation,onPlaceAction))
                    {
                        //在找到的位置放下物品
                        return true;
                    }
                } while (placeThing.Count != placeCount);

                Debug.LogError($"在{pos.Pos}位置以{placeMode}的模式放置物品失败");
                placedThing = null;
                return false;
                break;
            default:
                Debug.LogError($"意料之外的错误，在{pos.Pos}位置以{placeMode}的模式放置物品失败");
                placedThing = null;
                return false;
        }
    }

    public static bool TryFindPlaceNear(PosNode startPos, Rotation rotation, Thing placeThing, bool allowStack, out PosNode resultPos, Predicate<PosNode> validator)
    {
        //TODO:放置优先级 同类堆叠 > 在容器中堆叠 > 空位置
        PlaceSpotPriority priority = PlaceSpotPriority.Unusable;
        resultPos = startPos;
        //TODO:使用BFS找到周围的可以使用的位置
        var walkablePos = GetWalkablePosByBFS(placeThing,MinPalcePriorityFindRadius);
        for (int i = 0; i < walkablePos.Count; i++)
        {
            var pos = walkablePos[i];
            PlaceSpotPriority priorityInWalkablePos =
                GetPlaceSpotPriorityAt(pos, rotation, placeThing, startPos, allowStack, validator);
            if (priorityInWalkablePos > priority)
            {
                resultPos = walkablePos[i];
                priority = priorityInWalkablePos;
            }

            if (priority == PlaceSpotPriority.Prime)
            {
                break;
            }
        }

        if (priority >= PlaceSpotPriority.Medium)
        {
            return true;
        }

        //近的没找到，扩大范围
        var mediumWalkablePos = GetWalkablePosByBFS(placeThing, MediumPlacePriorityFindRadius);
        for (int i = 0; i < mediumWalkablePos.Count; i++) {
            var pos = mediumWalkablePos[i];
            PlaceSpotPriority priorityInWalkablePos =
                GetPlaceSpotPriorityAt(pos, rotation, placeThing, startPos, allowStack, validator);
            if (priorityInWalkablePos > priority) {
                resultPos = mediumWalkablePos[i];
                priority = priorityInWalkablePos;
            }

            if (priority == PlaceSpotPriority.Prime) {
                break;
            }
        }


        if (priority >= PlaceSpotPriority.Medium) {
            return true;
        }

        var maxWalkablePos = GetWalkablePosByBFS(placeThing, MaxPlacePriorityFindRadius);
        for (int i = 0; i < maxWalkablePos.Count; i++) {
            var pos = maxWalkablePos[i];
            PlaceSpotPriority priorityInWalkablePos =
                GetPlaceSpotPriorityAt(pos, rotation, placeThing, startPos, allowStack, validator);
            if (priorityInWalkablePos > priority) {
                resultPos = maxWalkablePos[i];
                priority = priorityInWalkablePos;
            }

            if (priority == PlaceSpotPriority.Prime) {
                break;
            }
        }


        if (priority >= PlaceSpotPriority.Medium) {
            return true;
        }


        return false;
    }

    private static PlaceSpotPriority GetPlaceSpotPriorityAt(PosNode pos, Rotation rotation, Thing placeThing, PosNode startPos, bool allowStack, Predicate<PosNode> validator)
    {
        //放置优先级 同类堆叠 > 在容器中堆叠 > 空位置
        if (!pos.InBound() || !pos.IsWalkable())
        {
            return PlaceSpotPriority.Unusable;
        }

        //TODO：Thing后面会占多个格子，需要判断是否全部在地图范围内

        if (validator != null && !validator(pos))
        {
            return PlaceSpotPriority.Unusable;
        }

        //TODO:开始判断优先级
        foreach (var thing in pos.MapData.ThingMap.ThingsAt(pos.Pos))
        {
            if (thing is Blueprint || thing is Thing_Building_Frame)
            {
                //TODO:最好不要放在蓝图和框架上
                return PlaceSpotPriority.Unusable;
            }
        }
        //TODO: 如果这里有同类物品,返回最高优先级

        //TODO:如果这里有容器,容器中可以堆叠,返回较高优先级
        Debug.LogWarning("没有可以放置的位置，返回Unusable");
        //什么都没,返回中等优先级
        return PlaceSpotPriority.Medium;
    }

    public static List<PosNode> GetWalkablePosByBFS(Thing thing, int maxLength) {
        var result = new List<PosNode>();
        var startPos = thing.MapData.GetSectionByPos(thing.Position.Pos);
        var queue = new Queue<PosNode>();
        queue.Enqueue(startPos.CreatePathNode());
        var closeList = new HashSet<PosNode>(new PathNodeComparer());
        closeList.Clear();
        while (queue.Count > 0) {
            var curNode = queue.Dequeue();

            var mapData = MapController.Instance.Map.GetMapDataByIndex(curNode.MapDataIndex);
            foreach (var dir in PathFinder.DirVecList) {
                if (mapData.GetSectionByPosition(curNode.Pos + dir) is { } section) {
                    if (!section.Walkable) {
                        //必须要可以走的
                        continue;
                    }
                    var newNode = section.CreatePathNode();
                    if (closeList.Contains(newNode)) {
                        continue;
                    }

                    newNode.Length = curNode.Length + 1;
                    if (newNode.Length > maxLength) {
                        continue;
                    }

                    newNode.Parent = curNode;
                    queue.Enqueue(newNode);
                    DebugDrawer.DrawBox(newNode.Pos);
                }
            }

            result.Add(curNode);
            closeList.Add(curNode);
        }

        return result;
    }

    public static bool TryPlaceThingDirect(Thing placeThing, PosNode pos, out Thing placedThing,
        Rotation rotation = default(Rotation), Action<Thing, int> onPlaceAction = null)
    {
        placedThing = null;
        helperThingList.Clear();
        helperThingList.AddRange(pos.MapData.ThingMap.ThingsListAt(pos.Pos));
        helperThingList.Sort((thingA, thingB) => thingB.Count.CompareTo(thingA.Count));//数量由大到小排
        //相同的物品,堆叠数量大于1的才能堆叠
        if (placeThing.Def.StackLimit > 1)
        {
            for (int i = 0; i < helperThingList.Count; i++)
            {
                var thing = helperThingList[i];
                if (placeThing.CanStackWith(thing))
                {
                    //尝试堆叠，多余的还要重新找地方放
                    int beforeStackCount = placeThing.Count;
                    if (thing.TryStackWith(placeThing))
                    {
                        //合并完了，结果就是当前的物品
                        placeThing = thing;
                        onPlaceAction?.Invoke(thing,beforeStackCount);
                    }

                    //数量变动了，说明有一部分成功合并进去了
                    if (onPlaceAction != null && beforeStackCount != thing.Count)
                    {
                        onPlaceAction.Invoke(thing,beforeStackCount - placeThing.Count);
                    }
                }
            }
        }

        int maxThingInPosCount;
        if (placeThing.Def.Category == ThingCategory.Item)
        {
            int itemCountInPos = helperThingList.Count((Thing thing) => thing.Def.Category == ThingCategory.Item);
            maxThingInPosCount = pos.GetMaxItemCountInPos() - itemCountInPos;
        }
        else
        {
            maxThingInPosCount = placeThing.Count + 1;
        }

        if (maxThingInPosCount <= 0 && placeThing.Def.StackLimit <= 1)
        {
            maxThingInPosCount = 1;
        }

        for (int i = 0; i < maxThingInPosCount; i++)
        {
            //尝试将物体分割放入格子中
            if (SplitAndSpawnOneStackOnPos(placeThing,pos,out placedThing,onPlaceAction,rotation))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 放一组物品到位置上
    /// </summary>
    /// <param name="placeThing"></param>
    /// <param name="pos"></param>
    /// <param name="resultThing"></param>
    /// <param name="onPlaceAction"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    private static bool SplitAndSpawnOneStackOnPos(Thing placeThing,PosNode pos,out Thing resultThing, Action<Thing, int> onPlaceAction, Rotation rotation = default(Rotation))
    {
        Thing splitThing = (placeThing.Count <= placeThing.Def.StackLimit)
            ? placeThing
            : placeThing.SplitOff(placeThing.Def.StackLimit);
        resultThing = SpawnHelper.Spawn(splitThing, pos,placeThing.Rotation);
        onPlaceAction?.Invoke(splitThing,splitThing.Count);
        //因为SplitOff如果数量不够的话，会直接返回该物体，所以这里判断是否相等，如果相等的话说明分完了
        return splitThing == placeThing;
    }


}
