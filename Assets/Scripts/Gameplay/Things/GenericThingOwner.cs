﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

public class ThingOwner<T> : ThingOwner where T : Thing
{
    public ThingOwner() {
    }

    public ThingOwner(IThingHolder owner)
        : base(owner) {
    }

    public ThingOwner(IThingHolder owner, bool oneStackOnly)
        : base(owner, oneStackOnly) {
    }

    private List<T> _things = new List<T>();

    public override Thing GetAt(int index)
    {
        return _things[index];
    }

    public override int TryAdd(Thing addThing, int count, bool canMerge = true)
    {
        if (count <= 0)
        {
            return 0;
        }
        if (addThing == null)
        {
            return 0;
        }

        if (Contains(addThing))
        {
            Debug.LogWarning("尝试重复添加物体");
            return 0;
        }

        if (addThing.HoldingOwner != null)
        {
            Debug.LogWarning("尝试添加一个已经有持有者的物品");
            return 0;
        }

        if (!CanAcceptAnyOf(addThing,canMerge))
        {
            return 0;
        }

        int currentCount = addThing.Count;
        int splitCount = Math.Min(currentCount, count);
        Thing splitThing = addThing.SplitOff(splitCount);
        if (!TryAdd((T)splitThing,canMerge))
        {
            if (splitThing != addThing)
            {
                int result = currentCount - addThing.Count - splitThing.Count;
                addThing.TryAbsorbStack(splitThing,false);
                return result;
            }

            return currentCount - addThing.Count;
        }

        return splitCount;
    }



    public override bool TryAdd(Thing addThing, bool canMerge = true)
    {
        if (addThing == null)
        {
            
            return false;
        }

        if (!(addThing is T item))
        {
            return false;
        }

        if (Contains(addThing)) {
            Debug.LogWarning("尝试重复添加物体");
            return false;
        }

        if (addThing.HoldingOwner != null) {
            Debug.LogWarning("尝试添加一个已经有持有者的物品");
            return false;
        }

        if (!CanAcceptAnyOf(addThing, canMerge)) {
            return false;
        }

        if (canMerge)
        {
            //尝试合并进去
            for (int i = 0; i < _things.Count; i++)
            {
                T hadThing = _things[i];
                if (!hadThing.CanStackWith(addThing))
                {
                    continue;
                }

                int canAddNum = Math.Min(addThing.Count, hadThing.Def.StackLimit - hadThing.Count);

                if (canAddNum > 0)
                {
                    Thing splitThing = addThing.SplitOff(canAddNum);
                    int curHadNum = hadThing.Count;
                    hadThing.TryAbsorbStack(addThing, true);
                    if (hadThing.Count > curHadNum)
                    {
                        //TODO:增加了数量,需要发出事件
                    }

                    if (addThing.IsDestroyed || addThing.Count == 0)
                    {
                        return true;
                    }
                }
            }
        }

        if (Count >= _maxStacks)
        {
            return false;
        }
        //直接添加进来
        addThing.HoldingOwner = this;
        _things.Add(item);
        //TODO:发出事件

        return true;
    }

    public override int IndexOf(Thing thing)
    {
        throw new NotImplementedException();
    }

    public override bool Remove(Thing thing)
    {
        throw new NotImplementedException();
    }

    public override int Count => _things.Count;
}