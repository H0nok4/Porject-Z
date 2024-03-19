using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New JobDefine", menuName = "Define/Create JobDefine")]
public class JobDefine : BaseDefine
{
    public EditableType DriverClass;

    public bool CanSuspend = true;
}