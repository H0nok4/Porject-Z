using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class CollectionUtility {
    public static bool IsNullOrEmpty(this IEnumerable list) {
        if (list == null)
        {
            return true;
        }

        int index = 0;
        foreach (var _ in list)
        {
            index++;
        }

        if (index == 0)
        {
            return true;
        }

        return false;
    }
}