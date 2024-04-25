/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Main
{
    public partial class UI_ComNeed : GComponent
    {
        public GTextField m_TxtTitle;
        public GProgressBar m_ComBar;
        public const string URL = "ui://0kazkhhcnrf9c";

        public static UI_ComNeed CreateInstance()
        {
            return (UI_ComNeed)UIPackage.CreateObject("Main", "ComNeed");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            m_TxtTitle = (GTextField)GetChildAt(0);
            m_ComBar = (GProgressBar)GetChildAt(1);
        }
    }
}