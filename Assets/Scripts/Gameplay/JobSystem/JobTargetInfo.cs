using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct JobTargetInfo {
    public Thing Thing;

    public Section Section;

    public Thing_Unit Unit => Thing as Thing_Unit;


}