using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ConfigType
{
    public partial class WorkGiverDefine
    {
        private WorkGiver _workGiver;

        public WorkGiver WorkGiver {
            get {
                if (_workGiver == null) {
                    _workGiver = (WorkGiver)Activator.CreateInstance(WorkGiverType.ToType());
                    _workGiver.Def = this;
                }

                return _workGiver;
            }
        }
    }
}
