using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public struct IntVec2
{
    public int X;
    public int Y;

    public static readonly IntVec2 Zero = new IntVec2(0, 0);

    public static readonly IntVec2 One = new IntVec2(1, 1);

    public static readonly IntVec2 Two = new IntVec2(2, 2);

    public static readonly IntVec2 North = new IntVec2(0, 1);

    public static readonly IntVec2 East = new IntVec2(1, 0);

    public static readonly IntVec2 South = new IntVec2(0, -1);

    public static readonly IntVec2 West = new IntVec2(-1, 0);

    public static readonly IntVec2 Invalid = new IntVec2(int.MinValue, int.MinValue);

    public static bool operator ==(IntVec2 a, IntVec2 b) {
        if (a.X == b.X && a.Y == b.Y) {
            return true;
        }

        return false;
    }

    public static IntVec2 operator +(IntVec2 a, IntVec2 b)
    {
        return new IntVec2(a.X + b.X, a.Y + b.Y);
    }

    public static IntVec2 operator -(IntVec2 a, IntVec2 b)
    {
        return new IntVec2(a.X + b.X, a.Y + b.Y);
    }

    public static bool operator !=(IntVec2 a, IntVec2 b) {
        if (a.X != b.X || a.Y != b.Y) {
            return true;
        }

        return false;
    }

    public IntVec2(int x, int y)
    {
        X = x;
        Y = y;
    }

    public IntVec2 Copy()
    {
        return new IntVec2(this.X,this.Y);
    }

    public Vector3 ToVector3()
    {
        return new Vector3(X, Y);
    }

    public override string ToString()
    {
        return $"(X = {X},Y = {Y})";
    }
}