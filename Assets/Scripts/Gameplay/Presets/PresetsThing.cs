using UnityEngine;

public abstract class PresetsThing : MonoBehaviour {
    public abstract PresetType Type { get; }

    public void Start() {
        PresetsThingManager.Instance.Register(this);
    }

    public abstract void OnRegister();
}