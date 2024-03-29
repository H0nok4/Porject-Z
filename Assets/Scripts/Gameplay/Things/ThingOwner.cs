using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO:还需要一个泛型的ThingOwner
public abstract class ThingOwner : IList<Thing>
{
    //TODO:可以管理一系列的物体，可以用作背包或者发射仓之类的
    public IThingHolder Owner;

    public ThingOwner()
    {

    }

    public ThingOwner(IThingHolder owner)
    {
        Owner = owner;
    }

    /// <summary>
    /// 背包中的食物之类的可能会过期，所以需要Tick一下
    /// </summary>
    public void ThingOwnerTick(bool removeIfDestroyed)
    {
        //遍历都需要从后往前
        for (int i = Count - 1; i >= 0; i--)
        {
            Thing item = GetAt(i);
            //TODO:有的物品可能不需要Tick，需要跳过来节省时间
            item.Tick();
            if (item.IsDestroyed && removeIfDestroyed)
            {
                Remove(item);
            }
        }
    }




    Thing IList<Thing>.this[int index]
    {
        get => GetAt(index);
        set => throw new System.NotImplementedException();
    }

    public abstract Thing GetAt(int index);

    public abstract bool TryAdd(Thing thing, int count, bool canMerge = true);

    public abstract bool TryAdd(Thing thing, bool canMerge = true);

    public abstract int IndexOf(Thing thing);

    public abstract bool Remove(Thing thing);


    public Thing Take(Thing thing, int count)
    {
        if (!Contains(thing))
        {
            Debug.LogError("想要取出不在库存中的物品");
            return null;
        }

        if (count > thing.Count)
        {
            Debug.LogError($"想要取出数量为{count}个的物品，但是只有{thing.Count}个");
            count = thing.Count;
        }

        if (count == thing.Count)
        {
            //TODO:取出的同时需要移除
            Remove(thing);
            return thing;
        }

        Thing splitThing = thing.SplitOff(count);
        return splitThing;
    }

    public void Add(Thing item)
    {
        TryAdd(item);
    }

    public void Clear()
    {
        //链表从后往前清理
        for (int i = Count - 1; i >= 0; i--)
        {
            Remove(GetAt(i));
        }
    }

    public bool Contains(Thing item)
    {
        if (item == null)
            return false;

        return item.HoldingOwner == this;
    }

    public bool Contains(ThingBuildableDefine def)
    {
        return Contains(def, 1);
    }

    /// <summary>
    /// 判断是否拥有某种配置的物体
    /// </summary>
    /// <param name="def"></param>
    /// <param name="minCount">大于等于这个数值的才会判断为已拥有</param>
    /// <returns></returns>
    public bool Contains(ThingBuildableDefine def, int minCount)
    {
        if (minCount <= 0)
        {
            //常理上来说不应该有这种情况
            return true;
        }

        int totalCount = 0;
        for (int i = 0; i < Count; i++)
        {
            var thing = GetAt(i);
            if (thing.Def == def)
            {
                //累计数量
                totalCount += thing.Count;
            }

            if (totalCount >= minCount)
            {
                return true;
            }
        }

        return false;


    }

    public void CopyTo(Thing[] array, int arrayIndex)
    {
        for (int i = 0; i < Count; i++)
        {
            array[i + arrayIndex] = GetAt(i);
        }
    }

    public void Insert(int index, Thing item) {
        throw new InvalidOperationException("不允许插入");
    }

    public void RemoveAt(int index) {
        if (index < 0 || index >= Count)
        {
            Debug.LogError($"想要移除的索引大于ThingOwner持有的物品数量，当前物品数量为:{Count},想要移除的位置为:{index}");
            return;
        }

        Remove(GetAt(index));
    }
    /// <summary>
    /// 指的是库存种类
    /// </summary>
    public abstract int Count { get; }

    public bool IsReadOnly { get; }

    public IEnumerator<Thing> GetEnumerator()
    {
        for (int i = 0; i < Count; i++) {
            yield return GetAt(i);
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        for (int i = 0; i < Count; i++) {
            yield return GetAt(i);
        }
    }
}
