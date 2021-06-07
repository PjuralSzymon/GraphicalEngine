using GraphicalEngine.Engine;
using GraphicalEngine.GameObject;
using System;
using System.Collections.Generic;

namespace GraphicalEngine.Algorithms
{
    public static class _4FillAlgorithm
    {
        public static Color ColorToFill = new Color(255,0,0);
        public static void Fill(IntPtr BitMap, int width, int height, Point p)
        {
            bool[,] CheckMap = new bool[width, height];
            Color SearchingColor = PixelOperation.getPixelColor(BitMap, width, height, p.x, p.y);
            List<Point> PixelList = new List<Point>();
            PixelList.Add(p);

            Queue<Point> PointQueue = new Queue<Point>();
            PointQueue.Enqueue(p);
            while (PointQueue.Count>0)
            {
                Point point = PointQueue.Dequeue();
                PixelOperation.putPixel(BitMap, width, height, point.x, point.y, ColorToFill);
                if (SearchingColor == PixelOperation.getPixelColor(BitMap, width, height, point.x + 1, point.y) && CheckMap[point.x + 1, point.y] == false)
                {
                    CheckMap[point.x + 1, point.y] = true;
                    PointQueue.Enqueue(new Point(point.x + 1, point.y));
                }
                if (SearchingColor == PixelOperation.getPixelColor(BitMap, width, height, point.x - 1, point.y) && CheckMap[point.x - 1, point.y] == false)
                {
                    CheckMap[point.x - 1, point.y] = true;
                    PointQueue.Enqueue(new Point(point.x - 1, point.y));
                }
                if (SearchingColor == PixelOperation.getPixelColor(BitMap, width, height, point.x, point.y + 1) && CheckMap[point.x, point.y + 1] == false)
                {
                    CheckMap[point.x, point.y + 1] = true;
                    PointQueue.Enqueue(new Point(point.x, point.y + 1));
                }
                if (SearchingColor == PixelOperation.getPixelColor(BitMap, width, height, point.x, point.y - 1) && CheckMap[point.x, point.y - 1] == false)
                {
                    CheckMap[point.x, point.y - 1] = true;
                    PointQueue.Enqueue(new Point(point.x, point.y - 1));
                }
            }
        }
    }
}
