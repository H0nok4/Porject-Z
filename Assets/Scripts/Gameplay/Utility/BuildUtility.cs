using System.Collections;
using System.Collections.Generic;
using ConfigType;
using UnityEngine;
using UnityEngine.Rendering;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public static class BuildUtility
{
    public static bool CanBuild(Thing building, Thing_Unit unit, bool checkSkill = true, bool forced = false)
    {
        //TODO:后面需要判断单位是否满足建筑要求之类的

        return true;
    }

    public static Thing FirstBlockingThing(Thing buildingThing,Thing_Unit_Pawn builder)
    {
        Thing miniOrRebuildThing = ((buildingThing is not Blueprint b) ? null : MiniToInstallOrBuildingToReinstall(b));
        //TODO:建筑后面会占用多个格子，需要每个格子都判断
        List<Thing> thingList = buildingThing.MapData.ThingMap.ThingsListAt(buildingThing.Position.Pos);
        foreach (var thing in thingList)
        {
            if (BuildingBlocked(buildingThing,thing))
            {
                return thing;
            }
        }

        return null;
    }

    public static bool BuildingBlocked(Thing buildingThing, Thing existThing)
    {
        if (buildingThing == existThing)
        {
            return false;
        }

        ThingDefine buildingDef = (buildingThing is Blueprint) ? buildingThing.Def :
            (!(buildingThing is Thing_Building_Frame)) ? buildingThing.Def.BlueprintDef :
            buildingThing.Def.EntityBuildDef.BlueprintDef;
        if (buildingThing.Def.Category == ThingCategory.Building && SpawnHelper.SpawningWipes(buildingDef,existThing.Def))
        {
            return true;
        }

        ThingDefine buildingEntityDefing = buildingDef.EntityBuildDef as ThingDefine;
        if (buildingEntityDefing != null)
        {
            if (existThing.Def.Category == ThingCategory.Item)
            {
                return true;
            }
        }

        if (existThing.Def.Category == ThingCategory.Unit || (existThing.Def.Category == ThingCategory.Item && buildingDef.EntityBuildDef.Passability == Traversability.Impassable))
        {
            return true;
        }



        return false;
    }



    private static Thing MiniToInstallOrBuildingToReinstall(Blueprint blueprint)
    {
        //TODO:如果这个建筑是可以拆卸来重新安装的，就可以尝试重新安装
        //if (blueprint is Blueprint_Install installThing)
        //{
        //    return installThing.MiniToInstallOrBuildingToReinstall;
        //}

        return null;
    }

    public static Job HandleBlockingThingJob(IBuildable blueprint, Thing_Unit unit, bool forced)
    {
        var blockThing = FirstBlockingThing((Thing)blueprint, (Thing_Unit_Pawn)unit);
        if (blockThing == null)
        {
            return null;
        }
        //TODO:如果是物品，需要搬离
        if (blockThing.Def.Category == ThingCategory.Item)
        {
            if (HaulUtility.CanHaulAside(unit,blockThing,out PosNode haulResultNode))
            {
                return HaulUtility.HaulAsideJobFor(unit,blockThing);
            }
        }
        //TODO:其他的后面处理


        return null;
    }

    public static int GetNeedItemCount(IBuildable thing, ThingDefine defineCountDef)
    {
        foreach (var needResources in thing.NeedResources())
        {
            if (needResources.Def.ID == defineCountDef.ID)
            {
                return needResources.Count;
            }
        }

        return 0;
    }
}
