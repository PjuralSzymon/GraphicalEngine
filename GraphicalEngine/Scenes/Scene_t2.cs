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
            LightSource.transform.position = new Vector3(0,150,-50);
            LightSource.maxDistance = 200;
            Lpoint.transform.position.X = -100;
            Lpoint.transform.size = new Vector3(20, 20, 20);
            Lpoint.color = new Color(255, 255, 255);
            Lpoint.transform.position = LightSource.transform.position;


            obj1.color = new Color(255, 0, 0);
            obj2.color = new Color(255, 255, 0);
            obj1.transform.position.X = -50;
            obj2.transform.position.X = 50;
            gameObjects.Add(obj1);
            gameObjects.Add(obj2);
            gameObjects.Add(Lpoint);

            foreach (Object gameobject in gameObjects)
                gameobject.Start();
        }

        float sinX = 0;
        public override void Update(float deltaTime)
        {
            sinX += 0.1f;
            LightSource.transform.position.X = (float)(150*Math.Sin(sinX));
            LightSource.transform.position.Y = (float)(150 * Math.Cos(sinX));
            Lpoint.transform.position = LightSource.transform.position;

            obj1.transform.rotation.X += 1;
            obj1.transform.rotation.Y += 1;
            obj1.transform.rotation.Z += 1;

            obj2.transform.rotation.X -= 1;
            obj2.transform.rotation.Y -= 1;
            obj2.transform.rotation.Z -= 1;
        }

    }
}
