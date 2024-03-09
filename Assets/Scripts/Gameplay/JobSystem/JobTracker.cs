using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ThingUnit_JobTracker
{
    public Thing_Unit Unit;

    public Job Job;

    public JobDriver JobDriver;

    //TODO:可以加一个队列，让单位可以按顺序工作

    public ThingUnit_JobTracker(Thing_Unit unit) {
        Unit = unit;
    }

    public void StartJob(Job job)
    {
        
    }
}