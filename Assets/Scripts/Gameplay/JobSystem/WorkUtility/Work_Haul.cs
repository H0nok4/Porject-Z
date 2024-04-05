using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEditor.Rendering;
using UnityEngine;

public static class Work_Haul {
    public static bool CanCarryThing(Thing_Unit unit, Thing haulThing)
    {
        if (!haulThing.Spawned)
        {
            Debug.LogError("想要拣起不存在的东西");
            return false;
        }

        if (haulThing.Count == 0)
        {
            Debug.LogError("想要捡起数量为0的东西");
            return false;
        }

        if (unit.JobTracker.Job.Count <= 0)
        {
            unit.JobTracker.Job.Count = 1;
        }

        return true;
    }

    public static Work CarryThingToContainer()
    {
        Work work = WorkMaker.MakeWork();
        work.InitAction = delegate
        {
            var unit = work.Unit;
            var targetThing = unit.JobTracker.Job.GetTarget(JobTargetIndex.B);
            work.Unit.PathMover.StartPath(new PawnPath(PathFinder.AStarFindPath(unit, targetThing.Position,PathMoveEndType.Touch)));
        };
        //TODO:添加失败条件
        work.CompleteMode = WorkCompleteMode.PathMoveEnd;
        return work;
    }

    public static Work StartCarryThing(JobTargetIndex targetIndex,bool failIfCountLessThanNeed = false,bool reserve = true)
    {
        Work work = WorkMaker.MakeWork();
        work.InitAction = delegate
        {
            Thing_Unit unit = work.Unit;
            Job curJob = unit.JobTracker.Job;
            Thing targetThing = curJob.GetTarget(targetIndex).Thing;
            if (CanCarryThing(unit,targetThing))
            {
                int thingSpace = unit.CarryTracker.GetThingSpaceCountByDef(targetThing.Def);
                if (thingSpace == 0)
                {
                    throw new Exception("搬运工作想要搬运无法携带的东西");
                }

                if (failIfCountLessThanNeed && targetThing.Count < curJob.Count)
                {
                    unit.JobTracker.EndCurrentJob(JobEndCondition.Incompletable);
                }
                else
                {
                    int canCarryNum = Mathf.Min(thingSpace, curJob.Count, targetThing.Count);
                    if (canCarryNum <= 0)
                    {
                        throw new Exception("可搬运的数量为0");
                    }

                    var stackNum = targetThing.Count;
                    var carryNum = unit.CarryTracker.TryCarryThing(targetThing, canCarryNum, reserve);
                    if (carryNum == 0)
                    {
                        unit.JobTracker.EndCurrentJob(JobEndCondition.Incompletable);
                    }
                    //TODO:可能需要拿多个物体才能凑齐
                    //if (carryNum < stackNum)
                    //{
                        
                    //}
                }

            }
        };

        return work;
    }

    public static Work PutHauledThingIntoContainer(JobTargetIndex jobTargetIndex, JobTargetIndex jobTargetIndex1)
    {
        Work work = WorkMaker.MakeWork();
        work.InitAction = delegate
        {
            Thing_Unit unit = work.Unit;
            Job curJob = unit.JobTracker.Job;
            if (unit.CarryTracker.CarriedThing == null)
            {
                Debug.LogError("有搬运物体的工作但是单位没有拿物体");
            }
        }
    }
}