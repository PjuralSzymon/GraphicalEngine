using System;
using System.Collections.Generic;
using System.Numerics;

namespace GraphicalEngine.Components.Meshes
{
    public class Triangle
    {

        public Vector4 A;
        public Vector4 B;
        public Vector4 C;

        private Line[] lines = new Line[3];

        //For precaluclation:
        private Vector4 TA;
        private Vector4 TB;
        private Vector4 TC;
        private Point a;
        private Point b;
        private Point c;

        public Triangle()
        {
            lines = new Line[3];
            lines[0] = new Line();
            lines[1] = new Line();
            lines[2] = new Line();
        }
        public void preCalculate(Transform parent, Transform camera, int width, int heigth)
        {
            TA = parent.translate(A);
            TB = parent.translate(B);
            TC = parent.translate(C);
            a = parent.translate3Dto2D(TA, width, heigth, camera);
            b = parent.translate3Dto2D(TB, width, heigth, camera);
            c = parent.translate3Dto2D(TC, width, heigth, camera);
        }
        public List<Point> getPixels() 
        {
            List<Point> allPoints = new List<Point>();
            lines[0].a = a;
            lines[0].b = b;
            allPoints.AddRange(lines[0].getPixels());
            lines[1].a = c;
            lines[1].b = a;
            allPoints.AddRange(lines[1].getPixels());
            lines[2].a = b;
            lines[2].b = c;
            allPoints.AddRange(lines[2].getPixels());

            Point[] tmp = new Point[3];
            tmp[0] = a; tmp[1] = b; tmp[2] = c;
            allPoints.AddRange(FillAlg.fillPolygon(tmp));
            return allPoints; 
        }
        public float getZval(Point p, Transform camera)
        { 
            return getDistance(p, camera); 
        }

        public float getDistance(Point p,Transform Object)
        {
            float d1 = (float)p.distanceTo(a);
            float d2 = (float)p.distanceTo(b);
            float d3 = (float)p.distanceTo(c);
            double sum = d1 + d2 + d3;
            Vector3 point = new Vector3((float)((d1 * TA.X + d2 * TB.X + d3 * TC.X) / sum),
                                        (float)((d1 * TA.Y + d2 * TB.Y + d3 * TC.Y) / sum),
                                        (float)((d1 * TA.Z + d2 * TB.Z + d3 * TC.Z) / sum));
            return (float)Math.Sqrt(Math.Pow((Object.position.X - point.X), 2)+
                                    Math.Pow((Object.position.Y - point.Y), 2)+
                                    Math.Pow((Object.position.Z - point.Z), 2));
        }

    }

    public static class FillAlg
    {
        public static List<Point> fillPolygon(Point[] P)
        {
            List<Point> Points = new List<Point>();
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
                    Points.AddRange(FillLine(x1, x2, y));
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
            return Points;
        }

        private static List<Point> FillLine(int xFrom, int xTo, int y)
        {
            Line TmpLine = new Line();
            TmpLine.a = (new Point(xFrom, y));
            TmpLine.b = (new Point(xTo, y));
            return TmpLine.getPixels();
            //TmpLine.Draw(BitMap, width, height, GlobalMode.Default);
        }

        private static int[] getIndeces(Point[] P)
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

        private static bool ExistsIn(int[] table, int x)
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] == x) return true;
            }
            return false;
        }
        private static Point getP(Point[] P, int i)
        {
            if (i < 0)
                i = P.Length + i;
            if (i >= P.Length)
                i = i - P.Length;
            return P[i];
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

}
