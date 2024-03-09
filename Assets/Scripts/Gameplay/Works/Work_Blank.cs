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
        return work;
    }

    public static void ReturnWork(Work work)
    {
        SimplePool<Work>.Return(work);
    }
}