/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_ThingDesView : GComponent
    {
        public GList m_ListThingBtn;
        public GList m_ListCommand;
        public GLoader m_DesLoader;
        public const string URL = "ui://0kazkhhcf1nd3";

        public static UI_ThingDesView CreateInstance()
        {
            return (UI_ThingDesView)UIPackage.CreateObject("Main", "ThingDesView");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_ListThingBtn = (GList)GetChildAt(0);
            m_ListCommand = (GList)GetChildAt(1);
            m_DesLoader = (GLoader)GetChildAt(2);
        }
    }
}