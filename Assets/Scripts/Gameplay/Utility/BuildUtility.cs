using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public static class BuildUtility
{
    public static bool CanBuild(Thing building, Thing_Unit unit, bool checkSkill = true, bool forced = false)
    {
        //TODO:������Ҫ�жϵ�λ�Ƿ����㽨��Ҫ��֮���

        return true;
    }

    public static Thing FirstBlockingThing(Thing buildingThing,Thing_Unit_Pawn builder)
    {
        Thing miniOrRebuildThing = ((buildingThing is not Blueprint b) ? null : MiniToInstallOrBuildingToReinstall(b));
        //TODO:���������ռ�ö�����ӣ���Ҫÿ�����Ӷ��ж�
        List<Thing> thingList = buildingThing.MapData.ThingMap.ThingsListAt(buildingThing.Position);
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

        Define_Thing buildingThingDef = (buildingThing is Blueprint) ? buildingThing.Def :
            (!(buildingThing is Thing_Building_Frame)) ? buildingThing.Def.blueprintDef :
            buildingThing.Def.entityBuildDef.blueprintDef;
        if (buildingThing.Def.Category == ThingCategory.Building && SpawnHelper.SpawningWipes(buildingThingDef,existThing.Def))
        {
            return true;
        }

        Define_Thing buildingEntityDefing = buildingThingDef.entityBuildDef as Define_Thing;
        if (buildingEntityDefing != null)
        {
            if (existThing.Def.Category == ThingCategory.Item)
            {
                return true;
            }
        }

        if (existThing.Def.Category == ThingCategory.Unit || (existThing.Def.Category == ThingCategory.Item && buildingThingDef.entityBuildDef.Passability == Traversability.Impassable))
        {
            return true;
        }


        return false;
    }



    private static Thing MiniToInstallOrBuildingToReinstall(Blueprint blueprint)
    {
        //TODO:�����������ǿ��Բ�ж�����°�װ�ģ��Ϳ��Գ������°�װ
        if (blueprint is Blueprint_Install installThing)
        {
            return installThing.MiniToInstallOrBuildingToReinstall;
        }

        return null;
    }
}
