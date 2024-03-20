using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ThingUtility {
    public static int GetCanStackNum(Thing main, Thing wantToStackThing)
    {
        return Math.Min(wantToStackThing.Count, main.Def.StackLimit - main.Count);
    }
}
