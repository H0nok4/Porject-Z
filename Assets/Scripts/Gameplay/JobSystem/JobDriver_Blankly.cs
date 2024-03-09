using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class JobDriver_Blankly : JobDriver{
    //TODO:发呆，啥事也不干几秒钟

    public override IEnumerable<Work> MakeWorks()
    {
        var work = WorkMaker.MakeWork();
        work.InitAction = delegate
        {
            Debug.Log("没什么事干，开始发呆");
        };
        work.CompleteMode = WorkCompleteMode.Delay;
        work.NeedWaitingTick = 300;
        yield return work;
        var finishWork = WorkMaker.MakeWork();
        finishWork.InitAction = delegate
        {
            Debug.Log("发呆结束了");
        };
        yield return finishWork;

    }
}