using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ThingOwnerUtility {
    public static void GetThingHolderFromThings(List<IThingHolder> outHolder, IList<Thing> owner)
    {
        if (owner == null)
        {
            return;
        }

        for (int i = 0; i < owner.Count; i++)
        {
            if (owner[i] is IThingHolder holder)
            {
                outHolder.Add(holder);
            }

            if (owner[i] is ThingWithComponent thingWithComp)
            {
                List<ThingComponentBase> allComponent = thingWithComp.ComponentList;
                foreach (var thingComponentBase in allComponent)
                {
                    if (thingComponentBase is IThingHolder comHolder)
                    {
                        outHolder.Add(comHolder);
                    }
                }
            }
        }
    }
}