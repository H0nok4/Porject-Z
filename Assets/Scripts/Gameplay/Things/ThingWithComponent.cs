using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ThingWithComponent : Thing {
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

    public override void Tick()
    {
        base.Tick();

    }

    

    public override void SpawnSetup(MapData mapData)
    {
        base.SpawnSetup(mapData);

        var com = GameObject.GO.AddComponent<DrawItemNum>();
        com.Init(this);
    }
}