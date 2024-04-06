using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Gameplay;
using ConfigType;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : Singleton<PlayerController>
{
    public Thing_Unit_Pawn ThingUnitPawnUnit;
    public void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnPlayerMouseButton0Down();    
        }
        else if (Input.GetMouseButtonDown(1))
        {
            OnPlayerMouseButton1Down();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetGameTickSpeed(TimeSpeed.Normal);
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetGameTickSpeed(TimeSpeed.Fast);
        }else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetGameTickSpeed(TimeSpeed.Superfast);
        }else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameTicker.Instance.Paused)
            {
                GameTicker.Instance.SetTimeSpeed(GameTicker.Instance._prePauseTimeSpeed);
            }
            else
            {
                GameTicker.Instance.Pause();
            }
        }
    }

    public void OnPlayerMouseButton0Down()
    {
        if (DesignatorManager.Instance.IsBuildingState)
        {
            //TODO:放置一个蓝图
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = MapController.Instance.Map.GetMapDataByIndex(0).TileMapObject.WorldToCell(mousePosition);
            var int2Pos = new IntVec2(cellPosition.x, cellPosition.y);
            DesignatorManager.Instance.PlaceBlueprintAt(int2Pos,MapController.Instance.Map.GetMapDataByIndex(0));
        }else if (DesignatorManager.Instance.IsPlacingState)
        {
            //TODO:先放置100个木头
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = MapController.Instance.Map.GetMapDataByIndex(0).TileMapObject.WorldToCell(mousePosition);
            var int2Pos = new IntVec2(cellPosition.x, cellPosition.y);
            DesignatorManager.Instance.PlaceThing(int2Pos, MapController.Instance.Map.GetMapDataByIndex(0));
        }
    }

    public void OnPlayerMouseButton1Down()
    {
        //TODO:如果当前处于建筑模式，退出建筑模式
        DesignatorManager.Instance.DesignatorType = DesignatorType.None;
        //else
        //{
        //    //按下右键测试寻路
        //    TryFindPath();
        //}
    }

    public void SetGameTickSpeed(TimeSpeed speed)
    {
        GameTicker.Instance.SetTimeSpeed(speed);
    }

    //TODO:临时，开始寻路
    //public void TryFindPath()
    //{
    //    //TODO:后面需要改成分配给单位一个强制移动的Job
    //    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    Vector3Int cellPosition = MapController.Instance.Map.GetMapDataByIndex(0).TileMapObject.WorldToCell(mousePosition);
    //    Debug.Log($"当前右键的位置为:X:{cellPosition.x} Y:{cellPosition.y}");
    //    var findPath = PathFinder.AStarFindPath(pawn,
    //        MapController.Instance.Map.GetMapDataByIndex(0)
    //            .GetSectionByPosition(new IntVec2(cellPosition.x, cellPosition.y)).CreatePathNode(),
    //        MapController.Instance.Map);
    //    PawnPath path = new PawnPath() { FindingPath = findPath ,Using = true};
    //    pawn.PathMover.SetPath(path);
    //}


    //private float _speed = 5;

    //public IEnumerator MovePawn(Pawn pawn,List<PosNode> nodes)
    //{
    //    Debug.Log("开始寻路");
    //    int index = 0;
    //    while (index < nodes.Count)
    //    {
    //        //TODO:
    //        PosNode targetWaypoint = nodes[index];
    //        pawn.SetPosition(targetWaypoint.Pos.Copy()); = ;
    //        //TODO:先直接瞬移过去然后等待一会

    //        yield return new WaitForSeconds(0.3f);

    //        index++;
    //    }

    //    Debug.Log("结束寻路");

    //}
}