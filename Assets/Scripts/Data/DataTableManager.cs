using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class DataTableManager : Singleton<DataTableManager> {
    private GameObject _thingObject;

    public GameObject ThingObject {
        get {
            if (_thingObject == null) {
                _thingObject = Resources.Load<GameObject>("GameObject/ThingObject");
            }

            return _thingObject;
        }
    }


}
