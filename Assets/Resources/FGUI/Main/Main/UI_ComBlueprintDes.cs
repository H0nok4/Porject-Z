/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_ComBlueprintDes : GComponent
    {
        public GTextField m_TxtName;
        public GList m_ListNeedResources;
        public const string URL = "ui://0kazkhhcruxwe";

        public static UI_ComBlueprintDes CreateInstance()
        {
            return (UI_ComBlueprintDes)UIPackage.CreateObject("Main", "ComBlueprintDes");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_TxtName = (GTextField)GetChildAt(1);
            m_ListNeedResources = (GList)GetChildAt(3);
        }
    }
}