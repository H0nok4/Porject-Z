using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum PathMoveEndType {
    None,
    InCell,//走到格子里才算完
    Touch,//走到可以互动的位置才算完
}