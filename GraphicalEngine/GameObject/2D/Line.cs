using System;
using GraphicalEngine.GameObject;
using GraphicalEngine.Engine;

namespace GraphicalEngine.GameObject
{

    public class Line : Object
    {
        bool initialized = false;
        public Point initP= new Point(0,0);
        public Point endiP = new Point(0,0);

        Color BKG_COLOR = new Color(255,255,255);
        Color LINE_COLOR = new Color(0, 0, 0);


        public Line()
        {

        }

        public Line(Point p1, Point p2)
        {
            setStartPos(p1);
            setEndingPos(p2);
        }
        public void setStartPos(Point _p)
        {
            if(!initialized)
            {
                initialized = true;
                initP = _p;
                endiP = _p;
            }
            initP = new Point(_p.x, _p.y);
        }
        public void setEndingPos(Point _p)
        {
            endiP = new Point(_p.x, _p.y);
        }

        public override void Draw(IntPtr BitMap, int width, int height)
        {
            Draw(BitMap, width, height, LINE_COLOR);
        }

        public override void Clear(IntPtr BitMap, int width, int height) 
        {
            Draw(BitMap, width, height, BKG_COLOR);
        }

        public void Draw(IntPtr BitMap, int width, int height, Color C)
        {
            Point a = initP;
            Point b = endiP;
            if (!PixelOperation.belongToRex(width, height, a.x, a.y) && !PixelOperation.belongToRex(width, height, b.x, b.y)) return;
            if(b.x<a.x)
            {
                Point tmp = a;
                a = b;
                b = tmp;
            }
            int deltaY = 1;
            if (b.y - a.y < 0) 
                deltaY = -1;
            if (Math.Abs(b.y - a.y) - Math.Abs(b.x - a.x) < 0) //dy-dx<0
                //ThickAntialiasedLine(a, b, deltaY, BitMap, width, height);
                E_NE_alg(a, b, deltaY, BitMap, width, height, C);
            else
                N_NE_alg(a, b, deltaY, BitMap, width, height, C);
        }

        private void E_NE_alg(Point a,Point b, int deltaY, IntPtr BitMap, int width, int height, Color C)
        {
            Console.WriteLine("E");
            int dx = b.x - a.x;
            int dy = deltaY*(b.y - a.y);
            int d = 2 * (dy - dx);
            int dE = 2 * dy;
            int dNE = 2 *(dy - dx);
            int xs = a.x, ys = a.y;
            int xf = b.x, yf = b.y;

            int two_v_dx = 0; //numerator, v=0 for the first pixel
            double invDenom = 1 / (2 * Math.Sqrt(dx * dx + dy * dy)); //inverted denominator
            double two_dy_invDenom = 2 * dx * invDenom; //precomputed constant

            PixelOperation.putPixel(BitMap, width, height, xs, ys, C);
            PixelOperation.putPixel(BitMap, width, height, xf, yf, C);

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
                PixelOperation.putPixel(BitMap, width, height, xs, ys, C);
                PixelOperation.putPixel(BitMap, width, height, xf, yf, C);

            }
        }
        private void N_NE_alg(Point a, Point b, int deltaY, IntPtr BitMap, int width, int height, Color C)
        {
            Console.WriteLine("N");
            int dx =b.x - a.x;
            int dy = deltaY*(b.y - a.y);
            int d = 2 * (dx - dy);
            int dN = 2 * dx;
            int dNE = 2 *(dx - dy);
            int xs = a.x, ys = a.y;
            int xf = b.x, yf = b.y;

            int two_v_dy = 0; //numerator, v=0 for the first pixel
            double invDenom = 1 / (2 * Math.Sqrt(dx * dx + dy * dy)); //inverted denominator
            double two_dy_invDenom = 2 * dy * invDenom; //precomputed constant

            PixelOperation.putPixel(BitMap, width, height, xs, ys,C);
            PixelOperation.putPixel(BitMap, width, height, xf, yf,C);

            while (deltaY*(ys-yf) <0)
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
                PixelOperation.putPixel(BitMap, width, height, xs, ys,C);
                PixelOperation.putPixel(BitMap, width, height, xf, yf,C);
            }
        }

        public void SetColor(Color c)
        {
            LINE_COLOR = c;
        }
    }
}
