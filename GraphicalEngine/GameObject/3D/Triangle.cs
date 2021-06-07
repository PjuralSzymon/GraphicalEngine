using GraphicalEngine.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEngine.GameObject
{
    public static class Triangle
    {
        public static List<Vector4> getPixels(Vector4 a, Vector4 b, Vector4 c, float delta)
        {
            List<Vector4> toDraw = new List<Vector4>();
            toDraw.AddRange(Line3D.getPixels(a, b, delta));
            toDraw.AddRange(Line3D.getPixels(a, c, delta));
            toDraw.AddRange(Line3D.getPixels(c, b, delta));
            toDraw.AddRange(Fill(a,b,c,delta));
            return toDraw;
        }

        public static List<Vector4> Fill(Vector4 A, Vector4 B, Vector4 C, float delta)
        {
            List<Vector4> toDraw = new List<Vector4>();

            toDraw.Add(A);
            Vector4 A1 = A;
            Vector4 A2 = A;
            Vector4 D1 = divide(substract(B, A1), delta);
            Vector4 D2 = divide(substract(C, A2), delta);
            float boundry = 2 / delta;
            while (getDistance(A1, B) > boundry && getDistance(A2, C) > boundry)
            {
                    A1 = add(A1, D1);
                    A2 = add(A2, D2);
                toDraw.AddRange(Line3D.getPixels(A1, A2, delta));
            }
            return toDraw;
        }


        private static float getDistance(Vector4 a, Vector4 b)
        {
            return (float)Math.Sqrt(Math.Pow(b.X - a.X, 2) +
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
            a.X = a.X / b;
            a.Y = a.Y / b;
            a.Z = a.Z / b;
            return a;
        }
    }
}
