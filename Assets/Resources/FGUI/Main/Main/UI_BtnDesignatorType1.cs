/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_BtnDesignatorType1 : GComponent
    {
        public Controller m_CtrlType;
        public Controller m_CtrlToggleActive;
        public GLoader m_LoaderIcon;
        public GTextField m_TxtName;
        public const string URL = "ui://0kazkhhcl0ax9";

        public static UI_BtnDesignatorType1 CreateInstance()
        {
            return (UI_BtnDesignatorType1)UIPackage.CreateObject("Main", "BtnDesignatorType1");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_CtrlType = GetControllerAt(0);
            m_CtrlToggleActive = GetControllerAt(1);
            m_LoaderIcon = (GLoader)GetChildAt(1);
            m_TxtName = (GTextField)GetChildAt(2);
        }
    }
}