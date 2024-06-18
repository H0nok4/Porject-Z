using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using UnityEngine;

public class ThingUnit_NeedTracker {
    public List<Need> Needs = new List<Need>();

    public Thing_Unit Unit;

    public long PreUpdateTick;



    public FoodNeed Food {
        get {
            foreach (var need in Needs) {
                if (need.NeedDef.Type == NeedType.Food)
                    return (FoodNeed)need;
            }

            return null;
        }
    }

    public ThingUnit_NeedTracker(Thing_Unit unit) {
        //TODO:添加Need实例
        Unit = unit;
        foreach (var needDefine in DataManager.Instance.NeedDefineList) {
            if (!CanTrackNeed(needDefine, unit)) {
                continue;
            }

            if (needDefine.NeedClass == null || needDefine.NeedClass.TypeName.IsNullOrEmpty()) {
                continue;
            }

            var needType = needDefine.NeedClass.ToType();
            if (needType == null) {
                Debug.LogError($"不存在的Need类型，NeedDefine.Type = {needDefine.Type}");
                continue;
            }
            var needInstance = (Need)Activator.CreateInstance(needType);
            needInstance.Init(needDefine,unit);
            Needs.Add(needInstance);
        }
    }

    private const int UpdateTick = 60;

    public void Tick() {
        //TODO:每60Tick更新一次
        if (Unit.IsDead) {
            return;
        }

        if (GameTicker.Instance.CurrentTick - PreUpdateTick < UpdateTick) {
            return;
        }

        foreach (var need in Needs) {
            need.Tick(UpdateTick);
        }

        PreUpdateTick = GameTicker.Instance.CurrentTick;
    }

    public bool CanTrackNeed(NeedDefine needDefine, Thing_Unit unit) {
        if (needDefine.ActiveOnPawn && unit is Thing_Unit_Pawn) {
            return true;
        }

        return false;
    }


}