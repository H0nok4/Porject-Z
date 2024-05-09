/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main {
    public partial class UI_ComUnitDes : IThingDesBase {
        public void Refresh(Thing thing) {
            m_TxtName.Set(thing.Def.Name);
        }
    }
}