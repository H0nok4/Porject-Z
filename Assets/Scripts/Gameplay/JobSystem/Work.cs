using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using Unity.VisualScripting;

public class Work
{
    public string DebugName { get; set; }

    public Thing_Unit Unit;

    public WorkCompleteMode CompleteMode = WorkCompleteMode.Instant;

    public int NeedWaitingTick;

    public Action TickAction;

    public Action InitAction;

    public Action FinishedAction;

    public bool InPool;

    public bool AtomicWithPrevious;

    public Thing_Unit GetActor()
    {
        return Unit;
    }

    public void ClearAll()
    {
        Unit = null;
        CompleteMode = WorkCompleteMode.Instant;
        NeedWaitingTick = 0;
        TickAction = null;
        InitAction = null;
        FinishedAction = null;
        AtomicWithPrevious = false;

        if (FinishedAction == null)
        {
            return;
        }

        FinishedAction.Invoke();
    }

    public void Clean() {
        if (FinishedAction == null) {
            return;
        }

        FinishedAction.Invoke();
    }
}