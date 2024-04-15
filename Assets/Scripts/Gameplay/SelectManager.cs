using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectManager : Singleton<SelectManager> {
    public readonly List<Thing> SelectThings = new List<Thing>();

    public void ClearSelectThings() {
        SelectThings.Clear();
    }

    public void SetSelectThings(List<Thing> thingList) {
        ClearSelectThings();
        SelectThings.AddRange(thingList);
    }

    public void SetSelectThings(Thing thing) {
        ClearSelectThings();
        SelectThings.Add(thing);
    }

    public void AddSelectThings(List<Thing> thingList) {
        SelectThings.AddRange(thingList);
    }

    public void AddSelectThings(Thing thing) {
        SelectThings.Add(thing);
    }
}
