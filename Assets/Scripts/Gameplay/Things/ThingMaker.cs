using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;

public static class ThingMaker {
    public static Thing MakeNewThing(ThingDefine thingDefine, ThingDefine itemThingDefine = null)
    {
        Thing newThing = (Thing)Activator.CreateInstance(thingDefine.ThingClass.ToType());
        newThing.Def = thingDefine;
        newThing.PostMake();//创建后的一些处理,例如物品血量的设置

        return newThing;
    }

    public static Thing MakeNewThing(ThingDefine thingDefine,int count, ThingDefine itemThingDefine = null) {
        Thing newThing = (Thing)Activator.CreateInstance(thingDefine.ThingClass.ToType());
        newThing.Count = count;
        newThing.Def = thingDefine;
        newThing.PostMake();//创建后的一些处理,例如物品血量的设置

        return newThing;
    }
}