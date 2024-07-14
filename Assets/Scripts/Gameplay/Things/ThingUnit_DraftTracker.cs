using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class ThingUnit_DraftTracker
{
    public Thing_Unit Unit;

    private bool _isDraft;
    public bool IsDraft {
        get => _isDraft;
        set
        {
            _isDraft = value;
            Logger.Instance?.Log("单位被征召");
        }
    }

    public ThingUnit_DraftTracker(Thing_Unit unit)
    {
        Unit = unit;
    }

}
