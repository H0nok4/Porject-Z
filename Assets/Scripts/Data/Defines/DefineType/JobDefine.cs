using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;

[Serializable]
public class JobDefine : BaseDefine
{
    public EditableType DriverClass;

    public bool CanSuspend = true;
}