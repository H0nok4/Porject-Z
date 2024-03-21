using Assets.Scripts.Gameplay;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
                        //�Ҳ����ط�����
                        return false;
                    }

                    if (TryPlaceThingDirect(placeThing,bestPos,out placedThing,rotation,onPlaceAction))
                    {
                        //���ҵ���λ�÷�����Ʒ
                        return true;
                    }
                } while (placeThing.Count != placeCount);

                Debug.LogError($"��{pos.Pos}λ����{placeMode}��ģʽ������Ʒʧ��");
                placedThing = null;
                return false;
                break;
            default:
                Debug.LogError($"����֮��Ĵ�����{pos.Pos}λ����{placeMode}��ģʽ������Ʒʧ��");
                placedThing = null;
                return false;
        }
    }

    private static bool TryFindPlaceNear(PosNode startPos, Rotation rotation, Thing placeThing, bool allowStack, out PosNode resultPos, Predicate<PosNode> validator)
    {
        //TODO:�������ȼ� ͬ��ѵ� > �������жѵ� > ��λ��
        PlaceSpotPriority priority = PlaceSpotPriority.Unusable;
        resultPos = startPos;
        //TODO:ʹ��BFS�ҵ���Χ�Ŀ���ʹ�õ�λ��
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

        //����û�ҵ�������Χ
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
        //�������ȼ� ͬ��ѵ� > �������жѵ� > ��λ��
        if (!pos.InBound() || !pos.IsWalkable())
        {
            return PlaceSpotPriority.Unusable;
        }

        //TODO��Thing�����ռ������ӣ���Ҫ�ж��Ƿ�ȫ���ڵ�ͼ��Χ��

        if (validator != null && !validator(pos))
        {
            return PlaceSpotPriority.Unusable;
        }

        //TODO:��ʼ�ж����ȼ�

        return PlaceSpotPriority.Unusable;
    }

    public static List<PosNode> GetWalkablePosByBFS(Thing thing, int maxLength) {
        var result = new List<PosNode>();
        var startPos = thing.MapData.GetSectionByPos(thing.Position);
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
                        //����Ҫ�����ߵ�
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
        helperThingList.Sort((thingA, thingB) => thingB.Count.CompareTo(thingA.Count));//�����ɴ�С��
        //��ͬ����Ʒ,�ѵ���������1�Ĳ��ܶѵ�
        if (placeThing.Def.StackLimit > 1)
        {
            for (int i = 0; i < helperThingList.Count; i++)
            {
                var thing = helperThingList[i];
                if (placeThing.CanStackWith(thing))
                {
                    //���Զѵ�������Ļ�Ҫ�����ҵط���
                    int beforeStackCount = placeThing.Count;
                    if (thing.TryStackWith(placeThing))
                    {
                        //�ϲ����ˣ�������ǵ�ǰ����Ʒ
                        placeThing = thing;
                        onPlaceAction?.Invoke(thing,beforeStackCount);
                    }

                    //�����䶯�ˣ�˵����һ���ֳɹ��ϲ���ȥ��
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
            //���Խ�����ָ���������
            if (SplitAndSpawnOneStackOnPos(placeThing,pos,out placedThing,onPlaceAction,rotation))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// ��һ����Ʒ��λ����
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
        resultThing = SpawnHelper.Spawn(splitThing, pos);
        onPlaceAction?.Invoke(splitThing,splitThing.Count);
        //��ΪSplitOff������������Ļ�����ֱ�ӷ��ظ����壬���������ж��Ƿ���ȣ������ȵĻ�˵��������
        return splitThing == placeThing;
    }


}
