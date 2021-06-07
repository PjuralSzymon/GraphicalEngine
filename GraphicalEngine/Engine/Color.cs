using System;
using System.Collections.Generic;
using System.Text;

namespace GraphicalEngine.Engine
{
    public class Color
    {
        public byte R = 0;
        public byte G = 0;
        public byte B = 0;

        public Color()
        {
            R = 0;
            G = 0;
            B = 0;
        }

        public Color(byte _R, byte _G, byte _B)
        {
            R = _R;
            G = _G;
            B = _B;
        }
        public static bool operator ==(Color a, Color b)
        {
            if (a == null || b == null) return false;
            if (a.R != b.R) return false;
            if (a.G != b.G) return false;
            if (a.B != b.B) return false;
            return true;
        }

        public static bool operator !=(Color a, Color b)
        {
            return !(a==b);
        }
        public static Color lerp(Color a, Color b, double s)
        {
            return new Color(
                (byte)((1 - s) * (double)a.R + s * (double)b.R),
                (byte)((1 - s) * (double)a.G + s * (double)b.G),
                (byte)((1 - s) * (double)a.B + s * (double)b.B));
        }
    }
}
