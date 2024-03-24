//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;

//public class Blueprint_Install : Blueprint{
//    //TODO:安装类型的蓝图
//    public override float WorkTotal { get; }
//    protected override Thing MakeSolidThing(out bool shouldSelect) {
        
//    }

//    public override IReadOnlyList<DefineThingClassCount> NeedResources() {
//        //TODO:被指定安装到这个位置的建筑或者已经卸下来变成的物品
//        throw new NotImplementedException();
//    }

//    public Thing MinifiedThing;//已经拆卸下来的建筑

//    public Thing ReinstallBuildingThing;//准备重新安装的建筑

//    public Thing MiniToInstallOrBuildingToReinstall
//    {
//        get
//        {
//            if (MinifiedThing != null)
//            {
//                return MinifiedThing;
//            }

//            if (ReinstallBuildingThing != null)
//            {
//                return ReinstallBuildingThing;
//            }

//            Debug.LogError("安装类型的蓝图没有来源");
//            return null;
//        }
//    }
//}