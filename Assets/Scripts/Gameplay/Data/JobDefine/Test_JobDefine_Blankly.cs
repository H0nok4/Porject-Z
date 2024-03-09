using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Test_JobDefine_Blankly : JobDefine
{
    public override Type DriverClass => typeof(JobDriver_Blankly);
}