using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;

[Serializable]
public class DefineThingClassCount
{
    public string DefineName;

    [NonSerialized]
    public ThingDefine Def;

    public int Count;
}