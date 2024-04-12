/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_ComBuildNeedResource : GComponent
    {
        public GTextField m_TxtNum;
        public const string URL = "ui://0kazkhhcmk936";

        public static UI_ComBuildNeedResource CreateInstance()
        {
            return (UI_ComBuildNeedResource)UIPackage.CreateObject("Main", "ComBuildNeedResource");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_TxtNum = (GTextField)GetChildAt(0);
        }
    }
}