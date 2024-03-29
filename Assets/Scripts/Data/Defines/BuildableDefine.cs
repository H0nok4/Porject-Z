using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;

public abstract class BuildableDefine : BaseDefine{
    /// <summary>
    /// 看是否可以通过
    /// </summary>
    public Traversability Passability;

    public int MaxHitPoint;

    public int Workload;

    public bool Rotatable = true;

    public IntVec2 Size = IntVec2.One;

    public int MoveCost;

    public Sprite BlueprintSprite;

    public Sprite ThingSprite;

    public Sprite FrameSprite;

    [NonSerialized]
    public ThingDefine EntityBuildDef;

    [SerializeField]
    private List<DefineThingClassCount> _costThing;
    public IReadOnlyList<DefineThingClassCount> CostList
    {
        get
        {
            if (_costThing == null)
            {
                _costThing = new List<DefineThingClassCount>();
            }

            return _costThing;
        }
    }

    public bool Selectable;


}