using GraphicalEngine.Components;
using GraphicalEngine.Engine;
using GraphicalEngine.GameObject;
using System;
using System.Collections.Generic;
using System.Numerics;
using Object = GraphicalEngine.GameObject.Object;

namespace GraphicalEngine.Scenes
{
    public class Arm : GameScene
    {
        Cube arm1 = new Cube();
        Cube arm2 = new Cube();

        public override void Start()
        {
            arm1.color = new Color(255, 0, 0);
            arm1.transform.size = new Vector3(100, 25, 25);
            arm1.transform.position = new Vector3(50, 1, 0);
            arm1.transform.pivot = new Vector3(0.0f, 0.5f, 0.5f);
            arm1.transform.Parent = arm2.transform;
            arm2.color = new Color(0, 255, 0);
            arm2.transform.size = new Vector3(100, 25, 25);
            arm2.transform.position = new Vector3(-50, 0, 0);

            gameObjects.Add(arm1);
            gameObjects.Add(arm2);

            foreach (Object gameobject in gameObjects)
                gameobject.Start();
        }

        float sinX = 0;
        public override void Update(float deltaTime)
        {
            sinX += 0.1f;
            arm2.transform.rotation.Y = (float)(150 * Math.Sin(sinX));
            arm1.transform.rotation.Y = (float)(150 * Math.Sin(sinX));
        }
    }
}
