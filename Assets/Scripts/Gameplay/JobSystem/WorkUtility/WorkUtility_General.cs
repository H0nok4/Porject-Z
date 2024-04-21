using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;

public static class WorkUtility_General {
    public static Work DoAtomic(Action action)
    {
        Work work = WorkMaker.MakeWork();
        work.InitAction = action;
        work.CompleteMode = WorkCompleteMode.Instant;
        work.AtomicWithPrevious = true;
        return work;
    }
}