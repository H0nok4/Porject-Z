/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main {
    public partial class UI_ComNeed : GComponent {
        public void Refresh(Need need) {
            m_TxtTitle.Set(need.NeedDef.Name);
            m_ComBar.max = 1;
            m_ComBar.value = need.CurValuePercent;
        }
    }
}