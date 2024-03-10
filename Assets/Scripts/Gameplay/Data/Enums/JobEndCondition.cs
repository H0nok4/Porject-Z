using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum JobEndCondition
{
    None,//一般为异常
    Ongoing,//还在执行，没有结束
    Successed,//正常结束
    Incompletable,//中途发现做不了了，比如寻路路径不对，去干活活取消了之类的
    ForceEnd,//强制停止，会打断或者挂起当前的工作
    Error,//出现了报错
}