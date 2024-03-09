using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Work
{
    public Thing_Unit Unit;

    public WorkCompleteMode CompleteMode = WorkCompleteMode.Instant;

    public int NeedWaitingTick;

    public Action TickAction;

    public Action InitAction;

    public Thing_Unit GetActor()
    {
        return Unit;
    }

}