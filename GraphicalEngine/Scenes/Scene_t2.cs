using GraphicalEngine.Engine;
using GraphicalEngine.GameObject;
using System;
using System.Collections.Generic;
using System.Numerics;
using Object = GraphicalEngine.GameObject.Object;

namespace GraphicalEngine.Scenes
{
    public class Scene_t2 : GameScene
    {
        private Cube obj1 = new Cube();
        private Cube obj2 = new Cube();

        private Cube Lpoint = new Cube();

        public override void Start()
        {
            LightSource.transform.position.Z = -50;
            LightSource.maxDistance = 200;
            Lpoint.transform.position.X = -100;
            Lpoint.transform.size = new Vector3(5, 5, 5);
            Lpoint.color = new Color(0, 255, 0);
            Lpoint.transform.position = LightSource.transform.position;


            obj1.color = new Color(255, 0, 0);
            obj2.color = new Color(255, 255, 0);
            obj1.transform.position.X = -50;
            obj2.transform.position.X = 50;
            gameObjects.Add(obj1);
            gameObjects.Add(obj2);
            gameObjects.Add(Lpoint);
        }

        public override void Update(float deltaTime)
        {
            //obj1.transform.rotation.X += 1;
            obj1.transform.rotation.Y += 1;
            //obj1.transform.rotation.Z += 1;

            obj2.transform.rotation.X -= 1;
            obj2.transform.rotation.Y -= 1;
            obj2.transform.rotation.Z -= 1;
        }

    }
}
