using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MapDataHandleThingGameObjectManager
{
    public GameObject HandleObject;

    public void Register(GameObject obj)
    {
        obj.transform.parent = HandleObject.transform;
    }


    public MapDataHandleThingGameObjectManager(GameObject handleObject)
    {

    }
}