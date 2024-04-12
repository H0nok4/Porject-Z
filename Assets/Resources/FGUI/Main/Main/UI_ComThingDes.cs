/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_ComThingDes : GComponent
    {
        public Controller m_CtrlThingType;
        public GTextField m_TxtName;
        public GList m_ListNeedResources;
        public const string URL = "ui://0kazkhhcf1nd3";

        public static UI_ComThingDes CreateInstance()
        {
            return (UI_ComThingDes)UIPackage.CreateObject("Main", "ComThingDes");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_CtrlThingType = GetControllerAt(0);
            m_TxtName = (GTextField)GetChildAt(1);
            m_ListNeedResources = (GList)GetChildAt(4);
        }
    }
}