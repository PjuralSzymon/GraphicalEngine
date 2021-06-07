using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEngine.Components.Meshes
{
    public class Mesh
    {
        public List<Triangle> triangles = new List<Triangle>();

        public Mesh(int size=0)
        {
            setSize(size);
        }
        public void setSize(int size)
        {
            for (int i = 0; i < size; i++)
                triangles.Add(new Triangle());
        }
        public Triangle[] getTriangles()
        {
            return triangles.ToArray();
        }

    }
}
