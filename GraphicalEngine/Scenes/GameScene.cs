using GraphicalEngine.Engine;
using GraphicalEngine.GameObject;
using System;
using System.Collections.Generic;
using Object = GraphicalEngine.GameObject.Object;


namespace GraphicalEngine.Scenes
{
    public class GameScene
    {
        public ViewPort viewport;

        public List<Object> gameObjects = new List<Object>();

        public Light LightSource = new Light();
        public virtual void Start() { }
        public virtual void Update(float deltaTime) { }

    }
}
