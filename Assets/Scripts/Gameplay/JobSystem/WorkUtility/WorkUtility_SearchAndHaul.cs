using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;

public static class WorkUtility_SearchAndHaul {
    public static Work HaulContainerThingImmediately(JobTargetIndex targetIndex)
    {
        Work work = WorkMaker.MakeWork("HaulContainerThingImm");
        //TODO:直接搬运容器中的物品,获取物品的数量一直到搬运者的承重上限
        return work;
    }
}