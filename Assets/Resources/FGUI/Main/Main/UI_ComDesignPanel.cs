/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_ComDesignPanel : GComponent
    {
        public GList m_ListType;
        public const string URL = "ui://0kazkhhcl0ax7";

        public static UI_ComDesignPanel CreateInstance()
        {
            return (UI_ComDesignPanel)UIPackage.CreateObject("Main", "ComDesignPanel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_ListType = (GList)GetChildAt(1);
        }
    }
}