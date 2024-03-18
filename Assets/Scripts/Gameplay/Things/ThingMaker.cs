using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class ThingMaker {
    public static Thing MakeNewThing(Define_Thing define, Define_Thing itemDefine = null)
    {
        Thing newThing = (Thing)Activator.CreateInstance(define.ThingClass.ToType());
        newThing.Def = define;
        newThing.PostMake();//创建后的一些处理,例如物品血量的设置

        return newThing;
    }
}