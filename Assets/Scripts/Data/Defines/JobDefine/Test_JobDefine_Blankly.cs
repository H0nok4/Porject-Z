using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Test_JobDefine_Blankly : JobDefine
{
    public override Type DriverClass => typeof(JobDriver_Blankly);

    public static Test_JobDefine_Blankly _blanklyJob = new Test_JobDefine_Blankly();
    public static Test_JobDefine_Blankly BlanklyJob
    {
        get
        {
            return _blanklyJob;
        }
    }
}