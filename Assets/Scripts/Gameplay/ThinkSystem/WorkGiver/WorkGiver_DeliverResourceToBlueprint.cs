using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;

public class WorkGiver_DeliverResourceToBlueprint : WorkGiver_DeliverResourceTo
{
    public override ThingRequest ThingRequest => ThingRequest.ForGroup(ThingRequestGroup.BuildingBlueprint);


    public override Job JobOnThing(Thing_Unit unit, Thing thing, bool forced = false)
    {
        if (!(thing is Blueprint blueprint))
        {
            return null;
        }

        if (BuildUtility.FirstBlockingThing(blueprint,(Thing_Unit_Pawn)unit) != null)
        {
            //TODO:有东西正在阻挡建造蓝图，看情况需要搬离之类的
            return BuildUtility.HandleBlockingThingJob(blueprint, unit, forced);
        }

        if (!BuildUtility.CanBuild(blueprint, unit))
        {
            return null;
        }

        var haulJob = DeliverResourceJobFor(unit, blueprint);
        if (haulJob != null)
        {
            return haulJob;
        }
        //没有需要运送的物资，直接返回建造
        var immediatlyBuildJob = ImmediatlyBuildBlueprintToFrame(unit, blueprint);
        if (immediatlyBuildJob != null)
        {
            return immediatlyBuildJob;
        }

        return null;
    }

    private Job ImmediatlyBuildBlueprintToFrame(Thing_Unit unit,IBuildable build)
    {
        //不用消耗物资，直接把蓝图改造成Frame

        if (build is Blueprint bluePrint && bluePrint.NeedResources().Count == 0)
        {
            Job job = JobMaker.MakeJob(ConfigType.DataManager.Instance.GetJobDefineByID(4));
            job.InfoA = bluePrint;
            return job;
        }

        return null;
    }

}