using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ProjectGraphics2
{
    class Kwtsh : _3D_Model
    {
        public float Rad = 50;
        public float RadSmall = 25;

        public void Design()
        {

            float xx, yy, ZZ =0;
            int i = 0;
            float inc = 10;
            int steps = (int)(360 / inc);
            int iStart;
            for (int k = 0; k < 1; k++)
            {

                iStart = i;
                for (float th = 0; th < 360; th += inc)
                {


                    xx = (float)(Math.Cos(th * Math.PI / 180) * Rad);
                    yy = (float)(Math.Sin(th * Math.PI / 180) * Rad);
                    L_3D_Pts.Add(new _3D_Point(xx, yy, ZZ));

                  /*  if (i > 0)
                        if (th > 0)
                        {
                            AddEdge(i, i - 1, Color.Brown);
                        }

                    
                        AddEdge(i, i + steps, Color.Yellow);

                    i++;*/

                }
             //   AddEdge(i - 1, iStart, Color.Brown);
                int j = 0;
                for (float th = 0; th < 360; th += inc)
                {
                    xx = (float)(Math.Cos(th * Math.PI / 180) * RadSmall);
                    yy = (float)(Math.Sin(th * Math.PI / 180) * RadSmall);

                    L_3D_Pts.Add(new _3D_Point(xx, yy, ZZ));

                    if (j > 0)
                    {
                        AddEdge(i, i - 1, Color.Green);
                    }
                    i++;
                    j++;
                }
                AddEdge(i - 1, i - j, Color.Green);

                ZZ += 30;

            }
        }
    }
}
