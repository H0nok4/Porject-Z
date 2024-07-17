/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_ComResContainerDes : GComponent
    {
        public GTextField m_TxtName;
        public const string URL = "ui://0kazkhhceliri";

        public static UI_ComResContainerDes CreateInstance()
        {
            return (UI_ComResContainerDes)UIPackage.CreateObject("Main", "ComResContainerDes");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_TxtName = (GTextField)GetChildAt(1);
        }
    }
}