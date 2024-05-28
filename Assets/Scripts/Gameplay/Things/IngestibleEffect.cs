using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigType;

public  class IngestibleEffect {

    public readonly int ID;

    private IngestibleDefine _def;

    public IngestibleDefine Define {
        get {
            if (_def == null) {
                _def = DataManager.Instance.GetIngestibleDefineByID(ID);
            }

            return _def;
        }
    }

    public int RecoverHungry {
        get {
            return _def.RecoverFood;
        }
    }

    public int RecoverThirsty {
        get {
            return _def.RecoverThirsty;
        }
    }

    public int RecoverJoy {
        get {
            return _def.RecoverJoy;
        }
    }

    public IngestibleEffect(int id) {
        ID = id;
    }

}