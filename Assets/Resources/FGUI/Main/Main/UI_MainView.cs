/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_MainView : GComponent
    {
        public Controller m_CtrlShowThingDes;
        public GComponent m_BtnBuildWall;
        public GComponent m_BtnPlaceThing;
        public GList m_ListMainSelection;
        public GComponent m_ComThingDes;
        public const string URL = "ui://0kazkhhcc0f50";

        public static UI_MainView CreateInstance()
        {
            return (UI_MainView)UIPackage.CreateObject("Main", "MainView");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_CtrlShowThingDes = GetControllerAt(0);
            m_BtnBuildWall = (GComponent)GetChildAt(0);
            m_BtnPlaceThing = (GComponent)GetChildAt(1);
            m_ListMainSelection = (GList)GetChildAt(3);
            m_ComThingDes = (GComponent)GetChildAt(4);
        }
    }
}