using System;
using System.Collections;
using System.Collections.Generic;
using ConfigType;
using UnityEngine;

public static class FogOfWarUtility
{
    public static HashSet<IntVec2> GetUnitRangeVisiblePos(IntVec2 unitPos,int mapIndex, int viewRange)
    {
        HashSet<IntVec2> result = SimplePool<HashSet<IntVec2>>.Get();
        result.Clear();
        var circleList = CircleUtility.GetBresenhamCircle(unitPos, viewRange);
        //TODO:对每一个最外围的点做一次直线检测,路上遇到不可通过的格子的时候就停止
        var mapData = MapController.Instance.Map.GetMapDataByIndex(mapIndex);
        foreach (var intVec2 in circleList)
        {
            var pos = unitPos;
            var endPos = intVec2;

            int dx = Math.Abs(endPos.X - pos.X);
            int dy = Math.Abs(endPos.Y - pos.Y);
            int sx = pos.X < endPos.X ? 1 : -1;
            int sy = pos.Y < endPos.Y ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                var section = mapData.GetSectionByPosition(pos);
                if (section == null || section.SectionType == SectionType.Wall)
                {
                    break;
                }
                
                result.Add(new IntVec2(pos.X, pos.Y));

                if (pos.X == endPos.X && pos.Y == endPos.Y)
                    break;

                int e2 = 2 * err;
                if (e2 > -dy) {
                    err -= dy;
                    pos.X += sx;
                }
                if (e2 < dx) {
                    err += dx;
                    pos.Y += sy;
                }
            }
        }


        return result;
    }
}
