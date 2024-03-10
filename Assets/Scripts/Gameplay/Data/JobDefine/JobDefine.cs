using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TODO:测试，后面需要用配置表生成工具生成
public abstract class JobDefine
{
    public abstract Type DriverClass { get; }

    public bool CanSuspend = true;
}