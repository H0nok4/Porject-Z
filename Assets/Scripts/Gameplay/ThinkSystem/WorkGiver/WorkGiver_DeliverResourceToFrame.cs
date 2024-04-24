using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WorkGiver_DeliverResourceToFrame : WorkGiver_DeliverResourceTo {
    public override ThingRequest ThingRequest => ThingRequest.ForGroup(ThingRequestGroup.BuildingFrame);

    public override Job JobOnThing(Thing_Unit unit, Thing thing, bool forced = false) {
        if (!(thing is Thing_Building_Frame frame)) {
            return null;
        }

        //TODO:看看目标是否被人预定
        if (!ReservationManager.Instance.CanReserve(unit,thing))
        {
            return null;
        }

        if (BuildUtility.FirstBlockingThing(frame, (Thing_Unit_Pawn)unit) != null) {
            //TODO:有东西正在阻挡建造蓝图，看情况需要搬离之类的
            return BuildUtility.HandleBlockingThingJob(frame, unit, forced);
        }

        if (!BuildUtility.CanBuild(frame, unit)) {
            return null;
        }

        var haulJob = DeliverResourceJobFor(unit, frame);
        if (haulJob != null) {
            return haulJob;
        }
        //没有材料或者不需要材料了


        return null;
    }


}