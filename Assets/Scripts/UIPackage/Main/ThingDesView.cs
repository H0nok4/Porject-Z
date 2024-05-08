using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using Main;
using UnityEngine;

namespace UI
{
    public class ThingDesView : FGUIView
    {
        public UI_ThingDesView _main;


        public override void Bind(GComponent component)
        {
            base.Bind(component);
            _main = (UI_ThingDesView)component;
        }
    }

}
