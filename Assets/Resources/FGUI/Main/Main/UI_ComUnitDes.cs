/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_ComUnitDes : GComponent
    {
        public GTextField m_TxtName;
        public GList m_ListUnitNeed;
        public const string URL = "ui://0kazkhhcruxwg";

        public static UI_ComUnitDes CreateInstance()
        {
            return (UI_ComUnitDes)UIPackage.CreateObject("Main", "ComUnitDes");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_TxtName = (GTextField)GetChildAt(1);
            m_ListUnitNeed = (GList)GetChildAt(2);
        }
    }
}