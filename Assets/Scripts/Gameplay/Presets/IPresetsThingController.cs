using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IPresetsThingController {
    public PresetType Type { get; }

    public void Init();

    public void Register(PresetsThing thing);

    public void UnRegister(PresetsThing thing);
}