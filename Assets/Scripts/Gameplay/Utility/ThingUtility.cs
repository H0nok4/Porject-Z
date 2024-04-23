using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public static class ThingUtility {
    public static int GetCanStackNum(Thing main, Thing wantToStackThing)
    {
        return Math.Min(wantToStackThing.Count, main.Def.StackLimit - main.Count);
    }

    public static Traversability GetThingTraversability(Thing thing) {
        if (thing.Def.IsBlueprint)
            return Traversability.CanStand;
        if (thing.Def.IsFrame)
            return Traversability.OnlyThrough;

        return thing.Def.Passability;
    }
    /// <summary>
    /// 因为所有Frame都为一个类，所以可以直接创建Frame配置
    /// </summary>
    /// <returns></returns>
    private static ThingDefine BaseFrameDef() {
        return new ThingDefine() {
            IsFrame = true,
            //ThingClass = new EditableType(){TypeName = "Thing_Building_Frame" },
            UseHitPoint = true,
            Selectable = true,
            Category = ThingCategory.Building,
            Destroyable = true,
            ThingClass = new EditableType(){TypeName = "Thing_Building_Frame" },
            FrameSpritePath = DataManager.Instance.FrameSpritePath,
            Passability = Traversability.OnlyThrough,
    };
    }

    private static ThingDefine BaseBlueprintDef()
    {
        return new ThingDefine()
        {
            IsBlueprint = true,
            Category = ThingCategory.Mirage,
            ThingClass = new EditableType(){TypeName = "Thing_Blueprint_Building" },
            Selectable = true,
            Destroyable = true,
            Passability = Traversability.CanStand
        };
    }

    public static void CreateFrameDefToThingDef(ThingDefine def)
    {
        Debug.Log("创建了一个Frame配置");
        var baseFrameDef = BaseFrameDef();
        //TODO:Frame点击的时候还是需要一些建成之后的建筑的信息，比如名称，工作量之类的
        baseFrameDef.ID = def.ID;
        baseFrameDef.SizeX = def.SizeX;
        baseFrameDef.SizeY = def.SizeY;
        baseFrameDef.Name = def.Name;
        baseFrameDef.Passability = def.Passability;
        baseFrameDef.Rotatable = def.Rotatable;
        baseFrameDef.Workload = def.Workload;
        if (def.IsBlueprint)
        {
            baseFrameDef.EntityBuildDef = def.EntityBuildDef;
        }
        else
        {
            baseFrameDef.EntityBuildDef = def;
        }

        def.FrameDef = baseFrameDef;
    }

    public static void CreateBlueprintDefToThingDef(ThingDefine thingDefine)
    {
        Debug.Log("创建了一个蓝图配置");
        var baseBlueprintDef = BaseBlueprintDef();
        baseBlueprintDef.ID = thingDefine.ID;
        baseBlueprintDef.SizeX = thingDefine.SizeX;
        baseBlueprintDef.SizeY = thingDefine.SizeY;
        baseBlueprintDef.Name = thingDefine.Name;
        baseBlueprintDef.Rotatable = thingDefine.Rotatable;
        baseBlueprintDef.ThingClass = new EditableType() { TypeName = "Thing_Blueprint_Building" };
        baseBlueprintDef.ThingSpritePath = thingDefine.BlueprintSpritePath;
        baseBlueprintDef.EntityBuildDef = thingDefine;
        thingDefine.BlueprintDef = baseBlueprintDef;
    }

    public static int TryAbsorbNum(Thing main, Thing other, bool respectStackLimit)
    {
        if (respectStackLimit)
        {
            return Math.Min((main.Def.StackLimit - main.Count), other.Count);
        }

        return other.Count;
    }
}
