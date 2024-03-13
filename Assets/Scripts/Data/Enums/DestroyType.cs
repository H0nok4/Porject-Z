using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum DestroyType
{
    None,
    KillFinalize,//击杀
    Deconstruct,//取消建筑
    FailConstruction,//建筑失败
    Cancel//取消（例如蓝图，半成品）
}