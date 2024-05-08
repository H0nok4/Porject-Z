using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using Main;
using UI;
using UnityEngine;

namespace UI
{
    public class DesignView : FGUIView
    {
        public UI_DesignView _main;

        public override void Bind(GComponent component)
        {
            base.Bind(component);
            _main = (UI_DesignView)component;
        }
    }

}
