using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Gameplay {
    public static class DebugDrawer {
        public static void DrawBox(IntVec2 pos)
        {
            float size = 0.5f;
            //以传入的点为中心点，画出一个周长为1的正方形
            Debug.DrawLine(new Vector3(pos.X - size,pos.Y + size),new Vector3(pos.X + size,pos.Y + size),Color.red,5f);
            Debug.DrawLine(new Vector3(pos.X + size, pos.Y + size), new Vector3(pos.X + size, pos.Y - size), Color.red, 5f);
            Debug.DrawLine(new Vector3(pos.X + size, pos.Y - size), new Vector3(pos.X - size, pos.Y - size), Color.red, 5f);
            Debug.DrawLine(new Vector3(pos.X - size, pos.Y - size), new Vector3(pos.X - size, pos.Y + size), Color.red, 5f);
        }
    }
}
