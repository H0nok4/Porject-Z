/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_ComThingDes : GComponent
    {
        public Controller m_CtrlThingType;
        public Controller m_CtrlShowDragBox;
        public GTextField m_TxtName;
        public GList m_ListNeedResources;
        public GList m_ListUnitNeed;
        public GList m_ListThingBtn;
        public GList m_ListCommand;
        public const string URL = "ui://0kazkhhcf1nd3";

        public static UI_ComThingDes CreateInstance()
        {
            return (UI_ComThingDes)UIPackage.CreateObject("Main", "ComThingDes");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_CtrlThingType = GetControllerAt(0);
            m_CtrlShowDragBox = GetControllerAt(1);
            m_TxtName = (GTextField)GetChildAt(1);
            m_ListNeedResources = (GList)GetChildAt(3);
            m_ListUnitNeed = (GList)GetChildAt(4);
            m_ListThingBtn = (GList)GetChildAt(5);
            m_ListCommand = (GList)GetChildAt(6);
        }
    }
}