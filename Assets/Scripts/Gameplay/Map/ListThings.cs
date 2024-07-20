using System;
using System.Collections;
using System.Collections.Generic;
using ConfigType;
using JetBrains.Annotations;
using UnityEngine;

public sealed class ListThings
{
    public Dictionary<int, List<Thing>> ListByDefine =
        new Dictionary<int, List<Thing>>();

    public List<Thing>[] ListByGroup;

    private static readonly List<Thing> _emptyList = new List<Thing>();

    public List<Thing> AllThing => ListByGroup[(int)ThingRequestGroup.All];

    public ListThings()
    {
        ListByGroup = new List<Thing>[ThingRequestGroupHelper.AllGroups.Length];
        ListByGroup[1] = new List<Thing>();
    }

    public List<Thing> GetThingsByGroup(ThingRequestGroup group)
    {
        return ThingsMatching(ThingRequest.ForGroup(group));
    }

    public List<Thing> GetThingsByThingDefine(ThingDefine thingDefine)
    {
        return ThingsMatching(ThingRequest.ForDefine(thingDefine));
    }

    public List<Thing> ThingsMatching(ThingRequest request)
    {
        if (request.ThingDefine != null)
        {
            if (ListByDefine.TryGetValue(request.ThingDefine.ID,out List<Thing> things))
            {
                return things;
            }

            return _emptyList;
        }

        if (request.Group != ThingRequestGroup.Undefined)
        {

            return ListByGroup[(int)request.Group] ?? _emptyList;
        }

        throw new InvalidOperationException($"错误的ThingRequest对象 + {request}");
    }

    public bool Contains(Thing thing)
    {
        return AllThing.Contains(thing);
    }

    public bool Contains(Thing thing, ThingRequestGroup specifyGroup)
    {
        if (ListByGroup[(int)specifyGroup] == null)
        {
            return false;
        }

        return ListByGroup[(int)specifyGroup].Contains(thing);
    }

    public void Add(Thing thing)
    {
        if (!ListByDefine.TryGetValue(thing.Def.ID,out var list))
        {
            list = new List<Thing>();
            ListByDefine.Add(thing.Def.ID,list);
        }

        list.Add(thing);
        var allGroup = ThingRequestGroupHelper.AllGroups;
        foreach (var thingRequestGroup in allGroup)
        {
            if (thingRequestGroup.Contains(thing.Def))
            {
                List<Thing> thingsList = ListByGroup[(int)thingRequestGroup];
                if (thingsList == null)
                {
                    thingsList = new List<Thing>();
                    ListByGroup[(int)thingRequestGroup] = thingsList;
                }

                thingsList.Add(thing);
            }
        }
    }

    public void Add(Thing thing, ThingRequestGroup specifyGroup)
    {
        var thingList = ListByGroup[(int)specifyGroup];
        if (thingList == null)
        {
            thingList = new List<Thing>();
            ListByGroup[(int)specifyGroup] = thingList;
        }

        //TODO:需要判断是否已经添加了,列表判断存在的复杂度为O(N),后面看看有没有性能问题,如果有的话得优化一下
        if (!thingList.Contains(thing))
        {
            thingList.Add(thing);
            Logger.Instance.Log($"添加了一个指定的ThingRequestGroup={specifyGroup},ThingName={thing.Name},当前组里剩下的数量为:{thingList.Count}");
        }

    }

    public void Remove(Thing thing, ThingRequestGroup specifyGroup)
    {
        var thingList = ListByGroup[(int)specifyGroup];
        if (thingList == null) {
            thingList = new List<Thing>();
            ListByGroup[(int)specifyGroup] = thingList;
            return;
        }

        //TODO:需要判断是否已经添加了,列表判断存在的复杂度为O(N),后面看看有没有性能问题,如果有的话得优化一下
        if (thingList.Contains(thing)) {
            thingList.Remove(thing);
            Logger.Instance.Log($"移除了一个指定的ThingRequestGroup={specifyGroup},ThingName={thing.Name},当前组里剩下的数量为:{thingList.Count}");

        }
    }

    public void Remove(Thing thing)
    {
        if (ListByDefine.TryGetValue(thing.Def.ID,out var list))
        {
            list.Remove(thing);
        }

        var allGroup = ThingRequestGroupHelper.AllGroups;
        foreach (var thingRequestGroup in allGroup) {
            if (thingRequestGroup.Contains(thing.Def)) {
                List<Thing> thingsList = ListByGroup[(int)thingRequestGroup];
                thingsList.Remove(thing);
            }
        }
    }

    public void Clear()
    {
        ListByDefine.Clear();
        for (int i = 0; i < ListByGroup.Length; i++)
        {
            if (ListByGroup[i] != null)
            {
                ListByGroup[i].Clear();
            }
        }
    }
}

public class DefineThingComparer : IEqualityComparer<ThingDefine> {
    public static readonly DefineThingComparer Instance = new DefineThingComparer();

    public bool Equals(ThingDefine x, ThingDefine y) {
        if (x == null && y == null) {
            return true;
        }
        if (x == null || y == null) {
            return false;
        }
        return x.ID == y.ID;
    }

    public int GetHashCode(ThingDefine obj) {
        return obj.GetHashCode();
    }
}

