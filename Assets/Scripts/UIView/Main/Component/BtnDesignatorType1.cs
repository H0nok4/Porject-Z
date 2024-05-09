/** This is an automatically generated class by FairyGUI. Please do not modify it. **/
using FairyGUI;
using FairyGUI.Utils;

namespace Main {
    public partial class UI_BtnDesignatorType1 : GComponent,ICommandUI {
        public void Refresh(CommandBase command)
        {
            m_TxtName.Set(command.Define.Name);
            onClick.Set(command.Execute);
        }
    }
}