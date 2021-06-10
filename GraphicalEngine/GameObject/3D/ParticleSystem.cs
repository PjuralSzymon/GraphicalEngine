using GraphicalEngine.Components;
using GraphicalEngine.Components.Meshes;
using GraphicalEngine.Engine;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace GraphicalEngine.GameObject
{
    public class ParticleSystem :Object
    {
        public List<Object> Particles = new List<Object>();
        public int NumberOfParticles = 20;

        //Movment
        public float acceleration = 0.1f;
        public float accelerationRandomness = 0.1f;

        public float distance = 30;
        public Vector3 Direction = new Vector3(0, -1, 0);
        public float DirectionRandomness = 0.5f;
        //Color
        public Color ColorStarting = new Color(0, 0, 0);
        public Color ColorEnding = new Color(255, 255, 255);
        //Size
        public Vector3 SizeStarting = new Vector3(0, 0, 0);
        public Vector3 SizeEnding = new Vector3(0, 0, 0);
        //Rotation
        private Vector3 RotationStarting = new Vector3(0, 0, 0);
        public Vector3 RotationEnding = new Vector3(45, 45, 45);

        private Random rnd = new Random();

        public void StartParticles()
        {
            for (int i = 0; i < NumberOfParticles; i++)
            {
                Particle P = new Particle();
                P.transform.position = transform.position;
                P.transform.size = SizeStarting;
                P.transform.rotation = RotationStarting;

                P.SizeEnding = SizeEnding;
                P.RotationEnding = RotationEnding;

                P.Direction = GetRandomDirection();
                P.acceleration =  GetRandomAcceleration();
                P.distance = 130 + (float)rnd.NextDouble() * 10;
                P.ColorStarting = ColorStarting;
                P.ColorEnding = ColorEnding;

                Particles.Add(P);
            }
        }
        public override void Start() 
        { 

        }
        public override void Update(float deltaTime) 
        {
            foreach (Particle P in Particles)
            {
                P.Update(deltaTime);
            }
        }

        private Vector3 GetRandomDirection()
        {
            Vector3 RandDirection = new Vector3((float)rnd.NextDouble()*2 -1, (float)rnd.NextDouble() * 2 - 1, (float)rnd.NextDouble() * 2 - 1);
            return new Vector3((Direction.X * (1 - DirectionRandomness) + DirectionRandomness * RandDirection.X),
                               (Direction.Y * (1 - DirectionRandomness) + DirectionRandomness * RandDirection.Y),
                               (Direction.Z * (1 - DirectionRandomness) + DirectionRandomness * RandDirection.Z));
        }
        private float GetRandomAcceleration()
        {
            float RandAcceleration = (float)rnd.NextDouble();
            return acceleration * (1 - accelerationRandomness) + accelerationRandomness * RandAcceleration;
        }

    }
}
