using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public static class ThingUtility {
    public static int GetCanStackNum(Thing main, Thing wantToStackThing)
    {
        return Math.Min(wantToStackThing.Count, main.Def.StackLimit - main.Count);
    }

    /// <summary>
    /// 因为所有Frame都为一个类，所以可以直接创建Frame配置
    /// </summary>
    /// <returns></returns>
    private static ThingBuildableDefine BaseFrameDef() {
        return new ThingBuildableDefine() {
            IsFrame = true,
            //ThingClass = new EditableType(){TypeName = "Thing_Building_Frame" },
            UseHitPoint = true,
            Selectable = true,
            Category = ThingCategory.Building,
            Destroyable = true,
            ThingClass = new EditableType(){TypeName = "Thing_Building_Frame" },
            FrameSprite = DataTableManager.Instance.FrameSprite,
    };
    }

    private static ThingBuildableDefine BaseBlueprintDef()
    {
        return new ThingBuildableDefine()
        {
            IsBlueprint = true,
            Category = ThingCategory.Mirage,
            ThingClass = new EditableType(){TypeName = "Thing_Blueprint_Building" },
            Selectable = true,
            
        };
    }

    public static void CreateFrameDefToThingDef(ThingBuildableDefine def)
    {
        Debug.Log("创建了一个Frame配置");
        var baseFrameDef = BaseFrameDef();
        //TODO:Frame点击的时候还是需要一些建成之后的建筑的信息，比如名称，工作量之类的
        baseFrameDef.Size = def.Size;
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

    public static void CreateBlueprintDefToThingDef(ThingBuildableDefine thingBuildableDefineBuildable)
    {
        Debug.Log("创建了一个蓝图配置");
        var baseBlueprintDef = BaseBlueprintDef();
        baseBlueprintDef.Size = thingBuildableDefineBuildable.Size;
        baseBlueprintDef.Name = thingBuildableDefineBuildable.Name;
        baseBlueprintDef.Rotatable = thingBuildableDefineBuildable.Rotatable;
        baseBlueprintDef.ThingClass = new EditableType() { TypeName = "Thing_Blueprint_Building" };
        baseBlueprintDef.ThingSprite = thingBuildableDefineBuildable.BlueprintSprite;
        baseBlueprintDef.EntityBuildDef = thingBuildableDefineBuildable;
        thingBuildableDefineBuildable.BlueprintDef = baseBlueprintDef;
    }
}
