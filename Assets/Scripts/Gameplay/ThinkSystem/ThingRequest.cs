
using System;

public struct ThingRequest {
    //TODO:后期会将Things分类，加快查询的速度，比如建造工作只需要扫描需要被建造的Things就可以了
    public ThingBuildableDefine ThingBuildableDefine;

    public ThingRequestGroup Group;

    public bool IsUndefined
    {
        get
        {
            if (ThingBuildableDefine == null)
            {
                return Group == ThingRequestGroup.Undefined;
            }

            return false;
        }
    }

    public static ThingRequest ForDefine(ThingBuildableDefine thingBuildableDefine)
    {
        ThingRequest result = default(ThingRequest);
        result.ThingBuildableDefine = thingBuildableDefine;
        result.Group = ThingRequestGroup.Undefined;
        return result;
    }

    public static ThingRequest ForGroup(ThingRequestGroup group)
    {
        ThingRequest result = default(ThingRequest);
        result.ThingBuildableDefine = null;
        result.Group = group;
        return result;
    }

    public override string ToString()
    {
        return $"Define = {ThingBuildableDefine},RequestGroup = {Group}";
    }
}

public static class ThingRequestGroupHelper
{
    public static readonly ThingRequestGroup[] AllGroups;

    static ThingRequestGroupHelper()
    {
        var values = Enum.GetValues(typeof(ThingRequestGroup));
        AllGroups = new ThingRequestGroup[values.Length];
        int index = 0;
        foreach (object value in values) {
            AllGroups[index] = (ThingRequestGroup)value;
            index++;
        }
    }

    public static bool Contains(this ThingRequestGroup group, ThingBuildableDefine thingBuildableDefine)
    {
        switch (group)
        {
            case ThingRequestGroup.Undefined:
                return false;
            case ThingRequestGroup.All:
                return true;
            case ThingRequestGroup.BuildingFrame:
                return thingBuildableDefine.IsFrame;
            case ThingRequestGroup.BuildingBlueprint:
                return thingBuildableDefine.IsBlueprint;
            default:
                return false;
        }
    } 
}

public enum ThingRequestGroup
{
    Undefined = 0,
    All = 1,
    BuildingFrame,
    BuildingBlueprint,
}