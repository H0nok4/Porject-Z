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

    private static List<DefineThingClassCount> MissingResources = new List<DefineThingClassCount>();

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

    public Job DeliverResourceJobFor(Thing_Unit unit, IBuildable build)
    {
        MissingResources.Clear();
        foreach (var defineCount in build.NeedResources())
        {
            //TODO:找到地图上存在的允许互动的同类物品运过去
            var exitsThing = MapController.Instance.Map.ListThings.GetThingsByThingDefine(defineCount.Def);
            if (exitsThing.Count > 0)
            {
                //TODO:先暂时直接获取所有物品数量来测试
                int avaliableItemCount = exitsThing.Sum((thing) => thing.Count);
                Debug.Log($"当前有{avaliableItemCount}个物品");
                var haulToContainerJob = JobMaker.MakeJob(DataManager.Instance.GetJobDefineByID(6));
                haulToContainerJob.InfoA = exitsThing[0];
                //TODO:首先，可能会有多个需要资源的建筑，能的话就一趟拿完，尽量拿齐之后按顺序把资源放到蓝图那边
                var needResourcesBuilding =
                    FindNearbyNeeders(unit, defineCount, build, avaliableItemCount, out int needItemNum);

                needResourcesBuilding.Add((Thing)build);

            }

        }

        return null;
    }

    private HashSet<Thing> FindNearbyNeeders(Thing_Unit unit, DefineThingClassCount defineCount, IBuildable build, int avaliableItemCount, out int needItemCount)
    {
        //TODO:暂时用找到所有相同的建筑蓝图来测试
        needItemCount = defineCount.Count;
        var result = new HashSet<Thing>();
        var allThing = MapController.Instance.Map.ListThings.GetThingsByGroup(ThingRequestGroup.BuildingBlueprint);
        foreach (var thing in allThing)
        {
            if (needItemCount >= avaliableItemCount)
            {
                break;
            }

            if (!(thing is Blueprint blueprint))
            {
                continue;
            }

            int otherThingNeedCount = BuildUtility.GetNeedItemCount((IBuildable)thing, defineCount.Def);
            if (otherThingNeedCount > 0)
            {
                result.Add(thing);
                needItemCount += otherThingNeedCount;
            }

        }

        return result;
    }
}