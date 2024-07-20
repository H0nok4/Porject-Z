using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WorkGiver_SearchContainer : WorkGiver_Scanner {
    public override ThingRequest ThingRequest => ThingRequest.ForGroup(ThingRequestGroup.WaitForSearchContainer);

    public override Job JobOnThing(Thing_Unit unit, Thing thing, bool forced = false)
    {
        return base.JobOnThing(unit, thing, forced);
    }
}