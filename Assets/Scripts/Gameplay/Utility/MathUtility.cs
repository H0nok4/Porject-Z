using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class MathUtility
{
    public static int PositiveMod(int num,int mod)
    {
        return (num % mod + num) % mod;
    }

}
