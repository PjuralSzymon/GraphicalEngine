using GraphicalEngine.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEngine.Components.Meshes
{
    public class Point
    {
        public int x = 0;
        public int y = 0;
        public float Z = 0;

        public Point()
        {
            x = 0;
            y = 0;
        }

        public Point(int _x, int _y, float _Z)
        {
            rePos(_x, _y, _Z);
        }

        public void rePos(int _x, int _y, float _Z)
        {
            x = _x;
            y = _y;
            Z = _Z;
        }

        public void rePos(Point P)
        {
            x = P.x;
            y = P.y;
            Z = P.Z;
        }

        public double distanceTo(Point p)
        {
            return Math.Sqrt((x - p.x) * (x - p.x) + (y - p.y) * (y - p.y));
        }

        public double distance3To(Point p)
        {
            return Math.Sqrt((x - p.x) * (x - p.x) + (y - p.y) * (y - p.y) + (Z - p.Z) * (Z - p.Z));
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y, a.Z + b.Z);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.x - b.x, a.y - b.y, a.Z- b.Z);
        }

        public static Point operator /(Point a, int b)
        {
            return new Point(a.x / b, a.y / b, a.Z/(float)b);
        }

        public void Clear()
        {
            x = 0;
            y = 0;
        }
        public override string ToString()
        {
            return "[" + x.ToString() + ":" + y.ToString() + "]";
        }

    }
}
