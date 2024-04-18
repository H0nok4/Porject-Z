using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
}