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
        double a = 0, b = 0, c = -5, d = 0, zoom = 1, Xcam=0, Ycam=0, Zkam=0, Xpoint=0, Ypoint=0, Zpoint=-5;
        int vanishingPointOX = 1, vanishingPointOY = 1;

        private void W(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                Ycam++;
                Ypoint++;
            }
            if (e.KeyCode == Keys.S)
            {
                Ycam--;
                Ypoint--;
            }
            if (e.KeyCode == Keys.A)
            {
                Xcam--;
                Xpoint--;
            }
            if (e.KeyCode == Keys.D)
            {
                Xcam++;
                Xpoint++;
            }
        }

        float xVanishingPoint =1.0f, yVanishingPoint=1.0f;

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            zoom = (double)trackBar3.Value / 1000.0;
            Zoomlabel.Text = "Текущее приближение:"+zoom.ToString();
            AnT.Focus();
        }

        public Form1()
        {
            InitializeComponent();
            AnT.InitializeContexts();
        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            vanishingPointOX = trackBar1.Value;
            Xlabel.Text =vanishingPointOX.ToString()+";0;0;";
            xVanishingPoint = 1.0f/(float)vanishingPointOX; //100 для проверки, выяснить сколько надо брать
            AnT.Focus();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            vanishingPointOY = trackBar2.Value;
            Ylabel.Text = "0;" + vanishingPointOY.ToString()+";0";
            yVanishingPoint = 1.0f/(float)vanishingPointOY;//100 для проверки, выяснить сколько надо брать
            AnT.Focus();
        }

       

        private void trackBarY_Scroll(object sender, EventArgs e)
        {
            b = (double)trackBarY.Value / 1000.0;
            labelY.Text = b.ToString();
            AnT.Focus();
        }

        private void trackBarZ_Scroll(object sender, EventArgs e)
        {
            c = (double)trackBarZ.Value / 1000.0;
            labelZ.Text = c.ToString();
            AnT.Focus();
        }

        private void trackBarX_Scroll_1(object sender, EventArgs e)
        {
             a = (double)trackBarX.Value / 1000.0;
             labelX.Text = a.ToString();
            AnT.Focus();
        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            Draw();
            if (d != 360) d++;
            else d = 0;

        }

        private void Draw()
        {  
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            float[] m = new float[] { 1, 0, 0, xVanishingPoint, 0, 1, 0, yVanishingPoint, 0, 0, 1, 0, 0, 0, 0, 1 };
            Gl.glLoadMatrixf(m);
            Gl.glOrtho(-6, 6, -6, 6, -10, 50);
            //Gl.glOrtho(-(double)AnT.Width, (double)AnT.Width, -(double)AnT.Height, (double)AnT.Height, -10, 10);
            //Glu.gluPerspective(45, (float)AnT.Width / (float)AnT.Height, 2, 200);

            Glu.gluLookAt(Xcam, Ycam, Zkam, Xpoint, Ypoint, Zpoint, 0, 1, 0);
            
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
            Gl.glRotated(d, 0, 1, 0);
            Gl.glRotated(d, 1,0, 0);
            Gl.glRotated(d, 0, 0,1);
            //Gl.glRotated(d, os_x, os_y, os_z);
            Gl.glScaled(zoom, zoom, zoom);

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
