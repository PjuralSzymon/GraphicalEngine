using GraphicalEngine.GameObject;
using GraphicalEngine.Scenes;
using System;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Object = GraphicalEngine.GameObject.Object;
using Point = GraphicalEngine.GameObject.Point;
using Transform = GraphicalEngine.Components.Transform;

namespace GraphicalEngine.Engine
{
    public class ViewPort
    {
        public bool DrawingFinished = true;
        public bool loaded = false;
        public Transform transform = new Transform();
        public Color BackgroundColor = new Color(255, 255, 255);
        public float maxDistance = 2000;

        private byte[,] ZValues;
        private Image ImageHolder;
        private int width;
        private int heigth;
        
        public ViewPort(Image _ImageHolder)
        {
            ImageHolder = _ImageHolder;
            transform.rotation.X = 0;
            transform.position.Z = -50;
        }
        private void Clear(WriteableBitmap Wbitmap)
        {
            if (Wbitmap == null) return;
            unsafe
            {
                Wbitmap.Lock();
                IntPtr pBackBuffer = Wbitmap.BackBuffer;
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < heigth; y++)
                    {
                        /*B*/
                        *(byte*)(pBackBuffer) = 255;
                        pBackBuffer += 1;
                        /*G*/
                        *(byte*)(pBackBuffer) = 255;
                        pBackBuffer += 1;
                        /*R*/
                        *(byte*)(pBackBuffer) = 255;
                        pBackBuffer += 2;
                    }
                }
                Wbitmap.AddDirtyRect(new Int32Rect(0, 0, width, heigth));
                Wbitmap.Unlock();
            }
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
            Clear((WriteableBitmap)ImageHolder.Source);
            ZValues = new byte[width, heigth];
            loaded = true;
        }
        public void DrawScene_oldV(GameScene scene)
        {
            DrawingFinished = false;
            //Clear((WriteableBitmap)ImageHolder.Source);
            ((WriteableBitmap)ImageHolder.Source).Lock();
            IntPtr Buff = ((WriteableBitmap)ImageHolder.Source).BackBuffer;
            Clear(((WriteableBitmap)ImageHolder.Source));
            //scene.ClearAll(Buff, width, heigth);
            scene.DrawAll(Buff, width, heigth);
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
                foreach(Vector4 P in gameObject.getPixels())
                {
                        Point p = gameObject.transform.translate3Dto2D(P, width, heigth, this.transform);
                        float zVal = gameObject.transform.getZValue(P, width, heigth, this.transform);
                        //if (canSetZval(p.x,p.y, zVal))
                        //{
                            PixelOperation.putPixel(((WriteableBitmap)ImageHolder.Source).BackBuffer, width, heigth, p.x, p.y, gameObject.color);
                            setZval(p.x, p.y, zVal);
                        //}
                }
            }
            ((WriteableBitmap)ImageHolder.Source).AddDirtyRect(new Int32Rect(0, 0, width, heigth));
            ((WriteableBitmap)ImageHolder.Source).Unlock();
            DrawingFinished = true;
        }

        private void setZval(int x, int y, float z)
        {
            byte interpolatedVal = (byte)(255*z / maxDistance);
            if (interpolatedVal > 255)
            {
                interpolatedVal = 255;
            }
            else if (interpolatedVal < 0) interpolatedVal = 0;
            if (PixelOperation.belongToRex(width,heigth,x,y))
             ZValues[x, y] = interpolatedVal;
        }
        private bool canSetZval(int x, int y, float z)
        {
            
            byte interpolatedVal = (byte)(255 * z / maxDistance);
            if (interpolatedVal > 255)
            {
                interpolatedVal = 255;
            }
            else if (interpolatedVal < 0) interpolatedVal = 0;
            return interpolatedVal<ZValues[x, y];
        }
    }
}
