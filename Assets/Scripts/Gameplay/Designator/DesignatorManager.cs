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

        SpawnHelper.Spawn(BuildingDef, pos, map.Index);
        //var thingObject = GameObject.Instantiate(DataTableManager.Instance.ThingObject);
        //var frame = new Frame(BuildingDef, new ThingObject(thingObject), map, pos);
        //frame.GameObject.GO.transform.position = pos.ToVector3();
    }

    public bool CanPlace(IntVec2 pos, MapData map) {
        if (!map.ThingMap.InBound(pos)) {
            return false;
        }

        if (map.ThingMap.ThingsListAt(pos).Count > 0) {
            return false;
        }

        return true;
    }
}