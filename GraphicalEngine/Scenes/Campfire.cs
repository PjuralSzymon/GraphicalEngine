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
        Cube floor1 = new Cube();
        Cube floor2 = new Cube();
        Cube floor3 = new Cube();
        Cube floor4 = new Cube();

        Cube floorMiddleObj = new Cube();
        ParticleSystem fire = new ParticleSystem();
        private Cube Lpoint = new Cube();

        Cube CubeRed = new Cube();
        Cube CubeGreen = new Cube();
        Cube CubeBlue = new Cube();

        Vector3 floorMiddle = new Vector3(0,150,0);
        public override void Start()
        {
            //floor:
            float floorDelta = 25;
            floor1.color = new Color(94, 69, 75);
            floor1.transform.position = new Vector3(0, 150, 150);
            floor1.transform.size = new Vector3(250, floorDelta, 250);
            floor1.transform.rotation = new Vector3(0, 45, 0);
            floor2.color = new Color(216, 179, 132);
            floor2.transform.position = floor1.transform.position - new Vector3(0, 3 * floorDelta / 4, 0);
            floor2.transform.size = new Vector3(250, floorDelta/2, 250); ;
            floor2.transform.rotation = floor1.transform.rotation;
            floor3.color = new Color(243, 240, 215);
            floor3.transform.position = floor2.transform.position - new Vector3(0, 3 * floorDelta / 8, 0);
            floor3.transform.size = new Vector3(250, floorDelta/4, 250); ;
            floor3.transform.rotation = floor1.transform.rotation;
            floor4.color = new Color(206, 229, 208);
            floor4.transform.position = floor3.transform.position - new Vector3(0, 3 * floorDelta / 8, 0);
            floor4.transform.size = new Vector3(250, floorDelta/4, 250); ;
            floor4.transform.rotation = floor1.transform.rotation;
            floorMiddle = floor4.transform.position - new Vector3(0, floorDelta / 4, 0);

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

            CubeRed.transform.position = floorMiddle - new Vector3(70, 10, 70);
            CubeRed.transform.rotation = new Vector3(0, 45, 0);
            CubeRed.transform.size = new Vector3(20, 50, 20);
            CubeRed.color = new Color(255, 0, 0);
            CubeGreen.transform.position = floorMiddle - new Vector3(-70, 10, 70);
            CubeGreen.transform.rotation = new Vector3(0, 45, 0);
            CubeGreen.transform.size = new Vector3(20, 50, 20);
            CubeGreen.color = new Color(0, 255, 0);
            CubeBlue.transform.position = floorMiddle - new Vector3(70, 10, -70);
            CubeBlue.transform.rotation = new Vector3(0, 45, 0);
            CubeBlue.transform.size = new Vector3(20, 50, 20);
            CubeBlue.color = new Color(0, 0, 255);
            //init:
            gameObjects.Add(floor1);
            gameObjects.Add(floor2);
            gameObjects.Add(floor3);
            gameObjects.Add(floor4);
            gameObjects.Add(CubeRed);
            gameObjects.Add(CubeGreen);
            gameObjects.Add(CubeBlue);
            gameObjects.Add(floorMiddleObj);
            gameObjects.AddRange(fire.Particles);
            foreach (Object gameobject in gameObjects)
                gameobject.Start();
        }

        public override void Update(float deltaTime)
        {
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
