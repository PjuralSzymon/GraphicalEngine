using GraphicalEngine.Engine;
using GraphicalEngine.GameObject;
using System;
using System.Collections.Generic;
using Object = GraphicalEngine.GameObject.Object;

namespace GraphicalEngine.Scenes
{
    public class Scene_t2 : GameScene
    {
        private Cube2 obj1 = new Cube2();

        public override void Start(ViewPort _viewport)
        {
            obj1.Start(_viewport);
            gameObjects.Add(obj1);
        }

        public override void Update(float deltaTime)
        {
            obj1.transform.rotation.X += 1;

            obj1.transform.rotation.Y += 1;

            obj1.transform.rotation.Z -= 1;
        }

    }
}
