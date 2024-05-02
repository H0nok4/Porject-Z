using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Gameplay;
using JetBrains.Annotations;
using UnityEngine;

public static class MapUtility {
    
    public static IntVec2 GetMapPosByInputMousePosition()
    {
        var mousePosToWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var currentMap = MapController.Instance.Map.CurrentActiveMap;
        if (mousePosToWorld.x < 0 || mousePosToWorld.x >= currentMap.Width)
        {
            return IntVec2.Invalid;
        }

        if (mousePosToWorld.y < 0 || mousePosToWorld.y >= currentMap.Height)
        {
            return IntVec2.Invalid;
        }

        return new IntVec2(Mathf.FloorToInt(mousePosToWorld.x), Mathf.FloorToInt(mousePosToWorld.y));
    }

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
                        return section.CreatePathNode();
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
        Debug.LogError($"找遍所有地方都没找到可以站立的点,起始点:{startPos.Pos}");
        return null;
    }
}