/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using UnityEngine;

namespace Main {
    public partial class UI_ComThingDes : GComponent {
        public void RefreshAsFrame(Thing_Building_Frame frame) {
            m_CtrlThingType.SetSelectedIndex(3);
            m_TxtName.Set(frame.Def.Name);
            m_ListNeedResources.RemoveChildrenToPool();

            var needResrouces = frame.NeedResources();
            foreach (var defineThingClassCount in needResrouces)
            {
                var resourcesCom = (UI_ComBuildNeedResource)m_ListNeedResources.AddItemFromPool();
                resourcesCom.m_TxtNum.RefreshValue("name",defineThingClassCount.Def.Name);
                var max = frame.Def.EntityBuildDef.CostList.Find((costRes) => costRes.Def.ID == defineThingClassCount.Def.ID).Count;
                resourcesCom.m_TxtNum.RefreshValue("cur", max - defineThingClassCount.Count);
                resourcesCom.m_TxtNum.RefreshValue("max",max);
            }
        }

        public void RefreshAsBlueprint(Thing_Blueprint_Building blueprint) {
            m_CtrlThingType.SetSelectedIndex(2);
            m_TxtName.Set(blueprint.Def.Name);
            m_ListNeedResources.RemoveChildrenToPool();

            var needResources = blueprint.NeedResources();
            foreach (var defineThingClassCount in needResources) {
                var resourcesCom = (UI_ComBuildNeedResource)m_ListNeedResources.AddItemFromPool();
                resourcesCom.m_TxtNum.RefreshValue("name", defineThingClassCount.Def.Name);
                resourcesCom.m_TxtNum.RefreshValue("cur",0);
                resourcesCom.m_TxtNum.RefreshValue("max",defineThingClassCount.Count);
            }
        }

        public void RefreshAsUnit(Thing_Unit thing)
        {
            m_CtrlThingType.SetSelectedIndex(1);
            m_TxtName.Set(thing.Def.Name);
            m_ListUnitNeed.numItems = thing.NeedTracker.Needs.Count;
            for (int i = 0; i < m_ListUnitNeed.numItems; i++) {
                var comNeed = (UI_ComNeed)m_ListUnitNeed._children[i];
                comNeed.Refresh(thing.NeedTracker.Needs[i]);
            }

            var commands = ThingCommandUtility.CreateCommandsByThings(thing);
            m_ListCommand.RemoveChildrenToPool();
            foreach (var thingUiCommand in commands)
            {
                //TODO:通过command创建一个UI
                var commandSlot = thingUiCommand.GetUIComponent(m_ListCommand);
                commandSlot.Refresh(thingUiCommand);
            }
        }

        //public GObject GetUIByCommand(GList list,CommandBase commandBase)
        //{
        //    if (commandBase is Command_Toggle)
        //    {
        //        return list.AddItemFromPool(UI_BtnDesignatorType1.URL);
        //    }


        //    Debug.LogError($"意料之外的Command类型,CommandType={commandBase.GetType()}");
        //    return null;
        //}

        public void RefreshAsItem(Thing thing)
        {
            m_CtrlThingType.SetSelectedIndex(5);
            m_TxtName.Set(thing.Def.Name);
        }

        public void RefreshAsBuilding(Thing thing)
        {
            m_CtrlThingType.SetSelectedIndex(4);
            m_TxtName.Set(thing.Def.Name);
        }
    }
}