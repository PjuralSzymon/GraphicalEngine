using GraphicalEngine.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalEngine.GameObject
{
    public class Point
    {
        public int x = 0;
        public int y = 0;

        public Point()
        {
            x = 0;
            y = 0;
        }
        public Point(int _x=0,int _y=0)
        {
            rePos(_x, _y);
        }

        public void rePos(int _x = 0, int _y = 0)
        {
            x = _x;
            y = _y;
        }

        public void rePos(Point P)
        {
            x = P.x;
            y = P.y;
        }

        public double distanceTo(Point p)
        {
            return Math.Sqrt((x - p.x)*(x - p.x) + (y - p.y)*(y - p.y));
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.x - b.x, a.y - b.y);
        }

        public static Point operator /(Point a, int b)
        {
            return new Point(a.x/b, a.y/b);
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

        public static void DrawPoint(IntPtr BitMap, int width, int height, Point p, Color c = null)
        {
            c = new Color(0, 0, 0);
            int size = 10;
            for(int x=-size/2;x<size/2;x++)
            {
                for(int y=-size/2;y<size/2;y++)
                {
                    PixelOperation.putPixel(BitMap, width, height, p + new Point(x,y), c);
                }
            }
        }
    }
}
