using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Test_JobDefine_WalkAround : JobDefine
{
    public override Type DriverClass => typeof(JobDriver_WalkAround);

    public static Test_JobDefine_WalkAround _walkAroundDefine = new Test_JobDefine_WalkAround();

    public static Test_JobDefine_WalkAround WalkAroundDefine
    {
        get
        {
            return _walkAroundDefine;
        }
    }
}