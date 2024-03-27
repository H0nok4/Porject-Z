using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class ThingMaker {
    public static Thing MakeNewThing(ThingBuildableDefine thingBuildableDefine, ThingBuildableDefine itemThingBuildableDefine = null)
    {
        Thing newThing = (Thing)Activator.CreateInstance(thingBuildableDefine.ThingClass.ToType());
        newThing.Def = thingBuildableDefine;
        newThing.PostMake();//创建后的一些处理,例如物品血量的设置

        return newThing;
    }
}