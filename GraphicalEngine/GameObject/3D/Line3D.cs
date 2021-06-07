using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEngine.GameObject
{
    public static class Line3D
    {
        public static List<Vector4> getPixels(Vector4 A, Vector4 B, float delta)
        {
            List<Vector4> toDraw = new List<Vector4>();

            toDraw.Add(A);
            Vector4 D = divide(substract(B, A),delta);
            while (getDistance(A,B)>2/delta)
            {
                A = add(A, D);
                toDraw.Add(A);
            }
            return toDraw;
        }
        private static float getDistance(Vector4 a, Vector4 b)
        {
            return (float)Math.Sqrt(Math.Pow(b.X-a.X,2) + 
                                    Math.Pow(b.Y - a.Y, 2) + 
                                    Math.Pow(b.Z - a.Z, 2));
        }
        private static Vector4 substract(Vector4 a, Vector4 b)
        {
            a.X = a.X - b.X;
            a.Y = a.Y - b.Y;
            a.Z = a.Z - b.Z;
            return a;
        }

        private static Vector4 add(Vector4 a, Vector4 b)
        {
            a.X = a.X + b.X;
            a.Y = a.Y + b.Y;
            a.Z = a.Z + b.Z;
            return a;
        }

        private static Vector4 divide(Vector4 a, float b)
        {
            a.X = a.X/b;
            a.Y = a.Y/b;
            a.Z = a.Z/b;
            return a;
        }
    }
}
