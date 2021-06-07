using GraphicalEngine.Engine;
using System;
using System.Collections.Generic;
using Object = GraphicalEngine.GameObject.Object;


namespace GraphicalEngine.Scenes
{
    public class GameScene
    {
        public ViewPort viewport;

        public List<Object> gameObjects = new List<Object>();
        public virtual void DrawAll(IntPtr BitMap, int width, int height) { }

        public virtual void ClearAll(IntPtr BitMap, int width, int height) { }
        public virtual void Start(ViewPort _viewport) { viewport = _viewport; }
        public virtual void Update(float deltaTime) { }

    }
}
