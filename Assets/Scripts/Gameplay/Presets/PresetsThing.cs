using UnityEngine;

public abstract class PresetsThing : MonoBehaviour {
    public abstract PresetType Type { get; }

    public abstract void OnRegister();

    public abstract void OnUnRegister();
}