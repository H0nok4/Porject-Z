/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main {
    public partial class UI_ComUnitDes : IThingDesBase {
        public void Refresh(Thing thing) {
            var unit = (Thing_Unit) thing;
            m_TxtName.Set(thing.Def.Name);
            //TODO:刷新需求
            m_ListUnitNeed.numItems = unit.NeedTracker.Needs.Count;
            for (int i = 0; i < unit.NeedTracker.Needs.Count; i++) {
                var comNeed = (UI_ComNeed) m_ListUnitNeed._children[i];
                comNeed.Refresh(unit.NeedTracker.Needs[i]);
            }
        }
    }
}