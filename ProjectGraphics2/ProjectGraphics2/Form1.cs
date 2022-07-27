using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectGraphics2
{
    public partial class Form1 : Form
    {
        Bitmap off;

        _3D_Model cube = new _3D_Model();

        List<Kwtsh> kwtsh = new List<Kwtsh>();
        List<_3D_Model> Bplane = new List<_3D_Model>();


        Camera Cam;
        Timer tt = new Timer();
        Random rr = new Random();

        int whereiam = 0;
        int Uct = 0, Rct = 0, Lct = 0, ct = 0;
        bool Rclicked = false;
        bool RC = false;
        bool LC = false;
        bool Lclicked = false;
        bool flagz = false;

        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.KeyDown += Form1_KeyDown;
            this.Load += Form1_Load;
            this.Paint += Form1_Paint;
            tt.Tick += Tt_Tick;
            tt.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Right && cube.L_3D_Pts[0].X < 200)
            {
                RC = true;
            }
            if (e.KeyCode == Keys.Left && cube.L_3D_Pts[2].X > -399)
            {

                LC = true;
            }
            DrawDubb(this.CreateGraphics());
        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            if (Uct + 16 > 1000)
            {
                tt.Stop();
                //   MessageBox.Show("You Won!");
            }
            if (Rclicked == false && Lclicked == false && flagz == false)
            {
                {
                    _3D_Point v1 = new _3D_Point(Bplane[Uct].L_3D_Pts[2]);
                    _3D_Point v2 = new _3D_Point(Bplane[Uct].L_3D_Pts[3]);

                    Transformation.RotateArbitrary(cube.L_3D_Pts, v1, v2, -10);

                    ct++;

                    if (ct == 9 && !RC && !LC)
                    {
                        Uct += 8;
                        ct = 0;
                        whereiam += 8;

                        for (int i = 0; i < Bplane.Count; i++)
                        {
                            Bplane[i].TransY(-8);
                        }
                        for (int i = 0; i < kwtsh.Count; i++)
                        {
                            kwtsh[i].TransY(-8);
                        }
                    }
                    if (ct == 9 && RC && !Rclicked)
                    {
                        Rclicked = true;
                        Uct += 8;
                        ct = 0;
                        whereiam += 8;
                    }
                    if (ct == 9 && LC && !Lclicked)
                    {
                        Lclicked = true;
                        Uct += 8;
                        ct = 0;
                        whereiam += 8;
                    }
                }
            }
            if (Rclicked == true)
            {
                _3D_Point v1 = new _3D_Point(Bplane[Rct].L_3D_Pts[1]);
                _3D_Point v2 = new _3D_Point(Bplane[Rct].L_3D_Pts[2]);

                Transformation.RotateArbitrary(cube.L_3D_Pts, v1, v2, -10);

                ct++;

                if (ct == 9)
                {
                    Rct += 1;
                    Lct += 1;
                    ct = 0;
                    RC = false;
                    Rclicked = false;
                    Lclicked = false;
                    whereiam += 1;
                }

            }
            if (Lclicked == true)

            {
                _3D_Point v1 = new _3D_Point(Bplane[Lct].L_3D_Pts[3]);
                _3D_Point v2 = new _3D_Point(Bplane[Lct].L_3D_Pts[0]);

                Transformation.RotateArbitrary(cube.L_3D_Pts, v1, v2, -10);

                ct++;

                if (ct == 9)
                {
                    Rct -= 1;
                    Lct -= 1;
                    ct = 0;
                    tt.Start();
                    LC = false;
                    Lclicked = false;
                    Rclicked = false;
                    whereiam -= 1;
                }

            }

            if (Bplane[whereiam].f == 1)
            {
                flagz = true;
                //tt.Stop();
                Transformation.TranslateZ(cube.L_3D_Pts, Bplane[whereiam].L_3D_Pts[0].Z + 3);

                //  MessageBox.Show("Game Over!");
            }

            // ctTick++;
            DrawDubb(this.CreateGraphics());
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(ClientSize.Width, ClientSize.Height);
            int x = -500;
            int y = -600;
            CreateCamera();

            for (int i = 1; i < 1001; i++)
            {
                _3D_Model pnn = new _3D_Model();
                pnn.f = 0;
                CreatePlane(pnn, x, y, 0);
                x += 100;
                Bplane.Add(pnn);

                if (i % 8 == 0)
                {
                    x = -500;
                    y += 100;
                }
            }
            CreateCube(cube, -500, -600, 0);

            int value;
            int kwtshno = rr.Next(150);
            for (int i = 0; i < kwtshno; i++)
            {
                value = rr.Next(1000);
                Kwtsh pnn = new Kwtsh();
                pnn.cam = Cam;
                pnn.Design();
                kwtsh.Add(pnn);

                Transformation.TranslateY(kwtsh[i].L_3D_Pts, Bplane[value].L_3D_Pts[0].Y + 50);
                Transformation.TranslateX(kwtsh[i].L_3D_Pts, Bplane[value].L_3D_Pts[0].X + 50);
                Bplane[value].f = 1;
            }


            for (int i = 0; i < 20; i++)
            {
                GoBackwrd();
            }
        }

        void CreatePlane(_3D_Model pln, float XS, float YS, float ZS)
        {
            float[] vert =
                            {
                            0  ,  0  ,0,
                            100   , 0   ,0,
                            100   , 100   ,0,
                            0  ,  100 , 0
                        };


            _3D_Point pnn;
            int j = 0;
            for (int i = 0; i < 4; i++)
            {
                pnn = new _3D_Point(vert[j] + XS, vert[j + 1] + YS, vert[j + 2] + ZS);
                j += 3;
                pln.AddPoint(pnn);
            }


            int[] Edges = {
                            0,1 ,
                            1,2 ,
                            2,3 ,
                            3,0
                        };
            j = 0;
            Color[] cl = { Color.Red, Color.Yellow, Color.Black, Color.Blue };
            for (int i = 0; i < 4; i++)
            {
                pln.AddEdge(Edges[j], Edges[j + 1], Color.Red);

                j += 2;
            }
            pln.cam = Cam;
        }

        void CreateCube(_3D_Model cb, float XS, float YS, float ZS)
        {
            float[] vert =
        {
                            0  ,  0  ,0,
                            100   , 0   ,0,
                            100   , 100   ,0,
                            0  ,  100 , 0,

                            0  ,  0  ,-100,
                            100   , 0   ,-100,
                            100   , 100   ,-100,
                            0  ,  100 , -100,


    };


            _3D_Point pnn;
            int j = 0;
            for (int i = 0; i < 8; i++)
            {
                pnn = new _3D_Point(vert[j] + XS, vert[j + 1] + YS, vert[j + 2] + ZS);
                j += 3;
                cb.AddPoint(pnn);
            }


            int[] Edges = {
                            0,1 ,
                            1,2 ,
                            2,3 ,
                            3,0 ,

                            4,5,
                            5,6,
                            6,7,
                            7,4,

                            0,4,
                            1,5,
                            2,6,
                            3,7

                        };
            j = 0;
            Color[] cl = { Color.Red, Color.Yellow, Color.Black, Color.Blue };
            for (int i = 0; i < 12; i++)
            {
                cb.AddEdge(Edges[j], Edges[j + 1], cl[3]);

                j += 2;
            }

            cb.cam = Cam;
        }
        void CreateCamera()
        {
            Cam = new Camera();

            Cam.cxScreen = 70;
            Cam.cyScreen = 70;

            Cam.ceneterX = this.ClientSize.Width / 2;
            Cam.ceneterY = this.ClientSize.Height / 2;

            Cam.BuildNewSystem();

        }

        void GoBackwrd()
        {
            float diffX = Cam.lookAt.X - Cam.cop.X;
            float diffY = Cam.lookAt.Y - Cam.cop.Y;
            float diffZ = Cam.lookAt.Z - Cam.cop.Z;

            float step = 0.01f;

            Cam.cop.X -= diffX * step;
            Cam.cop.Y -= diffY * step;
            Cam.cop.Z -= diffZ * step;

            Cam.BuildNewSystem();

        }
        void DrawScene(Graphics g)
        {

            g.Clear(Color.White);
            for (int i = 0; i < Bplane.Count; i++)
            {
                Bplane[i].DrawYourSelf(g);
            }
            for (int i = 0; i < kwtsh.Count; i++)
            {
                kwtsh[i].DrawYourSelf(g);
            }


            cube.DrawYourSelf(g);

        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}
