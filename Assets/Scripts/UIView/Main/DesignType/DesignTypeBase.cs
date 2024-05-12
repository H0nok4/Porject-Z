using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;

public abstract class DesignTypeBase
{
    public abstract DesignTypeDefine Define { get; }
    public abstract IEnumerable<DesignatorDecoratorBase> GetDesignators();
}