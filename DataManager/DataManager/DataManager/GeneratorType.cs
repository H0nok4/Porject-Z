using System;

[Serializable]
public class EditableType {
    public string TypeName;

    private Type _typeInstance;

    public Type ToType() {
        if (_typeInstance == null)
        {
            _typeInstance = Type.GetType(TypeName);
        }

        return _typeInstance;
    }
}