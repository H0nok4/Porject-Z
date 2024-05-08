/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_MainView : GComponent
    {
        public Controller m_CtrlShowDragBox;
        public Controller m_CtrlShowDesignator;
        public GComponent m_BtnPlaceThing;
        public GList m_ListMainSelection;
        public GGraph m_DragBox;
        public UI_DesignView m_ComDesignPanel;
        public const string URL = "ui://0kazkhhcc0f50";

        public static UI_MainView CreateInstance()
        {
            return (UI_MainView)UIPackage.CreateObject("Main", "MainView");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_CtrlShowDragBox = GetControllerAt(0);
            m_CtrlShowDesignator = GetControllerAt(1);
            m_BtnPlaceThing = (GComponent)GetChildAt(0);
            m_ListMainSelection = (GList)GetChildAt(2);
            m_DragBox = (GGraph)GetChildAt(3);
            m_ComDesignPanel = (UI_DesignView)GetChildAt(4);
        }
    }
}