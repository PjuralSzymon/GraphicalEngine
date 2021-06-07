using GraphicalEngine.Components;
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
        public ViewPort viewport;

        public Transform transform = new Transform();

        public virtual List<Vector4> getPixels() { return new List<Vector4>(); }
        public virtual void Start(ViewPort _viewport) { viewport = _viewport; }
        public virtual void Update(float deltaTime) { }
        public virtual void Draw(IntPtr BitMap, int width, int height) { }
        public virtual void Clear(IntPtr BitMap, int width, int height) { }
    }
}
