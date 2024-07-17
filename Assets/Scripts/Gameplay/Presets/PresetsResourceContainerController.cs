
public class PresetsResourceContainerController : IPresetsThingController {
    public PresetType Type => PresetType.GenResourceContainer;


    public void Init() {

    }

    public void Register(PresetsThing thing) {
        thing.OnRegister();
    }

    public void UnRegister(PresetsThing thing) {
        thing.OnUnRegister();
    }
}
