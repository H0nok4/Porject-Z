using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;

public static class DropUtility {
    public static bool TryDropSpawn(Thing thing, PosNode pos, ThingPlaceMode placeMode, out Thing droppedResult,
        Action<Thing, int> onDropped, Predicate<PosNode> posValidator, bool playSound = true)
    {
        if (!pos.InBound())
        {
            Debug.LogError("物品想要掉落在地图之外");
            droppedResult = null;
            return false;
        }

        if (playSound)
        {
            Debug.LogWarning("未实现-掉落声效");
        }

        return PlaceUtility.TryPlaceThing(thing, pos, placeMode, out droppedResult, onPlaceAction: onDropped,
            validator: posValidator);
    }
}