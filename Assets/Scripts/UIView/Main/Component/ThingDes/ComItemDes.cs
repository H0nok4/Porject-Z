
using FairyGUI;

namespace Main {
    public partial class UI_ComItemDes : IThingDesBase {
        public void Refresh(Thing thing) {
            m_TxtName.Set(thing.Name);
        }
    }
}