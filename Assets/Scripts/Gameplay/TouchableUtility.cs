
using System;

public static class TouchableUtility {
    public static bool CanTouch(PosNode posNode, PosNode targetNode)
    {
        if (posNode.MapDataIndex != targetNode.MapDataIndex)
        {
            //都不在一个地图上，无法触碰
            return false;
        }

        if (Math.Abs(posNode.Pos.X - targetNode.Pos.X) > 1 || Math.Abs(posNode.Pos.Y - targetNode.Pos.Y) > 1)
        {
            //距离超过一格，肯定触碰不了
            return false;
        }

        //TODO:后面可以添加是否有障碍物阻挡了

        return true;
    }

    public static bool CanTouch(PosNode posNode, int targetMapIndex, IntVec2 pos)
    {
        if (posNode.MapDataIndex != targetMapIndex) {
            //都不在一个地图上，无法触碰
            return false;
        }

        if (Math.Abs(posNode.Pos.X - pos.X) > 1 || Math.Abs(posNode.Pos.Y - pos.Y) > 1) {
            //距离超过一格，肯定触碰不了
            return false;
        }

        //TODO:后面可以添加是否有障碍物阻挡了

        return true;
    }
}