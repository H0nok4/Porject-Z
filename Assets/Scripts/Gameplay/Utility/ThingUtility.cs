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
    private static Define_Thing BaseFrameDef() {
        return new Define_Thing() {
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

    private static Define_Thing BaseBlueprintDef()
    {
        return new Define_Thing()
        {
            IsBlueprint = true,
            Category = ThingCategory.Mirage,
            ThingClass = new EditableType(){TypeName = "Thing_Blueprint_Building" },
            Selectable = true,
            
        };
    }

    public static void CreateFrameDefToThingDef(Define_Thing thingDef)
    {
        Debug.Log("创建了一个Frame配置");
        var baseFrameDef = BaseFrameDef();
        //TODO:Frame点击的时候还是需要一些建成之后的建筑的信息，比如名称，工作量之类的
        baseFrameDef.Size = thingDef.Size;
        baseFrameDef.Name = thingDef.Name;
        baseFrameDef.Passability = thingDef.Passability;
        baseFrameDef.Rotatable = thingDef.Rotatable;
        if (thingDef.IsBlueprint)
        {
            baseFrameDef.EntityBuildDef = thingDef.EntityBuildDef;
        }
        else
        {
            baseFrameDef.EntityBuildDef = thingDef;
        }

        thingDef.FrameDef = baseFrameDef;
    }

    public static void CreateBlueprintDefToThingDef(Define_Thing defineBuildable)
    {
        Debug.Log("创建了一个蓝图配置");
        var baseBlueprintDef = BaseBlueprintDef();
        baseBlueprintDef.Size = defineBuildable.Size;
        baseBlueprintDef.Name = defineBuildable.Name;
        baseBlueprintDef.Rotatable = defineBuildable.Rotatable;
        baseBlueprintDef.ThingClass = new EditableType() { TypeName = "Thing_Blueprint_Building" };
        baseBlueprintDef.ThingSprite = defineBuildable.BlueprintSprite;
        baseBlueprintDef.EntityBuildDef = defineBuildable;
        defineBuildable.BlueprintDef = baseBlueprintDef;
    }
}
