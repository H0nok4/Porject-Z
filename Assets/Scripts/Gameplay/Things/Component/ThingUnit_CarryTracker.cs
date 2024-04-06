using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;

public class ThingUnit_CarryTracker : IThingHolder
{
    public Thing_Unit Unit;

    public ThingOwner<Thing> ThingContainer;

    public Thing CarriedThing
    {
        get
        {
            if (ThingContainer.Count == 0)
            {
                return null;
            }

            return ThingContainer[0];
        }
    }

    public ThingUnit_CarryTracker(Thing_Unit unit)
    {
        Unit = unit;
        ThingContainer = new ThingOwner<Thing>(this,true);
    }

    public IThingHolder ParentOwner { get; }

    public void GetChildren(List<IThingHolder> outChildren)
    {
        ThingOwnerUtility.GetThingHolderFromThings(outChildren, GetCurrentHoldingThings());
    }

    /// <summary>
    /// 获得拿取物品的剩余空间
    /// </summary>
    /// <param name="def"></param>
    /// <returns></returns>
    public int GetThingSpaceCountByDef(ThingDefine def)
    {
        int maxStackNum = GetMaxStackNum(def);
        if (CarriedThing != null)
        {
            maxStackNum -= CarriedThing.Count;
        }

        return maxStackNum;
    }

    public int GetMaxStackNum(ThingDefine def)
    {
        //TODO:后面可以有负重设置

        return def.StackLimit;
    }

    public ThingOwner GetCurrentHoldingThings()
    {
        return ThingContainer;
    }

    public int TryCarryThing(Thing targetThing, int canCarryNum, bool reserve)
    {
        if (Unit.IsDead || Unit.IsDown)
        {
            return 0;
        }

        canCarryNum = Mathf.Min(canCarryNum,GetThingSpaceCountByDef(targetThing.Def));
        canCarryNum = Mathf.Min(canCarryNum, targetThing.Count);
        var splitCarryThing = targetThing.SplitOff(canCarryNum);
        var addNum = ThingContainer.TryAdd(splitCarryThing, canCarryNum);
        if (addNum > 0)
        {
            //TODO:可以播放捡起的声音
            if (reserve)
                Unit.Reserve(CarriedThing, Unit.JobTracker.Job);
            //TODO:如果已经被选中，需要重新选中

        }

        return addNum;
    }
}