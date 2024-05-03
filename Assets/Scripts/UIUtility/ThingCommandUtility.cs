using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class ThingUICommand
{
    public int ID { get; set; }
    public abstract void Execute();
}
public static class ThingCommandUtility {
    public static List<ThingUICommand> CreateCommandsByThings(IEnumerable<Thing> thing)
    {
        HashSet<ThingUICommand> commands = new HashSet<ThingUICommand>();
        
        return commands.ToList();
    }

    public static ThingUICommand GetThingUICommandByThing(Thing thing)
    {

    }

}