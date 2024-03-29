using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;

public static class HaulUtility {

    public static bool CanHaulAside(Thing_Unit p, Thing t, out PosNode storeCell) {
        storeCell = null;
        if (!t.Def.EverHaulable) {
            return false;
        }
        if (!p.CanReserveAndReach(t, PathMoveEndType.Touch)) {
            return false;
        }

        if (!TryFindSpotToPlaceHaulableCloseTo(t, p, t.PositionHeld, out storeCell)) {
            return false;
        }

        return true;
    }

    private static bool TryFindSpotToPlaceHaulableCloseTo(Thing thing, Thing_Unit thingUnit, PosNode from, out PosNode resultPos)
    {
        //TODO:DFS寻找到最近的空位
        return PlaceUtility.TryFindPlaceNear(from, thing.Rotation, thing, true, out resultPos, null);
    }

    public static Job HaulAsideJobFor(Thing_Unit unit,Thing haulThing)
    {
        if (!CanHaulAside(unit,haulThing,out PosNode haulPos))
        {
            return null;
        }

        Job haulJob = JobMaker.MakeJob(DataTableManager.Instance.JobDefineHandler.HaulToCell, haulThing, haulPos);
        haulJob.Count = 10000;
        haulJob.HaulMode = HaulMode.ToCell;
        return haulJob;
    }
}
