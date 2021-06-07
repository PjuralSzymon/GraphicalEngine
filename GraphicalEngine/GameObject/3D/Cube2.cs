using GraphicalEngine.Components;
using GraphicalEngine.Engine;
using System;
using System.Collections.Generic;
using System.Numerics;


namespace GraphicalEngine.GameObject
{
    class Cube2 : Object
    {
        private Vector4[,] points = new Vector4[4, 2];
        float precission = 50;
        //private Triangle2D[] triangles = new Triangle2D[12];

        public Cube2()
        {
            transform = new Transform();

            transform.size.X = 30;
            transform.size.Y = 30;
            transform.size.Z = 30;

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
        }


        public override void Start(ViewPort _viewport) 
        {
        }
        public override void Update(float deltaTime)
        {

        }

        public override List<Vector4> getPixels() 
        {
            List<Vector4> toDraw = new List<Vector4>();
            toDraw.AddRange(Triangle.getPixels(points[0, 0], points[1, 0], points[0, 1], precission));
            toDraw.AddRange(Triangle.getPixels(points[1, 1], points[0, 1], points[1, 0], precission));
            toDraw.AddRange(Triangle.getPixels(points[1, 1], points[2, 1], points[0, 1], precission));
            toDraw.AddRange(Triangle.getPixels(points[3, 1], points[2, 1], points[0, 1], precission));
            toDraw.AddRange(Triangle.getPixels(points[3, 1], points[2, 1], points[2, 0], precission));
            toDraw.AddRange(Triangle.getPixels(points[3, 1], points[3, 0], points[2, 0], precission));
            toDraw.AddRange(Triangle.getPixels(points[0, 0], points[3, 0], points[2, 0], precission));
            toDraw.AddRange(Triangle.getPixels(points[0, 0], points[1, 0], points[2, 0], precission));
            toDraw.AddRange(Triangle.getPixels(points[1, 1], points[2, 1], points[2, 0], precission));
            toDraw.AddRange(Triangle.getPixels(points[3, 0], points[3, 1], points[0, 1], precission));
            toDraw.AddRange(Triangle.getPixels(points[3, 0], points[3, 0], points[0, 1], precission));
            return toDraw;
        }

    }
}
