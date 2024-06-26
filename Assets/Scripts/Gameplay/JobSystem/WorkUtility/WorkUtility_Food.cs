using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;

public static class WorkUtility_Food {

    public static Work StartEat(JobTargetIndex containerTargetIndex)
    {
        var work = WorkMaker.MakeWork();


        work.InitAction = delegate
        {
            Thing_Unit unit = work.Unit;
            Job curJob = unit.JobTracker.Job;
            if (unit.CarryTracker.CarriedThing == null) {
                Debug.LogError("有搬运物体的工作但是单位没有拿物体");
            }
            else
            {
                var target = work.Unit.JobTracker.Job.GetTarget(containerTargetIndex).Thing;
                var thingOwner = target.TryGetThingOwner();
                if (thingOwner != null && thingOwner == work.Unit.CarryTracker.ThingContainer)
                {
                    Debug.Log("当前拿着的就是想吃的");
                }
                else
                {
                    Debug.LogError("当前拿着的东西不是准备吃的东西");
                    unit.JobTracker.EndCurrentJob(JobEndCondition.Error);
                }


            }

        };
        work.FinishedAction = delegate
        {
            var unit = work.Unit;
            unit.NeedTracker.Food.CurValuePercent = 1f;
            Debug.Log("吃完了,恢复食物");
            unit.CarryTracker.CarriedThing.Destroy();
        };
        work.NeedWaitingTick = 180;//TODO:暂定吃3秒
        work.CompleteMode = WorkCompleteMode.Delay;

        return work;
    }
}