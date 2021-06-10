using GraphicalEngine.Components;
using GraphicalEngine.Engine;
using GraphicalEngine.GameObject;
using System;
using System.Collections.Generic;
using System.Numerics;
using Object = GraphicalEngine.GameObject.Object;

namespace GraphicalEngine.Scenes
{
    public class Campfire : GameScene
    {
        Cube floor = new Cube();

        Cube floorMiddleObj = new Cube();
        ParticleSystem fire = new ParticleSystem();
        private Cube Lpoint = new Cube();

        Cube Cube1 = new Cube();
        Cube Cube2 = new Cube();
        Cube Cube3 = new Cube();
        Cube Cube4 = new Cube();

        Vector3 floorMiddle = new Vector3(0,150,0);
        public override void Start()
        {
            //floor:
            float floorDelta = 25;
            floor.color = new Color(7, 59, 76);
            floor.transform.position = floorMiddle;
            floor.transform.size = new Vector3(250, floorDelta/4, 250); ;
            floor.transform.rotation = new Vector3(0,45,0);
            floorMiddle = floor.transform.position - new Vector3(0, floorDelta / 4, 0);

            //fire:
            floorMiddleObj.color = new Color(255, 0, 0);
            floorMiddleObj.transform.position = floorMiddle;
            floorMiddleObj.transform.size = new Vector3(30, 30, 30);
            StartFire();

            //Light:
            LightSource.transform.position = floorMiddle - new Vector3(0, 40, 0); ;
            LightSource.maxDistance = 200;
            Lpoint.transform.position.X = -100;
            Lpoint.transform.size = new Vector3(70, 70, 70);
            Lpoint.color = new Color(255, 255, 255);
            Lpoint.transform.position = LightSource.transform.position;

            //Cubes:

            Cube1.transform.position = floorMiddle - new Vector3(70, 10, 70);
            Cube1.transform.rotation = new Vector3(0, 45, 0);
            Cube1.transform.size = new Vector3(20, 50, 20);
            Cube1.color = new Color(17, 138, 178);
            Cube2.transform.position = floorMiddle - new Vector3(-70, 10, 70);
            Cube2.transform.rotation = new Vector3(0, 45, 0);
            Cube2.transform.size = new Vector3(20, 50, 20);
            Cube2.color = new Color(6, 214, 160);
            Cube3.transform.position = floorMiddle - new Vector3(70, 10, -70);
            Cube3.transform.rotation = new Vector3(0, 45, 0);
            Cube3.transform.size = new Vector3(20, 50, 20);
            Cube3.color = new Color(255, 209, 102);
            Cube4.transform.position = floorMiddle - new Vector3(-70, 10, -70);
            Cube4.transform.rotation = new Vector3(0, 45, 0);
            Cube4.transform.size = new Vector3(20, 50, 20);
            Cube4.color = new Color(239, 71, 111);
            //init:
            gameObjects.Add(floor);
            gameObjects.Add(Cube1);
            gameObjects.Add(Cube2);
            gameObjects.Add(Cube3);
            gameObjects.Add(Cube4);

            gameObjects.Add(floorMiddleObj);
            gameObjects.AddRange(fire.Particles);
            foreach (Object gameobject in gameObjects)
                gameobject.Start();
        }

        float sinX = 0f;
        Random rng = new Random();
        public override void Update(float deltaTime)
        {
            sinX += rng.Next()/50;
            LightSource.transform.position.Y = (float)(10 * Math.Cos(sinX));
            Lpoint.transform.position = LightSource.transform.position;

            fire.Update(deltaTime);
        }


        //FIRE:       
        private void StartFire()
        {
            fire.NumberOfParticles = 25;

            fire.ColorStarting = new Color(161, 1, 0);
            fire.ColorEnding = new Color(255, 247, 93);
            fire.distance = 10;

            fire.transform.position = floorMiddle;
            fire.DirectionRandomness = 0.3f;
            fire.accelerationRandomness = 0.2f;

            fire.SizeStarting = new Vector3(40, 40, 40);
            fire.SizeEnding = new Vector3(0, 0, 0);
            fire.transform.rotation = new Vector3(0, 0, 0);
            fire.StartParticles();
        }
    }
}
