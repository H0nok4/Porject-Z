using System.Collections;
using System.Collections.Generic;
using EventSystem;
using UI;
using UnityEngine;

public class SelectManager : Singleton<SelectManager> {
    public readonly List<Thing> SelectThings = new List<Thing>();

    public int SelectThingCount => SelectThings.Count;

    public void ClearSelectThings() {
        foreach (var selectThing in SelectThings)
        {
            if (selectThing.Spawned) {
                selectThing.GameObject.DeSelect();
            }
        }
        SelectThings.Clear();


    }

    public void SetSelectThings(List<Thing> thingList) {
        ClearSelectThings();
        SelectThings.AddRange(thingList);
        foreach (var selectThing in SelectThings)
        {
            if (selectThing.Spawned) {
                selectThing.GameObject.Select();
            }
        }

        UIManager.Instance.SendUIEvent(EventDef.OnSelectThing);
    }

    public void SetSelectThings(Thing thing) {
        ClearSelectThings();
        SelectThings.Add(thing);
        thing.GameObject.Select();

        UIManager.Instance.SendUIEvent(EventDef.OnSelectThing);
    }

    public void AddSelectThings(List<Thing> thingList) {
        SelectThings.AddRange(thingList);
        foreach (var thing in thingList)
        {
            thing.GameObject.Select();
        }

        UIManager.Instance.SendUIEvent(EventDef.OnSelectThing);
    }

    public void AddSelectThings(Thing thing) {
        SelectThings.Add(thing);
        thing.GameObject.Select();

        UIManager.Instance.SendUIEvent(EventDef.OnSelectThing);
    }

    public void RemoveSelectThing(Thing thing)
    {
        if(!IsSelected(thing))
            return;

        SelectThings.Remove(thing);
    }

    public bool IsSelected(Thing thing)
    {
        return SelectThings.Contains(thing);
    }
}
