using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EditableType {
    public string TypeName;

    public Type ToType() {
        return Type.GetType(TypeName);
    }
}
