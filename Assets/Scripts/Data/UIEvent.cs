using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class UIEvent {
    public string EventID { get; private set; }

    public object[] EventParam { get; private set; }

    public UIEvent(string eventID,params object[] param)
    {
        EventID = eventID;
        EventParam = param;
    }
}