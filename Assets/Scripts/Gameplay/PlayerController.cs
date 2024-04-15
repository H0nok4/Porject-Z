using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Gameplay;
using ConfigType;
using FairyGUI;
using UI;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DragBox
{
    public Vector3 StartDragUIPosition;

    public Vector3 CurDragPosition => UIUtility.GetUIPositionByInputPos(Input.mousePosition);

    public bool IsDrag;

    public bool IsValid =>
        Vector3.Distance(StartDragUIPosition, UIUtility.GetUIPositionByInputPos(Input.mousePosition)) > 0.5f;

    /// <summary>
    /// 在UI上的左上角坐标
    /// </summary>
    public Vector3 LeftUpPos {
        get {
            var dragBox = PlayerController.Instance.DragBox;
 
            var startPos = dragBox.StartDragUIPosition;
            var curPos = dragBox.CurDragPosition;
            if (startPos.x > curPos.x && startPos.y > curPos.y) {
                //当前在左上角,点击位置在右下角,UI位置为当前鼠标位置,长度为起始位置X -  当前位置X 高度为起始位置Y - 当前位置Y;
                return new Vector3(curPos.x, curPos.y);
            }
            else if (startPos.x < curPos.x && startPos.y > curPos.y) {
                return new Vector3(startPos.x, curPos.y);
            }
            else if (startPos.x < curPos.x && startPos.y < curPos.y) {
                return new Vector3(startPos.x, startPos.y, 0);
            }
            else {
                return new Vector3(curPos.x, startPos.y);
            }
        }
    }

    /// <summary>
    /// 在UI上的右下角坐标
    /// </summary>
    public Vector3 RightDownPos {
        get {
            var dragBox = PlayerController.Instance.DragBox;

            var startPos = dragBox.StartDragUIPosition;
            var curPos = dragBox.CurDragPosition;
            if (startPos.x > curPos.x && startPos.y > curPos.y) {
                //当前在左上角,点击位置在右下角,UI位置为当前鼠标位置,长度为起始位置X -  当前位置X 高度为起始位置Y - 当前位置Y;
                return new Vector3(startPos.x, startPos.y);
            }
            else if (startPos.x < curPos.x && startPos.y > curPos.y) {
                return new Vector3(curPos.x, startPos.y);
            }
            else if (startPos.x < curPos.x && startPos.y < curPos.y) {
                return new Vector3(curPos.x,curPos.y, 0);
            }
            else {
                return new Vector3(startPos.x, curPos.y);
            }
        }
    }

    public float Width {
        get {
            var dragBox = PlayerController.Instance.DragBox;
            var startPos = dragBox.StartDragUIPosition;
            var curPos = dragBox.CurDragPosition;
            if (startPos.x > curPos.x && startPos.y > curPos.y) {
                //当前在左上角,点击位置在右下角,UI位置为当前鼠标位置,长度为起始位置X -  当前位置X 高度为起始位置Y - 当前位置Y;
                return startPos.x - curPos.x;
            }
            else if (startPos.x < curPos.x && startPos.y > curPos.y) {
                return curPos.x - startPos.x;
            }
            else if (startPos.x < curPos.x && startPos.y < curPos.y) {
                return curPos.x - startPos.x;
            }
            else {
                return startPos.x - curPos.x;
            }
        }
    }

    public float Height {
        get {
            var dragBox = PlayerController.Instance.DragBox;
            var startPos = dragBox.StartDragUIPosition;
            var curPos = dragBox.CurDragPosition;
            if (startPos.x > curPos.x && startPos.y > curPos.y) {
                //当前在左上角,点击位置在右下角,UI位置为当前鼠标位置,长度为起始位置X -  当前位置X 高度为起始位置Y - 当前位置Y;
                return startPos.y - curPos.y;
            }
            else if (startPos.x < curPos.x && startPos.y > curPos.y) {
                return startPos.y - curPos.y;
            }
            else if (startPos.x < curPos.x && startPos.y < curPos.y) {
                return curPos.y - startPos.y;
            }
            else {
                return curPos.y - startPos.y;
            }
        }
    }
    
}

public static class UIUtility
{
    public static Vector3 GetUIPositionByInputPos(Vector3 mousePosition)
    {
        return new Vector3(mousePosition.x,Screen.height - mousePosition.y);
    }

    public static Vector3 GetWorldPositionByUIPosition(Vector3 uiPosition) {
        var toUnityPositioin = new Vector3(uiPosition.x, Screen.height - uiPosition.y, 0);
        return Camera.main.ScreenToWorldPoint(toUnityPositioin);
    }
}

public class PlayerController : Singleton<PlayerController>
{
    public MainPanel MainPanel => UIManager.Instance.Find<MainPanel>();

    public DragBox DragBox = new DragBox();

    public Thing_Unit_Pawn ThingUnitPawnUnit;
    public void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DragBox.IsDrag = true;
            DragBox.StartDragUIPosition = UIUtility.GetUIPositionByInputPos(Input.mousePosition);

            var inputGetMouseDownPosition = Input.mousePosition;
            var hitTest = Stage.inst.HitTest(new Vector2(inputGetMouseDownPosition.x,
                Screen.height - inputGetMouseDownPosition.y),true); 
            if (hitTest != null && hitTest is not Stage)
            {
                GameInputContext.Instance.HitUI = true;
            }
            else
            {
                GameInputContext.Instance.HitUI = false;
            }

            OnPlayerMouseButton0Down();    
        }
        else if (Input.GetMouseButtonDown(1))
        {
            OnPlayerMouseButton1Down();
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (DragBox.IsDrag)
            {
                //TODO:选择DragBox中所有的物体(按优先级)
                DragBox.IsDrag = false;
                SelectDragBox();
            }
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

    private void SelectDragBox()
    {
        //TODO:按优先级选中单位 Unit > Item > Blueprint > Frame > Building
        //TODO:有一个对应类型的就选中当前所有同类型的,其他的忽略

        Debug.Log("选中界面中的东西");
        //TODO:先获得地图中范围内的所有物体
        var worldStartPos = UIUtility.GetWorldPositionByUIPosition(DragBox.LeftUpPos);
        var worldEndPos = UIUtility.GetWorldPositionByUIPosition(DragBox.RightDownPos);
        Debug.Log($"当前拖拽世界位置:起始：{worldStartPos},结束:{worldEndPos}");
        var startY = (int) worldEndPos.y;
        var endY = (int) worldStartPos.y;
        var startX = (int) worldStartPos.x;
        var endX = (int) worldStartPos.y;
        var things = new List<Thing>();
        for (int i = startY; i < endY; i++) {
            for (int j = startX; j < endX; j++) {
                if (MapController.Instance.Map.GetMapDataByIndex(0).ThingMap.ThingsAt(new IntVec2(j,i)) is {} enumrableThings) {
                    things.AddRange(enumrableThings);
                }
            }
        }
        Debug.Log($"拖拽范围内一共有{things.Count}个物体");

        //TODO:后面按优先级选择
        SelectManager.Instance.SetSelectThings(things);
    }

    public void Update()
    {
        GameInputContext.Instance.Clear();
        HandleInput();
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
        else {
            //TODO：点击的话，看看该位置是否有可以点击的物体，然后选中其中一个刷新到UI上
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = MapController.Instance.Map.GetMapDataByIndex(0).TileMapObject.WorldToCell(mousePosition);
            var things = MapController.Instance.Map.GetMapDataByIndex(0).ThingMap
                .ThingsListAt(new IntVec2(cellPosition.x, cellPosition.y));



            if (!GameInputContext.Instance.HitUI)
            {
                var ui = UIManager.Instance.Find<MainPanel>();
                if (ui != null && things.Count > 0) {
                    GameInputContext.Instance.AddEvent(new UIEvent(UIEventID.OnClickMapThing, things[0]));
                    ui.SetCurTrackedThing(things[0]);
                }
                else {
                    GameInputContext.Instance.AddEvent(new UIEvent(UIEventID.OnClickMapThing, null));
                    ui.SetCurTrackedThing(null);
                }
            }

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