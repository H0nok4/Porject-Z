using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WorkGiver_SearchContainerAndHaul : WorkGiver_Scanner{
    public override ThingRequest ThingRequest => ThingRequest.ForGroup(ThingRequestGroup.WaitForSearchAndHaulContainer);

    public override Job JobOnThing(Thing_Unit unit, Thing thing, bool forced = false)
    {
        if (!(thing is Thing_GenResourceContainer container)) {
            return null;
        }

        //TODO:看看目标是否被人预定
        if (!ReservationManager.Instance.CanReserve(unit, thing)) {
            return null;
        }

        //TODO:没有初始化过需要进行一个搜索流程,然后可能还有不完全初始化的,需要继续这个流程,完全初始化的是另一个收集物品的WorkGiver
        if (container.Inited)
        {
            
        }

        return null;
    }
}