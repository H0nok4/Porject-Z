using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThingHolder {
    IThingHolder ParentOwner { get; }

    void GetChildren(List<IThingHolder> outChildren);

    ThingOwner GetCurrentHoldingThings();
}
