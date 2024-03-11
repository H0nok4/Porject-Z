using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : Singleton<PlayerController>
{
    public Pawn pawn;
    public void HandleInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //TODO:按下右键
            TryFindPath();
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

    public void SetGameTickSpeed(TimeSpeed speed)
    {
        GameTicker.Instance.SetTimeSpeed(speed);
    }

    //TODO:临时，开始寻路
    public void TryFindPath()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPosition = MapController.Instance.Map.GetMapDataByIndex(0).TileMapObject.WorldToCell(mousePosition);
        Debug.Log($"当前右键的位置为:X:{cellPosition.x} Y:{cellPosition.y}");
        var findPath = PathFinder.AStarFindPath(pawn,
            MapController.Instance.Map.GetMapDataByIndex(0)
                .GetSectionByPosition(new IntVec2(cellPosition.x, cellPosition.y)).CreatePathNode(),
            MapController.Instance.Map);
        PawnPath path = new PawnPath() { FindingPath = findPath ,Using = true};
        pawn.PathMover.SetPath(path);
    }


    private float _speed = 5;

    public IEnumerator MovePawn(Pawn pawn,List<PathNode> nodes)
    {
        Debug.Log("开始寻路");
        int index = 0;
        while (index < nodes.Count)
        {
            //TODO:
            PathNode targetWaypoint = nodes[index];
            pawn.Position = targetWaypoint.Pos.Copy();
            //TODO:先直接瞬移过去然后等待一会

            yield return new WaitForSeconds(0.3f);

            index++;
        }

        Debug.Log("结束寻路");

    }
}