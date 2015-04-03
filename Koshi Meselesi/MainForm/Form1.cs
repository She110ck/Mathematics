using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainForm
{
    public partial class Form1 : Form
    {
        public static int n=10;
        public const int SAY = 6;
        public static int NUM=2;
        public double a, b, u0, h;
        
        public double[] x = null; //new double[n + 1];
        public double[,] y = null; //new double[SAY, n + 1];
        public double[,] k = null;//new double[4, n + 1];
        
        public Form1()
        {
            InitializeComponent();
        }
        public void initValues() {
          //  a = 0;
           // b = 1;
           // n = 10;
          //  u0 = 0;
            n = int.Parse(textBox3.Text);
            x = new double[n + 1];
            y = new double[SAY, n + 1];
            k = new double[4, n + 1];
            a = double.Parse(textBox1.Text);
            b = double.Parse(textBox2.Text);
            
            u0 = double.Parse(textBox4.Text);
            h = (double)(b - a) / n;
            for (int i = 0; i < n+1; i++) {
                x[i] = a + i * h;
            }
            for (int i = 0; i < SAY; i++)
            {
                y[i, 0] = u0;
            }
        }

        public void deqiqHell()
        {
            double c = 3;
            //(u0  + NUM) / Math.Exp(a);
            for (int i = 0; i < n + 1; i++)
            {
                y[0, i] = c * Math.Exp(x[i]) - 2 * x[i] - NUM;
            }
        }

        public void pikar()
        {
            for (int i = 0; i < n + 1; i++)
            {
                y[1, i] = (Math.Pow(x[i], 2) / fkt(2) + Math.Pow(x[i], 3) / fkt(3) + Math.Pow(x[i], 4) / fkt(4)) * NUM + u0 + u0 * Math.Pow(x[i], 3) / fkt(3);
            
            }

        }

        public void ashkarEyler() {

            for (int i = 0; i < n; i++)
            {
                y[2, i + 1] = y[2, i] + h * func(x[i], y[2, i]);
            }
        }
        public void qeyriAshkarEyler() {

            for (int i = 0; i < n; i++)
            {
                double yd = y[3, i] + h * func(x[i], y[3, i]);
                y[3, i + 1] = y[3, i] + (h/2)*( func(x[i], y[3, i])+func(x[i+1],yd));
            }
        }
        public void adams() {
            if (radioButton1.Checked)
            {
                for (int i = 0; i < 4; i++) y[5, i] = y[2, i];
            }
            else {
                for (int i = 0; i < 4; i++) y[5, i] = y[4, i];
            }
            for (int i = 3; i < n; i++)
            { 
            y[5,i+1]= y[5,i]+(h/24)*(55*func(x[i],y[5,i])-59*func(x[i-1],y[5,i-1])+37*func(x[i-2],y[5,i-2])-9*func(x[i-3],y[5,i-3]));
            }
        
        }
        public void runqeKutta() {
            for (int i = 0; i < n; i++)
            {
                k[0, i] = func(x[i],y[4,i]);
                k[1, i] = func(x[i] + h / 2, y[4, i] + (h / 2) * k[0, i]);
                k[2, i] = func(x[i] + h / 2, y[4, i] + (h / 2) * k[1, i]);
                k[3, i] = func(x[i] + h , y[4, i] + h * k[2, i]);
                y[4,i+1]=y[4,i]+(h/6)*(k[0,i]+2*k[1,i]+2*k[2,i]+k[3,i]);
            }
        }
      

        public double func(double x, double y)
        {
        return NUM*x+y;
        }

        public double fkt(int n) {
            if (n > 1)
            {
                return n * fkt(n - 1);
            }
            else return 1;
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        { 
            label1.BackColor = Color.Green;
            label1.ForeColor = Color.LightCyan;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            label1.BackColor = Color.Beige;
            label1.ForeColor = Color.DarkBlue;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals("") || textBox4.Text.Equals(""))
            {
                MessageBox.Show("Bütün xanaları doldurun!!!");
            }
            else
            {
                initValues();
                ashkarEyler();
                qeyriAshkarEyler();
                pikar();
                deqiqHell();
                runqeKutta();
                adams();
                //  MessageBox.Show(factorial(5)+"");
                for (int i = 0; i < n + 1; i++)
                {
                    dataGridView1.Rows.Add(x[i] + "", y[0, i] + "", y[1, i] + "", y[2, i] + "", y[3, i] + "", y[4, i] + "", y[5, i] + "");
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            label7.BackColor = Color.Beige;
            label7.ForeColor = Color.DarkBlue;
        }

        private void label7_MouseMove(object sender, MouseEventArgs e)
        {
            label7.BackColor = Color.Red;
            label7.ForeColor = Color.LightCyan;
        }

       

        private void label6_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox5.Visible = !textBox5.Visible;
            if ((!textBox5.Visible) && (!textBox5.Text.Equals("")))
            {
                NUM = int.Parse(textBox5.Text);
                label6.Text = "N=" + NUM;
            }
        }

        
    }
}
