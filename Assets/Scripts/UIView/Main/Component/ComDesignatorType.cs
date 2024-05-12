using FairyGUI;
using FairyGUI.Utils;

namespace Main {
    public partial class UI_ComDesignatorType : GComponent
    {
        public DesignTypeBase DesignType;

        public void Refresh(DesignTypeBase designType)
        {
            DesignType = designType;
            m_TxtName.text = designType.Define.Name;
        }


    }
}