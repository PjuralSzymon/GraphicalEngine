using GraphicalEngine.Components;
using GraphicalEngine.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEngine.GameObject
{
    public class Light
    {
        public Color color = new Color(255,255,255);
        public float maxDistance = 500;
        public Transform transform = new Transform();

        public Color getColor(Color objColor, float distance)
        {
            //return objColor;
            //return objColor;
            if (distance > maxDistance || distance<0) return new Color(0, 0, 0);
            float d = distance / maxDistance;
            return new Color((byte)((float)objColor.R * (1 - d)), 
                             (byte)((float)objColor.G * (1 - d)), 
                             (byte)((float)objColor.B * (1 - d))
                            );
        }
    }
}
