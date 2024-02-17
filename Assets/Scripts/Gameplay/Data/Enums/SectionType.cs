using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum SectionType
{
    //空气，走到这一格会向下掉落到最近一层非空气的地方
    Air = 0,
    //地面可以行走
    Floor = 1,
    //墙壁无法行走
    Wall = 2,
    //楼梯可以行走
    Stair = 3,
}