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
            return Define.RecoverFood;
        }
    }

    public int RecoverThirsty {
        get {
            return Define.RecoverThirsty;
        }
    }

    public int RecoverJoy {
        get {
            return Define.RecoverJoy;
        }
    }

    public IngestibleEffect(int id) {
        ID = id;
    }

}