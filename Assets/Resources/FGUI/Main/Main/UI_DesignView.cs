/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_DesignView : GComponent
    {
        public Controller m_CtrlShowListCommand;
        public UI_ComDesignTypePanel m_ComDesignTypePanel;
        public GList m_ListCommand;
        public const string URL = "ui://0kazkhhcl0ax7";

        public static UI_DesignView CreateInstance()
        {
            return (UI_DesignView)UIPackage.CreateObject("Main", "DesignView");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_CtrlShowListCommand = GetControllerAt(0);
            m_ComDesignTypePanel = (UI_ComDesignTypePanel)GetChildAt(0);
            m_ListCommand = (GList)GetChildAt(1);
        }
    }
}