using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tao.FreeGlut;
using Tao.OpenGl;
using Tao.Platform;

namespace CurseWorkOpenGLRogoznev
{
    public partial class Form1 : Form
    {
        double a = 0, b = 0, c = -5;
        int vanishingPointOX = 0, vanishingPointOY = 0;
        float xVanishingPoint=0.0f, yVanishingPoint=0.0f;


        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            vanishingPointOX = trackBar1.Value;
            Xlabel.Text =vanishingPointOX.ToString()+";0;0;";
            xVanishingPoint = (float)vanishingPointOX/100.0f; //100 для проверки, выяснить сколько надо брать
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            vanishingPointOY = trackBar2.Value;
            Ylabel.Text = "0;" + vanishingPointOY.ToString()+";0";
            yVanishingPoint = (float)vanishingPointOY/100.0f;//100 для проверки, выяснить сколько надо брать
        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            Draw();
        }

        private void Draw()
        {
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            float[] m = new float[] { 1, 0, 0, xVanishingPoint, 0, 1, 0, yVanishingPoint, 0, 0, 1, 0, 0, 0, 0, 1 };
            Gl.glLoadMatrixf(m);

            Glu.gluPerspective(45, (float)AnT.Width / (float)AnT.Height, 2, 200);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glClearColor(255, 255, 255, 1);
            Gl.glLoadIdentity();
            Gl.glPushMatrix();
            Gl.glTranslated(a, b, c);
            //Gl.glRotated(d, os_x, os_y, os_z);
            //Gl.glScaled(zoom, zoom, zoom);

            switch (FigureChanger.SelectedIndex)//тетраэдр гексаэдр октаэдр
            {
                case 0:
                    {
                        Glut.glutWireTetrahedron();
                    }
                    break;
                case 1:
                    {
                        Glut.glutWireCube(1);
                    }
                    break;
                case 2:
                    {
                        Glut.glutWireOctahedron();
                    }
                    break;

            }
            Gl.glPopMatrix();
            Gl.glFlush();
            AnT.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE);
            Gl.glClearColor(255, 255, 255, 1);
            Gl.glViewport(0, 0, AnT.Width, AnT.Height);
            FigureChanger.SelectedIndex = 0;
            RenderTimer.Start();
        }
    }

}
