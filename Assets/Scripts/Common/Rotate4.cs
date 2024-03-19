using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 环世界里面有4种朝向，矮人要塞直接没有朝向，不过因为需要射击，所以最好还是用4种朝向的设定（或者8种，但是相对应就需要加很多贴图，没必要，左右可以镜像复用，加上下可以三个贴图对应一个单位，
/// </summary>
public struct Rotator : IEquatable<Rotator>
{
    private byte _rotatorValue;

    public byte AsByte
    {
        get
        {
            return _rotatorValue;
        }
        set
        {
            _rotatorValue = (byte)MathUtility.PositiveMod(value, 4);
        }
    }

    public int AsInt
    {
        get
        {
            return (int) _rotatorValue;
        }
        set
        {
            _rotatorValue = (byte)MathUtility.PositiveMod(value, 4);
        }
    }

    public float AsAngle
    {
        get
        {
            switch (_rotatorValue)
            {
                case 0: return 0f;
                case 1: return 90f;
                case 2: return 180;
                case 3: return 270;
                default:
                    return 0;
            }
        }
    }

    public SpriteFaceSide AsSrpiteFaceSide
    {
        get
        {
            switch (_rotatorValue)
            {
                case 0: return SpriteFaceSide.Up;
                case 1: return SpriteFaceSide.Right;
                case 2: return SpriteFaceSide.Down;
                case 3: return SpriteFaceSide.Left;
                default:
                    return SpriteFaceSide.Up;
            }
        }
    }

    public Rotator(int value)
    {
        _rotatorValue = (byte)MathUtility.PositiveMod(value, 4);
    }

    public Rotator(byte value)
    {
        _rotatorValue = (byte)MathUtility.PositiveMod(value, 4);
    }

    public static Rotator Random => new Rotator(UnityEngine.Random.Range(0, 3));

    public void Rotate(RotationDirection dir)
    {
        AsByte += (byte)dir;
    }

    public IntVec2 FacingPosition
    {
        get
        {
            switch (_rotatorValue)
            {
                case 0: return new IntVec2(0, 1);//朝上看
                case 1: return new IntVec2(1, 0);
                case 2: return new IntVec2(0, -1);//朝下看
                case 3:return new IntVec2(-1, 0);
                default:
                    return new IntVec2(0, 0);
            }
        }
    }

    public bool Equals(Rotator other)
    {
        return _rotatorValue == other._rotatorValue;
    }

    public override bool Equals(object obj)
    {
        return obj is Rotator other && Equals(other);
    }

    public override int GetHashCode()
    {
        return _rotatorValue.GetHashCode();
    }
}