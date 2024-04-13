using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairyGUI
{
    public class UIEventRegAttribute : Attribute
    {
        public HashSet<string> regEventNames;

        public UIEventRegAttribute(params string[] uiEventNames)
        {
            regEventNames = new HashSet<string>();
            foreach (var uiEventName in uiEventNames)
            {
                regEventNames.Add(uiEventName);
            }
        }
    }
}