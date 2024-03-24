using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "JobDefineHandler", menuName = "Define Handler/Create JobDefineHandler")]
public class JobDefineHandler : ScriptableObject
{
    public JobDefine BlanklyDefine;

    public JobDefine BuildFrame;

    public JobDefine WalkAround;

    public JobDefine HaulToCell;

    public JobDefine ImmediatelyBuildBlueprintToFrame;
}
