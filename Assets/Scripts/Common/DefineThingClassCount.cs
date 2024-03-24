using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class DefineThingClassCount
{
    public string DefineName;

    [NonSerialized]
    public Define_Thing Def;

    public int Count;
}