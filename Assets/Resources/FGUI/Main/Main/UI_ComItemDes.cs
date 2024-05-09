/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_ComItemDes : GComponent
    {
        public GTextField m_TxtName;
        public GList m_ListUnitNeed;
        public const string URL = "ui://0kazkhhcruxwh";

        public static UI_ComItemDes CreateInstance()
        {
            return (UI_ComItemDes)UIPackage.CreateObject("Main", "ComItemDes");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_TxtName = (GTextField)GetChildAt(1);
            m_ListUnitNeed = (GList)GetChildAt(2);
        }
    }
}