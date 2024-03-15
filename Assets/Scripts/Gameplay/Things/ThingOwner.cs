using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO:����Ҫһ�����͵�ThingOwner
public abstract class ThingOwner : IList<Thing>
{
    //TODO:���Թ���һϵ�е����壬���������������߷����֮���
    public IThingHolder Owner;

    public ThingOwner()
    {

    }

    public ThingOwner(IThingHolder owner)
    {
        Owner = owner;
    }

    /// <summary>
    /// �����е�ʳ��֮��Ŀ��ܻ���ڣ�������ҪTickһ��
    /// </summary>
    public void ThingOwnerTick(bool removeIfDestroyed)
    {
        //��������Ҫ�Ӻ���ǰ
        for (int i = Count - 1; i >= 0; i--)
        {
            Thing item = GetAt(i);
            //TODO:�е���Ʒ���ܲ���ҪTick����Ҫ��������ʡʱ��
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
            Debug.LogError("��Ҫȡ�����ڿ���е���Ʒ");
            return null;
        }

        if (count > thing.Count)
        {
            Debug.LogError($"��Ҫȡ������Ϊ{count}������Ʒ������ֻ��{thing.Count}��");
            count = thing.Count;
        }

        if (count == thing.Count)
        {
            //TODO:ȡ����ͬʱ��Ҫ�Ƴ�
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
        //����Ӻ���ǰ����
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

    public bool Contains(Define_Thing def)
    {
        return Contains(def, 1);
    }

    /// <summary>
    /// �ж��Ƿ�ӵ��ĳ�����õ�����
    /// </summary>
    /// <param name="def"></param>
    /// <param name="minCount">���ڵ��������ֵ�ĲŻ��ж�Ϊ��ӵ��</param>
    /// <returns></returns>
    public bool Contains(Define_Thing def, int minCount)
    {
        if (minCount <= 0)
        {
            //��������˵��Ӧ�����������
            return true;
        }

        int totalCount = 0;
        for (int i = 0; i < Count; i++)
        {
            var thing = GetAt(i);
            if (thing.Def == def)
            {
                //�ۼ�����
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
        throw new InvalidOperationException("���������");
    }

    public void RemoveAt(int index) {
        if (index < 0 || index >= Count)
        {
            Debug.LogError($"��Ҫ�Ƴ�����������ThingOwner���е���Ʒ��������ǰ��Ʒ����Ϊ:{Count},��Ҫ�Ƴ���λ��Ϊ:{index}");
            return;
        }

        Remove(GetAt(index));
    }
    /// <summary>
    /// ָ���ǿ������
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
