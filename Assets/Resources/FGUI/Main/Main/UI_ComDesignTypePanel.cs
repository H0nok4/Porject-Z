/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_ComDesignTypePanel : GComponent
    {
        public GList m_ListType;
        public const string URL = "ui://0kazkhhcipbld";

        public static UI_ComDesignTypePanel CreateInstance()
        {
            return (UI_ComDesignTypePanel)UIPackage.CreateObject("Main", "ComDesignTypePanel");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_ListType = (GList)GetChildAt(1);
        }
    }
}