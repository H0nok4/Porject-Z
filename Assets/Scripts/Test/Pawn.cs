using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour,IThing
{
    public List<IntVec2> Path;

    private IntVec2 _position = IntVec2.Invalid;

    public IntVec2 Position
    {
        get
        {
            return _position;
        }
        set
        {
            if (value == _position)
            {
                return;
            }

            _position.X = value.X;
            _position.Y = value.Y;
        }
    }


    public void Tick()
    {
        //TODO:查看是否当前有寻路的
        
    }

    public Pawn(List<IntVec2> path, IntVec2 position, bool isDestoryed, ThingType thingType) {
        Path = path;
        Position = position;
        IsDestoryed = isDestoryed;
        ThingType = thingType;
    }

    public bool IsDestoryed { get; set; }
    public ThingType ThingType { get; set; }
}
