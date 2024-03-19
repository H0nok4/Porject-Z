using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BaseDefine : ScriptableObject
{
    public string Name;

    private int _uniqueID = -1;

    public int UniqueID {
        get {
            if (_uniqueID < 0) {
                _uniqueID = UniqueIDUtility.GetUID();
            }

            return _uniqueID;
        }
    }
}