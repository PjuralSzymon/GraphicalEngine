using GraphicalEngine.Components.Meshes;
using GraphicalEngine.GameObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalEngine.Engine
{
    public static class PixelOperation
    {
        public static IntPtr GetPos(IntPtr p, int width, int x, int y) { return p + 4 * (x + width * y); }
        public static void putPixel(IntPtr p, int width,int height, int x, int y, Color c)
        {
            if (belongToRex(width, height, x, y) == false) return;
            unsafe
            {
                /*B*/*(byte*)(GetPos(p, width, x, y)+0) = c.B;
                /*G*/*(byte*)(GetPos(p, width, x, y)+1) = c.G;
                /*R*/*(byte*)(GetPos(p, width, x, y)+2) = c.R;
            }
        }

        public static Color getPixelColor(IntPtr p, int width, int height, int x, int y)
        {
            Color Fc = new Color(0, 0, 0);
            if (belongToRex(width, height, x, y) == false)
            {
                return Fc;
            }
            unsafe
            {
                /*B*/
                Fc.B = *(byte*)(GetPos(p, width, x, y) + 0);
                /*G*/
                Fc.G = *(byte*)(GetPos(p, width, x, y) + 1);
                /*R*/
                Fc.R = *(byte*)(GetPos(p, width, x, y) + 2);
            }
            return Fc;
        }
        public static void putPixel(IntPtr p, int width,int height, Point point, Color c)
        {
            putPixel(p, width, height, point.x, point.y, c);
        }

        public static bool belongToRex(int w,int h, int x, int y)
        {
            int delta = 5;
            if (x <= delta || x >= w- delta) return false;
            if (y <= delta || y >= h- delta) return false;
            return true;
        }
    }
}
