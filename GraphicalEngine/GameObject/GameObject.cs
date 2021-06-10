using GraphicalEngine.Components;
using GraphicalEngine.Components.Meshes;
using GraphicalEngine.Engine;
using System;
using System.Collections.Generic;
using System.Numerics;
using Object = GraphicalEngine.GameObject.Object;

namespace GraphicalEngine.GameObject
{
    public class Object
    {
        public Color color = new Color(0, 0, 0);
        public Transform transform = new Transform();
        public Mesh mesh = new Mesh();

        public virtual List<Point> getPixels() { return new List<Point>(); }
        public virtual void Start( ) { }
        public virtual void Update(float deltaTime) { }
    }
}
