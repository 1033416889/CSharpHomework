using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;

namespace CSharpHomeworkProject2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetProcess();
            if (graphics == null) graphics = this.CreateGraphics();
            else
            {
                graphics.Clear(this.BackColor);
            }
            drawCayleyTree(10, 300, 410, 100, -Math.PI / 2);
        }

        private Graphics graphics;
        double th1 = 30 * Math.PI / 180;
        double th2 = 30 * Math.PI / 180;
        double per1 = 0.6;
        double per2 = 0.7;
        double k = 0.6;
        Pen myPen = new Pen(Color.Blue, 1);

        void drawCayleyTree(int n, double x0,double y0,double leng, double th)
        {
            if (n == 0) return;
            double x1 = x0 + leng * Math.Cos(th);
            double y1 = y0 + leng * Math.Sin(th);
            double x2 = x0 + leng * Math.Cos(th) * k;
            double y2 = y0 + leng * Math.Sin(th) * k;

            drawLine(x0, y0, x1, y1);

            drawCayleyTree(n - 1, x1, y1, per1 * leng, th + th1);
            drawCayleyTree(n - 1, x2, y2, per2 * leng, th - th2);
        }

        void drawLine(double x0,double y0, double x1,double y1)
        {
            graphics.DrawLine(
                myPen,
                (int)x0, (int)y0, (int)x1, (int)y1);
        }

        void reDraw()
        {
            if (checkBox1.Checked)
            {
                if (graphics == null) graphics = this.CreateGraphics();
                graphics.Clear(this.BackColor);
                drawCayleyTree(10, 300, 410, 100, -Math.PI / 2);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            TrackBar obj = (TrackBar)sender;
            textBox3.Text = obj.Value.ToString();

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            TrackBar obj = (TrackBar)sender;
            textBox2.Text = obj.Value.ToString();

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            TextBox obj = (TextBox)sender;
            if (obj.Text != "")
            {
                if (int.Parse(obj.Text) == 0) obj.Text = "0";
                if (int.Parse(obj.Text) > 90)
                {
                    trackBar1.Value = 90;
                    textBox3.Text = "90";
                }
                else
                    trackBar1.Value = int.Parse(obj.Text);
            }
            else obj.Text = "0";
            th1 = int.Parse(obj.Text)*Math.PI/180;
            reDraw();

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            TextBox obj = (TextBox)sender;
            if (obj.Text != "")
            {
                if (int.Parse(obj.Text) == 0) obj.Text = "0";
                if (int.Parse(obj.Text) > 90)
                {
                    trackBar2.Value = 90;
                    textBox2.Text = "90";
                }
                else
                    trackBar2.Value = int.Parse(obj.Text);
            }
            else obj.Text = "0";
            th2 = int.Parse(obj.Text)*Math.PI/180;

            reDraw();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int ires))
            {
                if (ires == 0)
                    textBox1.Text = "0.00";
                else
                    textBox1.Text = "1.00";
                textBox1.Select(4, 0);
                reDraw();
                return;
            }
            if (!float.TryParse(textBox1.Text, out float res)||res>1)
            {
                textBox1.Text = "0.";
                textBox1.Select(2, 0);
            }
            else
            {
                trackBar3.Value = (int)(res * 100);
                reDraw();
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if(int.TryParse(textBox4.Text,out int ires))
            {
                if (ires == 0)
                    textBox4.Text = "0.00";
                else
                    textBox4.Text = "1.00";
                textBox4.Select(4, 0);
                reDraw();
                return;
            }
            if (!float.TryParse(textBox4.Text, out float res)||res>1)
            {
                textBox4.Text = "0.";
                textBox4.Select(2, 0);
            }
            else
            {
                trackBar4.Value = (int)(res * 100);
                reDraw();
            }
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            if (trackBar3.Value == 0)
                textBox1.Text = "0.00";
            else
                textBox1.Text = ((trackBar3.Value) / 100.0).ToString();

            per2 = trackBar3.Value / 100.0;
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            if (trackBar4.Value == 0)
                textBox4.Text = "0.00";
            else
                textBox4.Text = ((trackBar4.Value) / 100.0).ToString();

            per1 = trackBar4.Value / 100.0;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox5.Text, out int ires))
            {
                if (ires == 0)
                    textBox5.Text = "0.00";
                else
                    textBox5.Text = "1.00";
                textBox5.Select(4, 0);
                reDraw();
                return;
            }
            if (!float.TryParse(textBox5.Text, out float res) || res > 1)
            {
                textBox5.Text = "0.";
                textBox5.Select(2, 0);
            }
            else
            {
                trackBar5.Value = (int)(res * 100);
                reDraw();
            }
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            if (trackBar5.Value == 0)
                textBox5.Text = "0.00";
            else
                textBox5.Text = ((trackBar5.Value) / 100.0).ToString();

            k = trackBar5.Value / 100.0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                myPen.Color = colorDialog1.Color;
            }

            reDraw();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            TextBox obj = (TextBox)sender;
            if (obj.Text != "")
            {
                if (int.Parse(obj.Text) == 0) obj.Text = "1";
                if (int.Parse(obj.Text) > 5)
                {
                    trackBar6.Value = 5;
                    obj.Text = "5";
                }
                else
                    trackBar6.Value = int.Parse(obj.Text);
            }
            else obj.Text = "1";

            myPen.Width = int.Parse(obj.Text);

            reDraw();
        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            TrackBar obj = (TrackBar)sender;
            textBox6.Text = obj.Value.ToString();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
