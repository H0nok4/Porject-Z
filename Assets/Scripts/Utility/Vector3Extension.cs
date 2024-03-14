
using System;
using UnityEngine;

public static class Vector3Extension {

    public static IntVec2 ToCellPos(this Vector3 vector)
    {
        return new IntVec2((int)Math.Floor(vector.x), (int)Mathf.Floor(vector.y));
    }
}