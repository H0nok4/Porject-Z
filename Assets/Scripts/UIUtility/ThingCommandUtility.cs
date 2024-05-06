using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;
using FairyGUI;


public abstract class CommandBase
{
    public int ID { get; set; }

    public DesignTypeDefine Define => DataManager.Instance.GetDesignTypeDefineByID(ID);
    public abstract void Execute();

    public CommandBase(int id) {
        ID = id;
    }

    public abstract ICommandUI GetUIComponent(GList list);
}

public interface ICommandUI
{
    void Refresh(CommandBase command);
}


public static class ThingCommandUtility {
    public static List<CommandBase> CreateCommandsByThings(IEnumerable<Thing> things)
    {
        HashSet<CommandBase> commands = new HashSet<CommandBase>();
        foreach (var thing in things)
        {
            foreach (var command in thing.GetCommands())
            {
                commands.Add(command);
            }
        }
        return commands.ToList();
    }

    public static List<CommandBase> CreateCommandsByThings(Thing thing) {
        HashSet<CommandBase> commands = new HashSet<CommandBase>();
        foreach (var command in thing.GetCommands()) {
            commands.Add(command);
        }
        return commands.ToList();
    }

}