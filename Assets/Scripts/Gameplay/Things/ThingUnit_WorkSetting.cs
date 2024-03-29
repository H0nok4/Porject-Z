using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;

public class ThingUnit_WorkSetting
{
    public Thing_Unit Unit;

    public List<WorkGiver> UsedWorkGivers = new List<WorkGiver>();

    public ThingUnit_WorkSetting(Thing_Unit unit) {
        Unit = unit;
        foreach (var workGiverDefine in DataManager.Instance.WorkGiverDefineList)
        {
            UsedWorkGivers.Add(workGiverDefine.WorkGiver);
        }
        //TODO:后面需要用配置初始化这个
    }
}