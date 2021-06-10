using GraphicalEngine.Components.Meshes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalEngine.Algorithms
{
public static class Liang_Barsky_Alg
    {
        //I think that in my program I have a different reference system than in the examples,
        //therefore the values are inverted
        public static (Point a, Point b) LiangBarsky(Point p1, Point p2, int width, int height) 
        { 
            float dx = p2.x - p1.x, dy = p2.y - p1.y; 
            float tE = 0, tL = 1; 
            if (Clip(dx, -(p1.x - 0), ref tE, ref tL)) 
                if (Clip(-dx, -(width - p1.x), ref tE, ref tL)) 
                    if (Clip(-dy, p1.y - height, ref tE, ref tL)) 
                        if (Clip(dy, 0 - p1.y, ref tE, ref  tL)) 
                        { 
                            if (tL < 1) 
                            {
                                p2.x = (int)(p1.x + dx * tL); 
                                p2.y = (int)(p1.y + dy * tL); 
                            } 
                            if (tE > 0) 
                            { 
                                p1.x += (int)(dx * tE); 
                                p1.y += (int)(dy * tE); 
                            } 
                            return (p1, p2); 
                        }
            return (new Point(0,0,p1.Z), new Point(0, 0, p2.Z));
        }

        private static bool Clip(float denom, float numer,ref float tE,ref float tL)
        {
            if (denom == 0)
            {  
                //Paralel line
                if ( numer > 0)
                    return false; // outside - discard
                return true; //skip to next edge
            }
            float t = numer/denom;
            if (denom > 0) 
            { 
                //PE
                if (t > tL) //tE > tL - discard
                   return false;
                if (t > tE)
                    tE = t;
            }
            else 
            {
                //denom < 0 - PL
                if (t < tE) //tL < tE - discard
                    return false;
                if ( t < tL )
                    tL = t;
            }return true;
        }
        }
}
