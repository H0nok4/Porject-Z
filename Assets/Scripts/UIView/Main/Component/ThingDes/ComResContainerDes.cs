using FairyGUI;
using FairyGUI.Utils;

namespace Main {
    public partial class UI_ComResContainerDes : IThingDesBase {
        public void Refresh(Thing thing)
        {
            m_TxtName.Set(thing.Name);
        }
    }
}