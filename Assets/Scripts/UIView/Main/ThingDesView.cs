using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EventSystem;
using FairyGUI;
using Main;
using UnityEngine;

namespace UI
{
    [CmdReg(EventDef.OnDeselectAllThing,EventDef.OnDeselectThing,EventDef.OnSelectThing)]
    [View("Main", "ThingDesView")]
    public class ThingDesView : FGUIView
    {
        public UI_ThingDesView _main;

        public Thing TrackedThing =>
            SelectManager.Instance.SelectThings.Count > 0 ? SelectManager.Instance.SelectThings[0] : null;

        public override void OnShow() {
            base.OnShow();

            Refresh();
        }

        public void Refresh()
        {
            RefreshTrackedThings();
            RefreshTrackedThingsCommands();
        }

        private void RefreshTrackedThingsCommands()
        {
            _main.m_ListCommand.RemoveChildrenToPool();
            if (TrackedThing == null)
            {
                return;
            }

            var commands = TrackedThing.GetCommands();
            foreach (var commandBase in commands)
            {
                var com = commandBase.GetUIComponent(_main.m_ListCommand);
                com.Refresh(commandBase);
            }
        }

        private void RefreshTrackedThings() {
            var selectThing = SelectManager.Instance.SelectThings.FirstOrDefault();
            if (selectThing == null)
            {
                _main.m_DesLoader.url = null;
                return;
            }

            _main.m_DesLoader.url = CreateLoaderByThing(selectThing);
            IThingDesBase thingDes = (IThingDesBase)_main.m_DesLoader.component;
            thingDes.Refresh(selectThing);
        }

        public override void Update() {
            RefreshTrackedThings();
        }

        private string CreateLoaderByThing(Thing thing) {
            if (thing == null) {
                return string.Empty;
            }

            if (thing is Thing_Unit) {
                return UI_ComUnitDes.URL;
            }

            return string.Empty;
        }

        public override void OnCmd(CmdData data)
        {
            base.OnCmd(data);
            switch (data.CmdName)
            {
                case EventDef.OnDeselectThing:
                    //TODO:刷新界面
                    RefreshTrackedThings();
                    break;
                case EventDef.OnDeselectAllThing:
                    //TODO:没有选中东西,把当前界面关闭
                    CloseSelf();
                    break;
                case EventDef.OnSelectThing:
                    //TODO:刷新界面
                    RefreshTrackedThings();
                    break;
            }
        }

        public override void Bind(GComponent component)
        {
            base.Bind(component);
            _main = (UI_ThingDesView)component;
        }
    }

}
