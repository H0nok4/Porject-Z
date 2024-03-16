using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Define_Buildable : BaseDefine{
    /// <summary>
    /// 看是否可以通过
    /// </summary>
    public Traversability Passability;

    public int MaxHitPoint;

    public int Workload;

    public IntVec2 Size = IntVec2.One;

    public int MoveCost;
}