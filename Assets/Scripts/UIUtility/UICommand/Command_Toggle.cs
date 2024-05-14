using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FairyGUI;
using Main;

public interface IActivableCommand {
    public bool IsActive { get; set; }
}

public class Command_Toggle : CommandBase, IActivableCommand {
    public Action<bool> Toggle;

    public bool IsActive { get; set; }

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