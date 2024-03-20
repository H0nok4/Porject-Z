using UnityEngine;

public enum DesignatorType {
    //TODO:测试用
    None,
    Building
}
public class DesignatorManager : Singleton<DesignatorManager> {
    //TODO:后面需要重构成通用的，目前多用于测试

    public DesignatorType DesignatorType;

    public Define_Thing BuildingDef = DataTableManager.Instance.ThingDefineHandler.WallFrame;

    public bool IsBuildingState {
        get {
            return DesignatorType == DesignatorType.Building && BuildingDef != null;
        }
    }

    public void PlaceFrameAt(IntVec2 pos, MapData map) {
        if (!CanPlace(pos, map)) {
            return;
        }

        SpawnHelper.Spawn(BuildingDef, new PosNode(){Pos = pos,MapDataIndex = map.Index});
    }

    public bool CanPlace(IntVec2 pos, MapData map) {
        if (!map.ThingMap.InBound(pos)) {
            return false;
        }

        //TODO:需要判断，如果有其他的FrameThing就不能放置
        var things = map.ThingMap.ThingsAt(pos);
        foreach (var thing in things)
        {
            if (!SpawnHelper.SpawningWipes(BuildingDef,thing.Def))
            {
                return false;
            }
        }

        return true;
    }
}