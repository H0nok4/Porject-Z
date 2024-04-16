/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_ComDesignatorType : GComponent
    {
        public GTextField m_TxtName;
        public const string URL = "ui://0kazkhhcl0ax8";

        public static UI_ComDesignatorType CreateInstance()
        {
            return (UI_ComDesignatorType)UIPackage.CreateObject("Main", "ComDesignatorType");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_TxtName = (GTextField)GetChildAt(1);
        }
    }
}