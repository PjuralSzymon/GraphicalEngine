using GraphicalEngine.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEngine.Components.Meshes
{
    public class Line
    {
        //If we enable this variable we will same a lot of computation power but a lot of RAM will be used
        private bool StableCalculation = false;
        private List<Point> preCalcualtedPoints = null;
        private Point _a = new Point(0, 0,0);
        private Point _b = new Point(0, 0,0);

        public Point a
        {
            get { return _a; }
            set
            {
                if (value == _a) return;
                if (StableCalculation)
                    preCalcualtedPoints = calculatePixels();
                _a = value;
            }
        }
        public Point b
        {
            get { return _b; }
            set
            {
                if (value == _b) return;
                if (StableCalculation)
                    preCalcualtedPoints = calculatePixels();
                _b = value;
            }
        }

        public List<Point> getPixels()
        {
            if (StableCalculation)
                return preCalcualtedPoints;
            return calculatePixels();
        }

        private List<Point> calculatePixels()
        {
            List<Point> points = new List<Point>();

            (a, b) = Algorithms.Liang_Barsky_Alg.LiangBarsky(
                    a,
                    b,
                    ViewPort.width,
                    ViewPort.heigth);

            if (b.x < a.x)
            {
                Point tmp = a;
                a = b;
                b = tmp;
            }

                int deltaY = 1;
            if (b.y - a.y < 0)
                deltaY = -1;
            if (Math.Abs(b.y - a.y) - Math.Abs(b.x - a.x) < 0)
                points.AddRange(E_NE_alg(a, b, deltaY));
            else
                points.AddRange(N_NE_alg(a, b, deltaY));
            return points;
        }

        private List<Point> E_NE_alg(Point a, Point b, int deltaY)
        {
            List<Point> points = new List<Point>();
            int dx = b.x - a.x;
            int dy = deltaY * (b.y - a.y);
            int d = 2 * (dy - dx);
            int dE = 2 * dy;
            int dNE = 2 * (dy - dx);
            int xs = a.x, ys = a.y;
            int xf = b.x, yf = b.y;

            int two_v_dx = 0; //numerator, v=0 for the first pixel
            double invDenom = 1 / (2 * Math.Sqrt(dx * dx + dy * dy)); //inverted denominator
            double two_dy_invDenom = 2 * dx * invDenom; //precomputed constant

            points.Add(new Point(xs, ys, getZ(xs, ys, a, b)));
            points.Add(new Point(xf, yf, getZ(xf, yf, a, b)));
            while (xs < xf)
            {
                xs += 1;
                xf -= 1;
                if (d < 0)
                {
                    two_v_dx = d + dx;
                    d += dE;
                    //E is choosen
                }
                else
                {
                    //NE is choosen
                    two_v_dx = d - dx;
                    d += dNE;
                    ys += deltaY;
                    yf -= deltaY;
                }
                points.Add(new Point(xs, ys, getZ(xs, ys, a, b)));
                points.Add(new Point(xf, yf, getZ(xf, yf, a, b)));
            }
            return points;
        }
        private List<Point> N_NE_alg(Point a, Point b, int deltaY)
        {
            List<Point> points = new List<Point>();
            int dx = b.x - a.x;
            int dy = deltaY * (b.y - a.y);
            int d = 2 * (dx - dy);
            int dN = 2 * dx;
            int dNE = 2 * (dx - dy);
            int xs = a.x, ys = a.y;
            int xf = b.x, yf = b.y;

            int two_v_dy = 0; //numerator, v=0 for the first pixel
            double invDenom = 1 / (2 * Math.Sqrt(dx * dx + dy * dy)); //inverted denominator
            double two_dy_invDenom = 2 * dy * invDenom; //precomputed constant

            points.Add(new Point(xs, ys, getZ(xs, ys, a, b)));
            points.Add(new Point(xf, yf, getZ(xf, yf, a, b)));

            while (deltaY * (ys - yf) < 0)
            {
                ys += deltaY;
                yf -= deltaY;
                if (d < 0)
                {
                    two_v_dy = d + dy;
                    d += dN;
                }
                //N is choosen
                else
                {
                    //NE is choosen
                    two_v_dy = d - dy;
                    d += dNE;
                    xs += 1;
                    xf -= 1;
                }
                points.Add(new Point(xs, ys, getZ(xs, ys, a, b)));
                points.Add(new Point(xf, yf, getZ(xf, yf, a, b)));
            }

            return points;
        }

        float getZ(int newX, int newY, Point a, Point b)
        {
            if (a.y > b.y)
            {
                Point tmp = b;
                b = a;
                a = tmp;
            }
            float t = (float)(a.distanceTo(new Point(newX, newY,0)) / a.distanceTo(b));
            float zt = ((b.Z - a.Z) * t + a.Z);
            return zt;
        }

    }
}
