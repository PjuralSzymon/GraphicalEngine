using GraphicalEngine.Components.Meshes;
using GraphicalEngine.GameObject;
using GraphicalEngine.Scenes;
using System;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Object = GraphicalEngine.GameObject.Object;
using Point = GraphicalEngine.Components.Meshes.Point;
using Transform = GraphicalEngine.Components.Transform;

namespace GraphicalEngine.Engine
{
    public class ViewPort
    {
        public bool DrawingFinished = true;
        public bool loaded = false;
        public Transform transform = new Transform();
        public Color BackgroundColor = new Color(0, 0, 0);
        public static int width;
        public static int heigth;

        private float maxDistance = 1000;
        private float[,] ZValues;
        private Image ImageHolder;
        
        public ViewPort(Image _ImageHolder)
        {
            ImageHolder = _ImageHolder;
            transform.rotation.X = 0;
            transform.position.Z = -200;
        }

        public void ReSize(int newWidth, int newHeigth)
        {
            width = newWidth;
            heigth = newHeigth;
            ImageHolder.Source = new WriteableBitmap(
                width,
                heigth,
                96,
                96,
                PixelFormats.Bgr32,
                null);
            ZValues = new float[width, heigth];
            loaded = true;
        }
        public void DrawScene_oldV(GameScene scene)
        {
            DrawingFinished = false;
            //Clear((WriteableBitmap)ImageHolder.Source);
            ((WriteableBitmap)ImageHolder.Source).Lock();
            IntPtr Buff = ((WriteableBitmap)ImageHolder.Source).BackBuffer;
            //Clear(((WriteableBitmap)ImageHolder.Source));
            //scene.ClearAll(Buff, width, heigth);
            //scene.DrawAll(Buff, width, heigth);
            ((WriteableBitmap)ImageHolder.Source).AddDirtyRect(new Int32Rect(0, 0, width, heigth));
            ((WriteableBitmap)ImageHolder.Source).Unlock();
            DrawingFinished = true;
        }

        public void DrawScene(GameScene scene)
        {
            DrawingFinished = false;
            ((WriteableBitmap)ImageHolder.Source).Lock();
            for (int x=0;x<width;x++)
                for(int y=0;y<heigth;y++)
                {
                    PixelOperation.putPixel(((WriteableBitmap)ImageHolder.Source).BackBuffer, width, heigth, x, y, BackgroundColor);
                    setZval(x, y, maxDistance);
                }

            foreach(Object gameObject in scene.gameObjects)
            {
              //  if(!gameObject.transform.ObjectIsIn(this.transform))
                    foreach(Triangle T in gameObject.mesh.triangles)
                    {
                        T.preCalculate(gameObject.transform, this.transform, width, heigth);
                        foreach (Point p in T.getPixels())
                        {
                            if (belongToRect(p))
                            {
                            //float zVal = T.getZval(p, this.transform)
                            if (canSetZval(p.x, p.y, p.Z))
                                {
                                    PixelOperation.putPixel(((WriteableBitmap)ImageHolder.Source).BackBuffer, width, heigth, p.x, p.y, 
                                        scene.LightSource.getColor(gameObject.color,T.getDistance(p, scene.LightSource.transform)));
                                    setZval(p.x, p.y, p.Z);
                                }
                            }
                        }
                    }
            }
            ((WriteableBitmap)ImageHolder.Source).AddDirtyRect(new Int32Rect(0, 0, width, heigth));
            ((WriteableBitmap)ImageHolder.Source).Unlock();
            DrawingFinished = true;
        }

        private void setZval(int x, int y, float z)
        {
             ZValues[x, y] = z;
        }
        private bool canSetZval(int x, int y, float z)
        {
            return z<ZValues[x, y];
        }

        public static bool belongToRect(Point p)
        {
            return !(p.x <= 0 || p.y<= 0 || p.x >= width || p.y >= heigth);
        }
    }
}
