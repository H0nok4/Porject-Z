using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UnitCarryTracker : IThingHolder
{
    public Thing_Unit Unit;

    public ThingOwner<Thing> ThingContainer;

    public Thing CarriedThing
    {
        get
        {
            if (ThingContainer.Count == 0)
            {
                return null;
            }

            return ThingContainer[0];
        }
    }
    public IThingHolder ParentOwner { get; }
    public void GetChildren(List<IThingHolder> outChildren)
    {
        throw new NotImplementedException();
    }

    public ThingOwner GetHoldingThing()
    {
        throw new NotImplementedException();
    }
}