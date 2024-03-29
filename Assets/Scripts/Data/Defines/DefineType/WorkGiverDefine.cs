using System;
using ConfigType;
using UnityEngine;

[Serializable]
public class WorkGiverDefine : BaseDefine
{
    public EditableType WorkGiverType;

    public WorkTypeDefine WorkTypeDefine;

    public bool ScanThings = true;

    public bool ScanSections;

    private WorkGiver _workGiver;

    public WorkGiver WorkGiver
    {
        get
        {
            if (_workGiver == null)
            {
                _workGiver = (WorkGiver)Activator.CreateInstance(WorkGiverType.ToType());
                _workGiver.Def = this;
            }

            return _workGiver;  
        }
    }
}