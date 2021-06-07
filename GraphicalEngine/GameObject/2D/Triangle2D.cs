using GraphicalEngine.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEngine.GameObject
{
    class Triangle2D : Object
    {
        public Color FILLCOLOR = new Color(0,255,0);

        public Point a;
        public Point b;
        public Point c;

        private Line[] lines = new Line[3];

        public Triangle2D()
        {
            Init(new Point(10, 10), new Point(10, 20), new Point(20, 20));
        }

        public Triangle2D( Point _a, Point _b, Point _c)
        {
            Init(_a, _b, _c);
        }

        private void Init(Point _a, Point _b, Point _c)
        {
            a = _a;
            b = _b;
            c = _c;
            lines[0] = new Line();
            lines[0].setStartPos(a);
            lines[0].setEndingPos(b);
            lines[1] = new Line();
            lines[1].setStartPos(a);
            lines[1].setEndingPos(c);
            lines[2] = new Line();
            lines[2].setStartPos(b);
            lines[2].setEndingPos(c);
        }

        public override void Draw(IntPtr BitMap, int width, int height)
        {
            fillPolygon(getPolygons(), BitMap, width, height);
            foreach (Line  L in lines)
            {
                L.Draw(BitMap, width, height);
            }
        }


        // FILLING ALG:

        private Point[] getPolygons()
        {
            Point[] result = new Point[3];
            result[0] = a;
            result[1] = b;
            result[2] = c;
            return result;
        }

        private void fillPolygon(Point[] P, IntPtr BitMap, int width, int height)
        {
            int[] indices = getIndeces(P);
            int N = indices.Length;
            int k = 0;
            int i = indices[k];
            int y = P[indices[0]].y;
            int ymin = y;
            int ymax = P[indices[N - 1]].y;
            ActiveEdgeTable AET = new ActiveEdgeTable();
            while (y < ymax)
            {
                while (P[i].y == y)
                {
                    // remember to wrap indices in polygon
                    if (getP(P, i - 1).y > getP(P, i).y)
                        AET.Add(getP(P, i), getP(P, i - 1));
                    if (getP(P, i + 1).y > getP(P, i).y)
                        AET.Add(getP(P, i), getP(P, i + 1));
                    ++k;
                    i = indices[k];
                }
                //sort AET by x value
                AET.sort();
                //fill pixels between pairs of intersections
                foreach ((int x1, int x2) in AET.getScanLine())
                {
                    FillLine(x1, x2, y, BitMap, width, height);
                }
                ++y;
                //remove from AET edges for which ymax = y
                AET.Edges.RemoveAll(x => x.maxy <= y);
                //for each edge in AET
                //x += 1 / m
                foreach (AE edge in AET.Edges)
                {
                    //x value is a position in bitmap it means taht it's an integer, that's why incremental soultion from lecture is problematic deu to rounding
                    edge.x = edge.minx + (int)Math.Floor((float)(y - edge.miny) / edge.m);
                }
            }
        }

        private int[] getIndeces(Point[] P)
        {
            int[] indices = new int[P.Length];
            for (int i = 0; i < indices.Length; i++) indices[i] = -1;
            int currentIndices = 0;
            int currentYmin = int.MaxValue;
            while (currentIndices < indices.Length)
            {
                int IFound = -1;
                currentYmin = int.MaxValue;
                for (int i = 0; i < P.Length; i++)
                {
                    if (P[i].y < currentYmin && ExistsIn(indices, i) == false)
                    {
                        IFound = i;
                        currentYmin = P[i].y;
                    }
                }
                if (IFound != -1)
                {
                    indices[currentIndices] = IFound;
                    currentIndices++;
                }
                else
                {
                    throw new InvalidOperationException("ERROR while creating indices for filling polygon");
                }
            }
            return indices;
        }

        private void FillLine(int xFrom, int xTo, int y, IntPtr BitMap, int width, int height)
        {
            Line TmpLine = new Line();
            TmpLine.SetColor(FILLCOLOR);
            TmpLine.setStartPos(new Point(xFrom, y));
            TmpLine.setEndingPos(new Point(xTo, y));
            TmpLine.Draw(BitMap, width, height);
        }

        private bool ExistsIn(int[] table, int x)
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] == x) return true;
            }
            return false;
        }
        private Point getP(Point[] P, int i)
        {
            if (i < 0)
                i = P.Length + i;
            if (i >= P.Length)
                i = i - P.Length;
            return P[i];
        }


    }

    public class ActiveEdgeTable
    {
        public List<AE> Edges = new List<AE>();
        public void Add(Point a, Point b)
        {
            Edges.Add(new AE(a, b));
        }

        public void sort()
        {
            Edges.Sort();
        }

        public List<(int, int)> getScanLine()
        {
            List<(int, int)> result = new List<(int, int)>();
            for (int i = 0; i < Edges.Count - 1; i = i + 2)
            {
                result.Add((Edges[i].x, Edges[i + 1].x));
            }
            return result;
        }

    }
    public class AE : IComparable
    {
        //Point a;
        //Point b;
        public int miny;
        public int maxy;
        public float m;
        public int minx;
        public int x;

        public AE(Point a, Point b)
        {

            miny = a.y;
            maxy = b.y;
            minx = a.x;
            m = ((float)a.y - (float)b.y) / ((float)a.x - (float)b.x);

            x = minx;
        }

        public bool isOutside(int newY)
        {
            return true;
        }
        public int CompareTo(object obj)
        {
            return CompareTo((AE)obj);
        }
        public int CompareTo(AE obj)
        {
            if (obj.x > x) return -1;
            else if (obj.x < x) return 1;
            else return 0;
        }
    }
}
