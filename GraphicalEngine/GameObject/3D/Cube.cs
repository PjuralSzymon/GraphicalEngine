using GraphicalEngine.Components;
using GraphicalEngine.Engine;
using System;
using System.Numerics;

namespace GraphicalEngine.GameObject
{
    class Cube : Object
    {
        public Transform transform;

        private ViewPort viewport;
        private Color _color = new Color(255, 0, 0);
        public Color color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                for (int i = 0; i < 12; i++)
                {
                    triangles[i].FILLCOLOR = _color;
                }
            }
        }

        private Vector4[,] points = new Vector4[4, 2];
        private Triangle2D[] triangles = new Triangle2D[12];

        public Cube()
        {
            transform.size.X = 50;
            transform.size.Y = 50;
            transform.size.Z = 50;

            points[0, 0] = new Vector4(- transform.pivot.X * transform.size.X,
                                       + transform.pivot.Y * transform.size.Y,
                                       - transform.pivot.Z * transform.size.Z,
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

            for (int i=0; i<12;i++)
            {
                triangles[i] = new Triangle2D();
                triangles[i].FILLCOLOR = color;
            }    
        }

        private void UpdateTriangles(int width, int height)
        {
            triangles[0].a.rePos(transform.translate3Dto2D(points[0, 0], width, height, viewport.transform));
            triangles[0].b.rePos(transform.translate3Dto2D(points[0, 1], width, height, viewport.transform));
            triangles[0].c.rePos(transform.translate3Dto2D(points[1, 0], width, height, viewport.transform));

            triangles[1].a.rePos(transform.translate3Dto2D(points[1, 1], width, height, viewport.transform));
            triangles[1].b.rePos(transform.translate3Dto2D(points[0, 1], width, height, viewport.transform));
            triangles[1].c.rePos(transform.translate3Dto2D(points[1, 0], width, height, viewport.transform));

            triangles[2].a.rePos(transform.translate3Dto2D(points[1, 1], width, height, viewport.transform));
            triangles[2].b.rePos(transform.translate3Dto2D(points[2, 1], width, height, viewport.transform));
            triangles[2].c.rePos(transform.translate3Dto2D(points[0, 1], width, height, viewport.transform));

            triangles[3].a.rePos(transform.translate3Dto2D(points[3, 1], width, height, viewport.transform));
            triangles[3].b.rePos(transform.translate3Dto2D(points[2, 1], width, height, viewport.transform));
            triangles[3].c.rePos(transform.translate3Dto2D(points[0, 1], width, height, viewport.transform));

            triangles[4].a.rePos(transform.translate3Dto2D(points[3, 1], width, height, viewport.transform));
            triangles[4].b.rePos(transform.translate3Dto2D(points[2, 1], width, height, viewport.transform));
            triangles[4].c.rePos(transform.translate3Dto2D(points[2, 0], width, height, viewport.transform));

            triangles[5].a.rePos(transform.translate3Dto2D(points[3, 1], width, height, viewport.transform));
            triangles[5].b.rePos(transform.translate3Dto2D(points[3, 0], width, height, viewport.transform));
            triangles[5].c.rePos(transform.translate3Dto2D(points[2, 0], width, height, viewport.transform));

            triangles[6].a.rePos(transform.translate3Dto2D(points[0, 0], width, height, viewport.transform));
            triangles[6].b.rePos(transform.translate3Dto2D(points[3, 0], width, height, viewport.transform));
            triangles[6].c.rePos(transform.translate3Dto2D(points[2, 0], width, height, viewport.transform));

            triangles[7].a.rePos(transform.translate3Dto2D(points[0, 0], width, height, viewport.transform));
            triangles[7].b.rePos(transform.translate3Dto2D(points[1, 0], width, height, viewport.transform));
            triangles[7].c.rePos(transform.translate3Dto2D(points[2, 0], width, height, viewport.transform));

            triangles[8].a.rePos(transform.translate3Dto2D(points[1, 1], width, height, viewport.transform));
            triangles[8].b.rePos(transform.translate3Dto2D(points[1, 0], width, height, viewport.transform));
            triangles[8].c.rePos(transform.translate3Dto2D(points[2, 0], width, height, viewport.transform));

            triangles[9].a.rePos(transform.translate3Dto2D(points[1, 1], width, height, viewport.transform));
            triangles[9].b.rePos(transform.translate3Dto2D(points[2, 1], width, height, viewport.transform));
            triangles[9].c.rePos(transform.translate3Dto2D(points[2, 0], width, height, viewport.transform));

            triangles[10].a.rePos(transform.translate3Dto2D(points[3, 0], width, height, viewport.transform));
            triangles[10].b.rePos(transform.translate3Dto2D(points[3, 1], width, height, viewport.transform));
            triangles[10].c.rePos(transform.translate3Dto2D(points[0, 1], width, height, viewport.transform));

            triangles[11].a.rePos(transform.translate3Dto2D(points[3, 0], width, height, viewport.transform));
            triangles[11].b.rePos(transform.translate3Dto2D(points[0, 0], width, height, viewport.transform));
            triangles[11].c.rePos(transform.translate3Dto2D(points[0, 1], width, height, viewport.transform));
        }

        public override void Start(ViewPort _viewport) 
        { 
            viewport = _viewport; 
        }

        public override void Update(float deltaTime)
        {

        }

        public override void Draw(IntPtr BitMap, int width, int height) 
        {
            UpdateTriangles(width, height);
            for (int i = 0; i < 12; i++)
            {
                triangles[i].Draw(BitMap, width, height);
            }
        }

    }
}
