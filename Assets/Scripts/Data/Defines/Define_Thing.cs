using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Define_Thing : Define_Buildable
{
    public ThingCategory Category;

    public bool Destroyable = true;

    public bool Rotatable = true;

    public bool UseHitPoint;

    public int StackLimit = 1;

    public Sprite ThingSprite;

    public Sprite FrameSprite;


}