using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;

public static class WorkUtility_Food {

    public static Work StartEat(JobTargetIndex index)
    {
        var work = WorkMaker.MakeWork();
        var target = work.Unit.JobTracker.Job.GetTarget(index);
        work.InitAction = delegate
        {
            Debug.Log($"开始吃东西,食品ID={target.Thing.Def.ID}");
        };
        work.FinishedAction = delegate
        {
            var unit = work.Unit;
            unit.NeedTracker.Food.CurValuePercent = 1f;
            Debug.Log("吃完了,恢复食物");
        };
        work.NeedWaitingTick = 180;//TODO:暂定吃3秒
        work.CompleteMode = WorkCompleteMode.Delay;

        return work;
    }
}