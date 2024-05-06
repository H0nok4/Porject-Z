using ConfigType;
using UnityEngine;

public enum DesignatorType {
    //TODO:测试用
    None,
    Building,
    Placing
}
public class DesignatorManager : Singleton<DesignatorManager> {
    //TODO:后面需要重构成通用的，目前多用于测试

    public DesignatorType DesignatorType;

    public ThingDefine BuildingDef = DataManager.Instance.GetThingDefineByID(2);

    public ThingDefine PlacingDef = DataManager.Instance.GetThingDefineByID(202);

    public bool IsBuildingState {
        get {
            return DesignatorType == DesignatorType.Building && BuildingDef != null;
        }
    }

    public bool IsPlacingState
    {
        get
        {
            return DesignatorType == DesignatorType.Placing && PlacingDef != null;
        }
    }

    public void PlaceBlueprintAt(IntVec2 pos, MapData map)
    {
        if (!CanPlace(pos,BuildingDef, map)) {
            return;
        }

        ThingUtility.CreateBlueprintDefToThingDef(BuildingDef);
        SpawnHelper.Spawn(BuildingDef.BlueprintDef, new PosNode(pos, map.Index) { });
    }

    public void PlaceThing(IntVec2 pos, MapData map)
    {
        if (!CanPlace(pos,PlacingDef,map))
        {
            return;
        }

        SpawnHelper.Spawn(PlacingDef, new PosNode(pos, map.Index) { },9);
    }

    public bool CanPlace(IntVec2 pos,ThingDefine wantPlaceDefine,MapData map) {
        if (!map.ThingMap.InBound(pos)) {
            return false;
        }

        //TODO:需要判断，如果有其他的FrameThing就不能放置
        var things = map.ThingMap.ThingsAt(pos);
        foreach (var thing in things)
        {
            if (SpawnHelper.SpawningWipes(wantPlaceDefine, thing.Def))
            {
                return false;
            }
        }

        return true;
    }
}