using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;

public class WorkGiver_BuildFrame : WorkGiver_Scanner
{
    public override ThingRequest ThingRequest => ThingRequest.ForGroup(ThingRequestGroup.BuildingFrame);

    public override Job JobOnThing(Thing_Unit unit, Thing thing, bool forced = false)
    {
        if (thing is not Thing_Building_Frame frame)
        {
            return null;
        }

        //TODO:后面Frame需要材料来建造的时候，需要加上已经放进去的材料是否足够
        if (frame.NeedResources().Count != 0) {
            return null;
        }

        //TODO:可能有人站在建筑上，到时候得判断建筑位置上是否有人

        //TODO:需要判断可抵达性，技能是否满足要求之类的

        if (!BuildUtility.CanBuild(thing,unit))
        {
            return null;
        }

        return JobMaker.MakeJob(ConfigType.DataManager.Instance.GetJobDefineByID(2), frame);

    }
}