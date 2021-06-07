using GraphicalEngine.Engine;
using GraphicalEngine.GameObject;
using System;

namespace GraphicalEngine.Scenes
{
    public class Scene_3D : GameScene
    {
        public ViewPort viewport;

        Cube Cube2 = new Cube();
        Cube Cube3 = new Cube();
        Cube Cube4 = new Cube();
        Triangle2D Triangle = new Triangle2D(new Point(50,50), new Point(50,200),new Point(200,200));
        public override void DrawAll(IntPtr BitMap, int width, int height) 
        {
            //Cube.Draw(BitMap, width, height);
            Cube2.Draw(BitMap, width, height);
            Cube3.Draw(BitMap, width, height);
            Cube4.Draw(BitMap, width, height);
            //Triangle.Draw(BitMap, width, height);
        }

        public override void ClearAll(IntPtr BitMap, int width, int height) { }
        public override void Start(ViewPort _viewport)
        {
            viewport = _viewport;
            Cube2.Start(viewport);
            Cube3.Start(viewport);
            Cube4.Start(viewport);
            Cube2.color = new Color(0, 255, 255);
            Cube3.color = new Color(0, 0, 255);
            Cube4.color = new Color(255, 0, 255);

            Cube2.transform.position.X = -100;
            Cube4.transform.position.X = 100;
        }
        public override void Update(float deltaTime) 
        {
            Cube2.transform.rotation.X += 1;

            Cube3.transform.rotation.Y += 1;

            Cube4.transform.rotation.Z -= 1;
        }

    }
}
