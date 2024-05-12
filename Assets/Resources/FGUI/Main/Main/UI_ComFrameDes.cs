/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_ComFrameDes : GComponent
    {
        public GTextField m_TxtName;
        public GList m_ListNeedResources;
        public const string URL = "ui://0kazkhhcruxwf";

        public static UI_ComFrameDes CreateInstance()
        {
            return (UI_ComFrameDes)UIPackage.CreateObject("Main", "ComFrameDes");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_TxtName = (GTextField)GetChildAt(1);
            m_ListNeedResources = (GList)GetChildAt(3);
        }
    }
}