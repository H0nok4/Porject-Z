/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_MainView : GComponent
    {
        public Controller m_CtrlShowThingDes;
        public Controller m_CtrlShowDragBox;
        public Controller m_CtrlShowDesignator;
        public Controller m_CtrlShowListCommand;
        public GComponent m_BtnPlaceThing;
        public GList m_ListMainSelection;
        public UI_ComThingDes m_ComThingDes;
        public GGraph m_DragBox;
        public UI_ComDesignPanel m_ComDesignPanel;
        public GList m_ListCommand;
        public const string URL = "ui://0kazkhhcc0f50";

        public static UI_MainView CreateInstance()
        {
            return (UI_MainView)UIPackage.CreateObject("Main", "MainView");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_CtrlShowThingDes = GetControllerAt(0);
            m_CtrlShowDragBox = GetControllerAt(1);
            m_CtrlShowDesignator = GetControllerAt(2);
            m_CtrlShowListCommand = GetControllerAt(3);
            m_BtnPlaceThing = (GComponent)GetChildAt(0);
            m_ListMainSelection = (GList)GetChildAt(2);
            m_ComThingDes = (UI_ComThingDes)GetChildAt(3);
            m_DragBox = (GGraph)GetChildAt(4);
            m_ComDesignPanel = (UI_ComDesignPanel)GetChildAt(5);
            m_ListCommand = (GList)GetChildAt(6);
        }
    }
}