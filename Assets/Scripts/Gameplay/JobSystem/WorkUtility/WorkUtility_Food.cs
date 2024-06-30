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
            Debug.LogError("开始吃食物");

        };
        work.FinishedAction = delegate
        {
            var unit = work.Unit;
            var target = (Thing_Item)unit.JobTracker.Job.GetTarget(0).Thing;
            //TODO:后面需要加个吃东西的接口
            unit.NeedTracker.Food.CurValuePercent = 1;
            Debug.Log("吃完了,恢复食物");
            unit.CarryTracker.CarriedThing.Destroy();

        };
        work.NeedWaitingTick = 180;//TODO:暂定吃3秒
        work.CompleteMode = WorkCompleteMode.Delay;

        return work;
    }
}