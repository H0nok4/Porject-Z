using System;
using UnityEngine;

public class PathMover {
    public Thing_Unit RegisterPawn;

    public Map AllMap => MapController.Instance.Map;

    public PawnPath CurrentMovingPath;

    private Action _onComplete;

    public PathMover(Thing_Unit pawn) {
        RegisterPawn = pawn;
    }

    public void SetPath(PawnPath path, Action onComplete = null) {
        //TODO:如果当前有正在移动中的路径,需要先移动到当前路径的最新一格再接着移动
        //if (CurrentMovingPath is {Using:true} ) {
        //    path.FindingPath.Insert(0, CurrentMovingPath.GetCurrentPosition());
        //}
        CurrentMovingPath = path;
        _onComplete = onComplete;
    }

    public void Tick() {
        if (CurrentMovingPath is not { Using: true }) {
            //没有正在移动的路径,返回
            return;
        }

        //TODO:根据当前的路径点移动物体
        if (CurrentMovingPath.GetCurrentPosition() is { } currentNode) {
            if (currentNode.FastDistance(RegisterPawn.GameObject.GO.transform.position) > Mathf.Epsilon) {
                //TODO:还没有重合,将Pawn朝目标点移动
                RegisterPawn.GameObject.GO.transform.position = Vector3.MoveTowards(RegisterPawn.GameObject.GO.transform.position,
                    currentNode.Pos.ToVector3(), RegisterPawn.MoveSpeed * Time.deltaTime);
            }
            else {

                //TODO:后面可能有上楼梯或者使用传送门等到达其他位置的功能，需要在PathNode中标记并且在这里操作
                TryEnterTile(currentNode);
                CurrentMovingPath.CurMovingIndex++;
                if (CurrentMovingPath.End) {
                    CurrentMovingPath.Complete();
                    _onComplete?.Invoke();
                    RegisterPawn.JobTracker.OnPathMoveEnd();
                }
            }
        }
    }

    public void TryEnterTile(PathNode node) {
        //TODO:每次进入一个新的格子，需要发出事件

        //TODO:可能从其他地图进来的

        var preSection = RegisterPawn.MapData.GetSectionByPosition(RegisterPawn.Position);
        preSection.UnRegisterThing(RegisterPawn);

        var mapData = AllMap.GetMapDataByIndex(node.MapDataIndex);
        RegisterPawn.Position = node.Pos.Copy();
        var currentSection = mapData.GetSectionByPosition(node.Pos);
        currentSection.RegisterThing(RegisterPawn);
    }
}