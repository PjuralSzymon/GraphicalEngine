using GraphicalEngine.Components;
using GraphicalEngine.Engine;
using System;
using System.Numerics;

namespace GraphicalEngine.GameObject
{
    public class Particle : Object
    {
        //Movment
        public float acceleration = 0.1f;
        public float distance = 30;
        public Vector3 Direction = new Vector3(0, -1, 0);
        //Color
        public Color ColorStarting = new Color(0, 0, 0);
        public Color ColorEnding = new Color(255, 255, 255);
        //Size
        private Vector3 SizeStarting = new Vector3(0, 0, 0);
        public Vector3 SizeEnding = new Vector3(0, 0, 0);
        //Rotation
        private Vector3 RotationStarting = new Vector3(0, 0, 0);
        public Vector3 RotationEnding = new Vector3(45, 45, 45);

        private Vector4[,] points = new Vector4[4, 2];
        
        private float xMove = 0;
        private Vector3 StartingPos = new Vector3();
        public Particle()
        {


        }

        public override void Start()
        {
            mesh.setSize(12);

            rewriteCord();
            StartingPos = transform.position;
            SizeStarting = transform.size;
            RotationStarting = transform.rotation;
        }

        private void rewriteCord()
        {
            points[0, 0] = new Vector4(-transform.pivot.X * transform.size.X,
                +transform.pivot.Y * transform.size.Y,
                -transform.pivot.Z * transform.size.Z,
                1);
            points[1, 0] = new Vector4(-transform.pivot.X * transform.size.X,
                                       +transform.pivot.Y * transform.size.Y,
                                       +transform.pivot.Z * transform.size.Z,
                                       1);
            points[2, 0] = new Vector4(-transform.pivot.X * transform.size.X,
                                       -transform.pivot.Y * transform.size.Y,
                                       +transform.pivot.Z * transform.size.Z,
                                       1);
            points[3, 0] = new Vector4(-transform.pivot.X * transform.size.X,
                                       -transform.pivot.Y * transform.size.Y,
                                       -transform.pivot.Z * transform.size.Z,
                                       1);

            points[0, 1] = new Vector4(+(1 - transform.pivot.X) * transform.size.X,
                                       +(1 - transform.pivot.Y) * transform.size.Y,
                                       -(1 - transform.pivot.Z) * transform.size.Z,
                                       1);
            points[1, 1] = new Vector4(+(1 - transform.pivot.X) * transform.size.X,
                                       +(1 - transform.pivot.Y) * transform.size.Y,
                                       +(1 - transform.pivot.Z) * transform.size.Z,
                                       1);
            points[2, 1] = new Vector4(+(1 - transform.pivot.X) * transform.size.X,
                                       -(1 - transform.pivot.Y) * transform.size.Y,
                                       +(1 - transform.pivot.Z) * transform.size.Z,
                                       1);
            points[3, 1] = new Vector4(+(1 - transform.pivot.X) * transform.size.X,
                                       -(1 - transform.pivot.Y) * transform.size.Y,
                                       -(1 - transform.pivot.Z) * transform.size.Z,
                                       1);
            mesh.getTriangles()[0].A = points[0, 0];
            mesh.getTriangles()[0].B = points[0, 1];
            mesh.getTriangles()[0].C = points[1, 0];

            mesh.getTriangles()[1].A = points[1, 1];
            mesh.getTriangles()[1].B = points[0, 1];
            mesh.getTriangles()[1].C = points[1, 0];

            mesh.getTriangles()[2].A = points[1, 1];
            mesh.getTriangles()[2].B = points[2, 1];
            mesh.getTriangles()[2].C = points[0, 1];

            mesh.getTriangles()[3].A = points[3, 1];
            mesh.getTriangles()[3].B = points[2, 1];
            mesh.getTriangles()[3].C = points[0, 1];

            mesh.getTriangles()[4].A = points[3, 1];
            mesh.getTriangles()[4].B = points[2, 1];
            mesh.getTriangles()[4].C = points[2, 0];

            mesh.getTriangles()[5].A = points[3, 1];
            mesh.getTriangles()[5].B = points[3, 0];
            mesh.getTriangles()[5].C = points[2, 0];

            mesh.getTriangles()[6].A = points[0, 0];
            mesh.getTriangles()[6].B = points[3, 0];
            mesh.getTriangles()[6].C = points[2, 0];

            mesh.getTriangles()[7].A = points[0, 0];
            mesh.getTriangles()[7].B = points[1, 0];
            mesh.getTriangles()[7].C = points[2, 0];

            mesh.getTriangles()[8].A = points[1, 1];
            mesh.getTriangles()[8].B = points[1, 0];
            mesh.getTriangles()[8].C = points[2, 0];

            mesh.getTriangles()[9].A = points[1, 1];
            mesh.getTriangles()[9].B = points[2, 1];
            mesh.getTriangles()[9].C = points[2, 0];

            mesh.getTriangles()[10].A = points[3, 0];
            mesh.getTriangles()[10].B = points[3, 1];
            mesh.getTriangles()[10].C = points[0, 1];

            mesh.getTriangles()[11].A = points[3, 0];
            mesh.getTriangles()[11].B = points[0, 0];
            mesh.getTriangles()[11].C = points[0, 1];

        }
        public override void Update(float deltaTime)
        {
            move();
            InterpolateColor();
            InterpolateSize();
            InterpolateRotation();
        }

        private void move()
        {

            if (xMove >= 1)
            {
                transform.position = StartingPos;
                xMove = 0;
            }
            else
            {
                xMove += acceleration;
                Vector3 delta = Direction * distance*(float)(-2.0f / (1.0f + xMove) + 2.0f);
                transform.position = StartingPos + delta;
            }
        }

        private void InterpolateColor()
        {
            if (xMove > 0.9) color = ColorEnding;
            else
            color =  new Color((byte)((float)ColorStarting.R * (1 - xMove) + xMove * ColorEnding.R),
                             (byte)((float)ColorStarting.G * (1 - xMove) + xMove * ColorEnding.G),
                             (byte)((float)ColorStarting.B * (1 - xMove) + xMove * ColorEnding.B)
                            );
        }

        private void InterpolateSize()
        {
            transform.size = new Vector3((SizeStarting.X * (1 - xMove) + xMove * SizeEnding.X),
                                         (SizeStarting.Y * (1 - xMove) + xMove * SizeEnding.Y),
                                         (SizeStarting.Z * (1 - xMove) + xMove * SizeEnding.Z));
            rewriteCord();
        }
        private void InterpolateRotation()
        {
            transform.rotation = new Vector3((RotationStarting.X * (1 - xMove) + xMove * RotationEnding.X),
                                         (RotationStarting.Y * (1 - xMove) + xMove * RotationEnding.Y),
                                         (RotationStarting.Z * (1 - xMove) + xMove * RotationEnding.Z));
        }
    }
}
