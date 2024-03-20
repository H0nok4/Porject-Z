using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Blueprint_Install : Blueprint{
    //TODO:安装类型的蓝图
    public override float WorkTotal { get; }

    public Thing MinifiedThing;//已经拆卸下来的建筑

    public Thing ReinstallBuildingThing;//准备重新安装的建筑

    public Thing MiniToInstallOrBuildingToReinstall
    {
        get
        {
            if (MinifiedThing != null)
            {
                return MinifiedThing;
            }

            if (ReinstallBuildingThing != null)
            {
                return ReinstallBuildingThing;
            }

            Debug.LogError("安装类型的蓝图没有来源");
            return null;
        }
    }
}