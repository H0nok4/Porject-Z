using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FairyGUI;
using Main;

public class Command_Toggle : CommandBase {
    public Action<bool> Toggle;

    public bool IsActive;

    public Command_Toggle(int id,Action<bool> toggle, bool isActive = false): base(id) {
        Toggle = toggle;
        IsActive = isActive;
    }

    public override void Execute()
    {
        IsActive = !IsActive;
        Toggle?.Invoke(IsActive);
    }

    public override ICommandUI GetUIComponent(GList list)
    {
        return (UI_BtnDesignatorType1) list.AddItemFromPool(UI_BtnDesignatorType1.URL);
    }
}