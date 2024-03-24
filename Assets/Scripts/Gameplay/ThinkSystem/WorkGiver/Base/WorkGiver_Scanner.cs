using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WorkGiver_Scanner : WorkGiver
{
    public virtual ThingRequest ThingRequest => ThingRequest.ForGroup(ThingRequestGroup.Undefined);

    public virtual PathMoveEndType PathMoveEndType => PathMoveEndType.Touch;

    public virtual IEnumerable<Thing> CanWorkThings(Thing_Unit unit)
    {
        return null;
    }

    public virtual bool HasJobOnThing(Thing_Unit unit,Thing thing,bool forced = false)
    {
        return JobOnThing(unit, thing, forced) != null;
    }

    public virtual Job JobOnThing(Thing_Unit unit, Thing thing, bool forced = false)
    {
        return null;
    }

    public virtual bool HasJobOnPosition(Thing_Unit unit, PosNode node, bool forced = false)
    {
        return JobOnPosition(unit,node,forced) != null;
    }

    public virtual Job JobOnPosition(Thing_Unit unit, PosNode node, bool forced = false)
    {
        return null;
    }


}
