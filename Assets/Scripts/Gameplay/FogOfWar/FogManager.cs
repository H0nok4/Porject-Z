using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class FogManager : Singleton<FogManager>
{

    public RenderTexture[] FogTexture;
    public bool[][] VisibleFog;

    public void Init(int mapLayerCount)
    {

    }
}

public static class CircleUtility
{
    // Bresenham画圆算法
    public static List<IntVec2> GetBresenhamCircle(IntVec2 center, int radius) {
        HashSet<IntVec2> coordinates = new HashSet<IntVec2>();

        int x = 0;
        int y = radius;
        int decisionOver2 = 1 - radius;

        while (x <= y) {
            
            AddOctantPoints(coordinates, center, x, y);

            x++;
            if (decisionOver2 <= 0) {
                decisionOver2 += 2 * x + 3;
            }
            else {
                decisionOver2 += 2 * (x - y) + 5;
                y--;
            }
        }

        //因为画八分之一圆的时候,俩边的端点会重复计算,所以用HashMap移除掉重复的点,这里再转成列表
        List<IntVec2> result = new List<IntVec2>();
        foreach (var coordinate in coordinates)
        {
            result.Add(coordinate);
        }

        return result;
    }

    private static void AddOctantPoints(HashSet<IntVec2> coordinates, IntVec2 center, int x, int y) {
        coordinates.Add(new IntVec2(center.X + x, center.Y + y));
        coordinates.Add(new IntVec2(center.X - x, center.Y + y));
        coordinates.Add(new IntVec2(center.X + x, center.Y - y));
        coordinates.Add(new IntVec2(center.X - x, center.Y - y));
        coordinates.Add(new IntVec2(center.X + y, center.Y + x));
        coordinates.Add(new IntVec2(center.X - y, center.Y + x));
        coordinates.Add(new IntVec2(center.X + y, center.Y - x));
        coordinates.Add(new IntVec2(center.X - y, center.Y - x));
    }
}
