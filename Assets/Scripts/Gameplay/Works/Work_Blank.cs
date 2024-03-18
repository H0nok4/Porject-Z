using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public static class WorkMaker {
    public static Work MakeWork()
    {
        Work work = SimplePool<Work>.Get();
        work.InPool = false;
        return work;
    }

    public static void ReturnWork(Work work)
    {
        work.InPool = true;
        SimplePool<Work>.Return(work);
    }
}