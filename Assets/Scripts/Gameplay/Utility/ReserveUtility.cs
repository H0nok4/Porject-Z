using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;

public static class ReserveUtility {
    public static bool CanReserve(this Thing_Unit unit, JobTargetInfo target, bool ignoreOtherReservations = false)
    {
        if (!unit.Spawned)
        {
            return false;
        }

        return ReservationManager.Instance.CanReserve(unit, target, ignoreOtherReservations);
    }

    /// <summary>
    /// 额外需要判断可达性
    /// </summary>
    /// <returns></returns>
    public static bool CanReserveAndReach(this Thing_Unit unit, JobTargetInfo targetInfo, PathMoveEndType moveType,
        bool ignoreOtherReservations = false)
    {
        if (!unit.Spawned)
        {
            return false;
        }

        //TODO:添加可达性判断


        return ReservationManager.Instance.CanReserve(unit, targetInfo, ignoreOtherReservations);
    }

    public static bool Reserve(this Thing_Unit unit,JobTargetInfo getTarget, Job job) {
        if (!unit.Spawned)
        {
            return false;
        }

        return ReservationManager.Instance.Reserve(unit, job, getTarget);
    }
}