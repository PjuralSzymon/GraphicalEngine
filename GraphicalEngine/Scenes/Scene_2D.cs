using GraphicalEngine.Engine;
using GraphicalEngine.GameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object = GraphicalEngine.GameObject.Object;

namespace GraphicalEngine.Scenes
{
    class Scene_2D : GameScene
    {
        public Line Line = new Line(new Point(100,100),new Point(300,300));
        public override void DrawAll(IntPtr BitMap, int width, int height) 
        {
            Line.Draw(BitMap, width, height);
        }

        public override void ClearAll(IntPtr BitMap, int width, int height)
        {
            Line.Clear(BitMap, width, height);
        }
        public override void Start(ViewPort _viewport)
        { 
            
        }
        public override void Update(float deltaTime) 
        {
            Line.setStartPos(Line.initP + new Point((int)(100*deltaTime), 0));
        }
    }
}
