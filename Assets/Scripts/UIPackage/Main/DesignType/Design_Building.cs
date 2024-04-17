using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;

public class Design_Building : DesignTypeBase {
    //TODO:后面可以做到
    public override DesignTypeDefine Define => DataManager.Instance.GetDesignTypeDefineByID(1);


    public override IEnumerable<DesignatorDecoratorBase> GetDesignators()
    {
        yield return new BuildingDesignator(DataManager.Instance.GetThingDefineByID(2));
    }
}