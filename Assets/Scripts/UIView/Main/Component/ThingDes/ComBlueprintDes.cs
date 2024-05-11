using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

namespace Main {
    public partial class UI_ComBlueprintDes :  IThingDesBase{
        public void Refresh(Thing thing) {
            m_TxtName.Set(thing.Name);

        }
    }
}

