using System;

[Serializable]
public class EditableType {
    public string TypeName;

    public Type ToType() {
        return Type.GetType(TypeName);
    }
}