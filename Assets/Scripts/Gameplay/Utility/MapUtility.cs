using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Gameplay;
using ConfigType;
using JetBrains.Annotations;
using UnityEngine;

public static class MapUtility {
    
    public static PosNode GetMapPosByInputMousePosition()
    {
        var mousePosToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var currentMap = MapController.Instance.Map.CurrentActiveMap;
        if (mousePosToWorld.x < 0 || mousePosToWorld.x >= currentMap.Width)
        {
            return null;
        }

        if (mousePosToWorld.y < 0 || mousePosToWorld.y >= currentMap.Height)
        {
            return null;
        }

        return new PosNode(new IntVec2(Mathf.FloorToInt(mousePosToWorld.x), Mathf.FloorToInt(mousePosToWorld.y)),currentMap.Index);
    }
    /// <summary>
    /// 找到可以移动的位置,不包括了起始点
    /// </summary>
    /// <param name="startPos"></param>
    /// <returns></returns>
    public static PosNode GetFirstStandablePosByPosNode(PosNode startPos)
    {
        var queue = new Queue<PosNode>();
        queue.Enqueue(startPos);
        var closeList = new HashSet<PosNode>(new PathNodeComparer());
        closeList.Clear();
        while (queue.Count > 0) {
            var curNode = queue.Dequeue();

            var mapData = MapController.Instance.Map.GetMapDataByIndex(curNode.MapDataIndex);
            foreach (var dir in PathFinder.DirVecList) {
                if (mapData.GetSectionByPosition(curNode.Pos + dir) is { } section) {
                    if (section.Walkable) {
                        //必须要可以走的
                        var thingsAtPos = mapData.ThingMap.ThingsAt(section.Position);
                        bool canWalk = true;
                        foreach (var thing in thingsAtPos) {
                            if (thing.Def.Passability is Traversability.Impassable) {
                                canWalk = false;
                                break;
                            }
                        }

                        if (canWalk) {
                            return section.CreatePathNode();
                        }
                    }
                    var newNode = section.CreatePathNode();
                    if (closeList.Contains(newNode)) {
                        continue;
                    }


                    newNode.Parent = curNode;
                    queue.Enqueue(newNode);
                    DebugDrawer.DrawBox(newNode.Pos);
                }
            }

            closeList.Add(curNode);
        }


        //找遍所有地方都没找到可以站立的点,有问题
        Logger.Instance?.LogError($"找遍所有地方都没找到可以站立的点,起始点:{startPos.Pos}");
        return null;
    }

    /// <summary>
    /// 找到可以移动的位置,包括了起始点
    /// </summary>
    /// <param name="startPos"></param>
    /// <returns></returns>
    public static PosNode GetFirstStandablePosByPosNodeContainsSourcePos(PosNode startPos) {
        var sourceMapData = MapController.Instance.Map.GetMapDataByIndex(startPos.MapDataIndex);
        if (sourceMapData.GetSectionByPosition(startPos.Pos) is { } sourceSectioni) {
            if (sourceSectioni.Walkable) {
                //必须要可以走的
                var thingsAtPos = sourceMapData.ThingMap.ThingsAt(sourceSectioni.Position);
                bool canWalk = true;
                foreach (var thing in thingsAtPos) {
                    if (thing.Def.Passability is Traversability.Impassable) {
                        canWalk = false;
                        break;
                    }
                }

                if (canWalk) {
                    return sourceSectioni.CreatePathNode();
                }
            }
        }

        var queue = new Queue<PosNode>();
        queue.Enqueue(startPos);
        var closeList = new HashSet<PosNode>(new PathNodeComparer());
        closeList.Clear();
        while (queue.Count > 0) {
            var curNode = queue.Dequeue();

            var mapData = MapController.Instance.Map.GetMapDataByIndex(curNode.MapDataIndex);
            foreach (var dir in PathFinder.DirVecList) {
                if (mapData.GetSectionByPosition(curNode.Pos + dir) is { } section) {
                    if (section.Walkable) {
                        //必须要可以走的
                        var thingsAtPos = mapData.ThingMap.ThingsAt(section.Position);
                        bool canWalk = true;
                        foreach (var thing in thingsAtPos) {
                            if (thing.Def.Passability is Traversability.Impassable) {
                                canWalk = false;
                                break;
                            }
                        }

                        if (canWalk) {
                            return section.CreatePathNode();
                        }
                    }
                    var newNode = section.CreatePathNode();
                    if (closeList.Contains(newNode)) {
                        continue;
                    }


                    newNode.Parent = curNode;
                    queue.Enqueue(newNode);
                    DebugDrawer.DrawBox(newNode.Pos);
                }
            }

            closeList.Add(curNode);
        }


        //找遍所有地方都没找到可以站立的点,有问题
        Logger.Instance?.LogError($"找遍所有地方都没找到可以站立的点,起始点:{startPos.Pos}");
        return null;
    }
}