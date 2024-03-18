using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ThingUnit_WorkSetting
{
    public Thing_Unit Unit;

    public List<WorkGiver> UsedWorkGivers = new List<WorkGiver>();

    public ThingUnit_WorkSetting(Thing_Unit unit) {
        Unit = unit;
        UsedWorkGivers.Add(DataTableManager.Instance.WorkGiverDefineHandler.WorkGiverDefine_BuildFrame.WorkGiver);
        //TODO:后面需要用配置初始化这个
    }
}