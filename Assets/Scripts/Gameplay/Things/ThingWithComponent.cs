using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ThingWithComponent : Thing {
    private List<ThingComponentBase> _componentList;

    public static List<ThingComponentBase> EmptyComponentList = new List<ThingComponentBase>();
    public List<ThingComponentBase> ComponentList {
        get {
            if (_componentList == null) {
                return EmptyComponentList;
            }
            return _componentList;
        }
    }

}