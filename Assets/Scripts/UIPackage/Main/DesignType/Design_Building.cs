using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;

public class Design_Building : DesignTypeBase {
    public override IEnumerable<DesignatorDecoratorBase> GetDesignators()
    {
        yield return new BuildingDesignator(DataManager.Instance.GetThingDefineByID(2));
    }
}