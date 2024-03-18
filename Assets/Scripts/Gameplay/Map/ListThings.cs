using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public sealed class ListThings
{
    public Dictionary<Define_Thing, List<Thing>> ListByDefine =
        new Dictionary<Define_Thing, List<Thing>>(DefineThingComparer.Instance);

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

    public List<Thing> GetThingsByThingDefine(Define_Thing define)
    {
        return ThingsMatching(ThingRequest.ForDefine(define));
    }

    public List<Thing> ThingsMatching(ThingRequest request)
    {
        if (request.ThingDefine != null)
        {
            if (ListByDefine.TryGetValue(request.ThingDefine,out List<Thing> things))
            {
                return things;
            }

            return _emptyList;
        }

        if (request.Group != ThingRequestGroup.Undefined)
        {

            return ListByGroup[(int)request.Group] ?? _emptyList;
        }

        throw new InvalidOperationException($"´íÎóµÄThingRequest¶ÔÏó + {request}");
    }

    public bool Contains(Thing thing)
    {
        return AllThing.Contains(thing);
    }

    public void Add(Thing thing)
    {
        if (!ListByDefine.TryGetValue(thing.Def,out var list))
        {
            list = new List<Thing>();
            ListByDefine.Add(thing.Def,list);
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

    public void Remvoe(Thing thing)
    {
        if (ListByDefine.TryGetValue(thing.Def,out var list))
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

public class DefineThingComparer : IEqualityComparer<Define_Thing> {
    public static readonly DefineThingComparer Instance = new DefineThingComparer();

    public bool Equals(Define_Thing x, Define_Thing y) {
        if (x == null && y == null) {
            return true;
        }
        if (x == null || y == null) {
            return false;
        }
        return x.UniqueID == y.UniqueID;
    }

    public int GetHashCode(Define_Thing obj) {
        return obj.GetHashCode();
    }
}

