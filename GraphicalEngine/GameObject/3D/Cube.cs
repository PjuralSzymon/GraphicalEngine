using GraphicalEngine.Components;
using GraphicalEngine.Engine;
using System;
using System.Numerics;

namespace GraphicalEngine.GameObject
{
    class Cube : Object
    {

        private Vector4[,] points = new Vector4[4, 2];

        public Cube()
        {

 
        }

        public override void Start() 
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
            mesh.setSize(12);

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

        }

    }
}
