using GraphicalEngine.Scenes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;


namespace GraphicalEngine.Engine
{

    public class GameEngine
    {
        ViewPort MainViewPort;
        GameScene CurrentScene;
        public int FixedDeltaTime=50;
        public int FPS = 0;

        Stopwatch stopwatch = new Stopwatch();
        private int deltaTime = 0;
        private int frameCounter = 0;
        private int timeCounter = 0;
        private Func<String, String> infoFunc;
        public GameEngine(Image _ImageHolder,Func<String,String> _infoFunc)
        {
            //Here Scene can be change
            CurrentScene = new Campfire();

            MainViewPort = new ViewPort(_ImageHolder);
            infoFunc = _infoFunc;
            stopwatch.Start();
            CurrentScene.Start();
        }
        float cirlce = 0;
        float R = 300;
        Vector2 MiddlePoint = new Vector2(0, 0);
        public void Update(Vector2 MoveInput, Vector2 MouseInput)
        {
            if (MainViewPort.DrawingFinished)
            {
                float RdeltaTime = ((float)deltaTime) / 1000;
                MainViewPort.transform.position = new Vector3(MiddlePoint.X + (float)(R * Math.Sin(cirlce)), -R, MiddlePoint.Y + (float)(-R * Math.Cos(cirlce)));
                MainViewPort.transform.rotation = new Vector3(-45, (float)(180* cirlce/Math.PI), 0);
                cirlce += MoveInput.Y/10;
                if(!(R<220 && MoveInput.X>0))
                    R -= MoveInput.X * 10;
                CurrentScene.Update(RdeltaTime);
                MainViewPort.DrawScene(CurrentScene);
                EfficientcyAnalisis();
            }
        }

        private void EfficientcyAnalisis()
        {
            stopwatch.Stop();
            deltaTime = (int)stopwatch.ElapsedMilliseconds;
            timeCounter = (int)(timeCounter + deltaTime);
            frameCounter = frameCounter + 1;
            if (timeCounter > 1000)
            {
                FPS = frameCounter;
                frameCounter = 0;
                timeCounter = 0;
            }
            infoFunc("FPS: " + FPS.ToString() + " timeCounter: " + timeCounter.ToString());
            stopwatch.Reset();
            stopwatch.Start();
        }

        public void ReSize(int newWidth, int newHeigth)
        {
            MainViewPort.ReSize(newWidth, newHeigth);
        }
    }
}
