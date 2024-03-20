using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MapDataHandleThingGameObjectManager
{
    public GameObject HandleObject;

    public void Register(ThingObject obj)
    {
        obj.GO.transform.parent = HandleObject.transform;
    }


    public void UnRegister(ThingObject obj)
    {
        obj.GO.transform.parent = null;
    }

    public MapDataHandleThingGameObjectManager(GameObject handleObject)
    {
        HandleObject = handleObject;
    }
}