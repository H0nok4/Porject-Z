using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FairyGUI;
using Main;
using UnityEngine;

namespace UI
{
    [View("Main", "ThingDesView")]
    public class ThingDesView : FGUIView
    {
        public UI_ThingDesView _main;

        public override void OnShow() {
            base.OnShow();

            RefreshTrackedThings();
        }

        private void RefreshTrackedThings() {
            var selectThing = SelectManager.Instance.SelectThings.First();
            if (selectThing == null) {
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

        public override void Bind(GComponent component)
        {
            base.Bind(component);
            _main = (UI_ThingDesView)component;
        }
    }

}
