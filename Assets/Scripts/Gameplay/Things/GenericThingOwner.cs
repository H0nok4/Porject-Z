using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;

public class ThingOwner<T> : ThingOwner where T : Thing {
    public ThingOwner() {
    }

    public ThingOwner(IThingHolder owner)
        : base(owner) {
    }

    public ThingOwner(IThingHolder owner, bool oneStackOnly)
        : base(owner, oneStackOnly) {
    }

    private List<T> _things = new List<T>();

    public override Thing GetAt(int index) {
        return _things[index];
    }

    public override int TryAdd(Thing addThing, int count, bool canMerge = true) {
        if (count <= 0) {
            return 0;
        }
        if (addThing == null) {
            return 0;
        }

        if (Contains(addThing)) {
            Debug.LogWarning("尝试重复添加物体");
            return 0;
        }

        if (addThing.HoldingOwner != null) {
            Debug.LogWarning("尝试添加一个已经有持有者的物品");
            return 0;
        }

        if (!CanAcceptAnyOf(addThing, canMerge)) {
            return 0;
        }

        int currentCount = addThing.Count;
        int splitCount = Math.Min(currentCount, count);
        Thing splitThing = addThing.SplitOff(splitCount);
        if (!TryAdd((T)splitThing, canMerge)) {
            if (splitThing != addThing) {
                int result = currentCount - addThing.Count - splitThing.Count;
                addThing.TryAbsorbStack(splitThing, false);
                return result;
            }

            return currentCount - addThing.Count;
        }

        return splitCount;
    }



    public override bool TryAdd(Thing addThing, bool canMerge = true) {
        if (addThing == null) {

            return false;
        }

        if (!(addThing is T item)) {
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

        if (canMerge) {
            //尝试合并进去
            for (int i = 0; i < _things.Count; i++) {
                T hadThing = _things[i];
                if (!hadThing.CanStackWith(addThing)) {
                    continue;
                }

                int canAddNum = Math.Min(addThing.Count, hadThing.Def.StackLimit - hadThing.Count);

                if (canAddNum > 0) {
                    //Thing splitThing = addThing.SplitOff(canAddNum);
                    int curHadNum = hadThing.Count;
                    hadThing.TryAbsorbStack(addThing, true);
                    if (hadThing.Count > curHadNum) {
                        //TODO:增加了数量,需要发出事件
                    }

                    if (addThing.IsDestroyed || addThing.Count == 0) {
                        return true;
                    }
                }
            }
        }

        if (Count >= _maxStacks) {
            return false;
        }
        //直接添加进来
        addThing.HoldingOwner = this;
        _things.Add(item);
        //TODO:发出事件

        return true;
    }

    public override int IndexOf(Thing thing) {
        if (!(thing is T item)) {
            return -1;
        }
        return _things.IndexOf(item);
    }

    public override bool Remove(Thing thing) {
        if (!Contains(thing)) {
            return false;
        }

        if (thing.HoldingOwner == this) {
            thing.HoldingOwner = null;
        }

        _things.Remove((T)thing);
        return true;
    }

    public override int Count => _things.Count;

    public bool TryGiveToOtherContainer(Thing item, ThingOwner otherContainer, bool canMergeWithExitsThing = true) {
        return TryGiveToOtherContainer(item, otherContainer, item.Count, canMergeWithExitsThing) == item.Count;
    }

    public int TryGiveToOtherContainer(Thing item, ThingOwner otherContainer, int count, bool canMergeWithExitsThing = true) {
        return TryGiveToOtherContainer(item, otherContainer, count, out Thing _, canMergeWithExitsThing);
    }

    public int TryGiveToOtherContainer(Thing item, ThingOwner otherContainer, int count, out Thing resultItem,
        bool canMergeWithExitsThing = true) {
        //TODO:尝试将物品存入ThingOwner中
        if (!Contains(item)) {
            Debug.LogError("想要转移不属于自己的物品给别的容器");
            resultItem = null;
            return 0;
        }

        if (otherContainer == this && count > 0) {
            resultItem = item;
            return item.Count;
        }

        if (!otherContainer.CanAcceptAnyOf(item, canMergeWithExitsThing)) {
            resultItem = null;
            return 0;
        }

        if (count <= 0) {
            resultItem = null;
            return 0;
        }

        if (Owner is MapData || otherContainer is MapData)
        {
            resultItem = null;
            return 0;
        }

        var canGiveToNum = Mathf.Min(item.Count, count);
        Thing giveThing = item.SplitOff(canGiveToNum);

        if (Contains(giveThing))
        {
            //如果全部都给别人了,就移除该物体
            Debug.Log("把材料都给别人了");
            Remove(giveThing);
        }
        else
        {
            Debug.Log($"给出去了{giveThing.Count}个材料,还剩{item.Count}个材料");
        }

        if (otherContainer.TryAdd(giveThing,canMergeWithExitsThing))
        {
            resultItem = giveThing;
            Debug.Log($"成功给出");
            return giveThing.Count;
        }
        //没有添加成功
        Debug.Log($"给出失败了,需要放回自己身上");
        resultItem = null;
        if (!otherContainer.Contains(resultItem) && resultItem.Count > 0 && !resultItem.IsDestroyed)
        {
            //当前剩余的数量
            int remainNum = canGiveToNum - giveThing.Count;
            //是分离出来的物体
            if (item != giveThing)
            {
                //合并进去
                item.TryAbsorbStack(giveThing, false);
                return remainNum;
            }
            //不是分离的,说明把item直接转移了,再把它加回去
            TryAdd(giveThing, false);
            return remainNum;
        }

        return item.Count;
    }
}