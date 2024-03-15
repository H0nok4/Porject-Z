using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThingHolder {
    IThingHolder Parent { get; }

    void GetChildren(List<IThingHolder> outChildren);

    ThingOwner GetOwner();
}
