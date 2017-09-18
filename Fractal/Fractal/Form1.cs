using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Input;

namespace Fractal
{
    public partial class Form1 : Form
    {
        double pi = 3.14;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //функция зарисовки фрактала
        public void DrawFractalOfJulia(int w, int h, Graphics g, Pen pen)
        {
            // при каждой итерации, вычисляется znew = zold² + С

            // вещественная  и мнимая части постоянной C
            double cRe, cIm;
            // вещественная и мнимая части старой и новой
            double newRe, newIm, oldRe, oldIm;
            // Можно увеличивать и изменять положение
            double zoom = 1, moveX = 0, moveY = 0;
            //Определяем после какого числа итераций функция должна прекратить свою работу
            int maxIterations = 300;

            //выбираем несколько значений константы С, это определяет форму фрактала Жюлиа
            cRe = -0.70176;
            cIm = -0.3842;

            //"перебираем" каждый пиксель
            for (int x = 0; x < w; x++)
                for (int y = 0; y < h; y++)
                {
                    //вычисляется реальная и мнимая части числа z
                    //на основе расположения пикселей,масштабирования и значения позиции
                    newRe = 1.5 * (x - w / 2) / (0.5 * zoom * w) + moveX;
                    newIm = (y - h / 2) / (0.5 * zoom * h) + moveY;

                    //i представляет собой число итераций 
                    int i;

                    //начинается процесс итерации
                    for (i = 0; i < maxIterations; i++)
                    {
                        //Запоминаем значение предыдущей итерации
                        oldRe = newRe;
                        oldIm = newIm;

                        // в текущей итерации вычисляются действительная и мнимая части 
                        newRe = oldRe * oldRe - oldIm * oldIm + cRe;
                        newIm = 2 * oldRe * oldIm + cIm;

                        // если точка находится вне круга с радиусом 2 - прерываемся
                        if ((newRe * newRe + newIm * newIm) > 4) break;
                    }

                    //определяем цвета
                    pen.Color = Color.FromArgb(255, (i * 9) % 255, 0, (i * 9) % 255);
                    //рисуем пиксель
                    g.DrawRectangle(pen, x, y, 1, 1);
                }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Выбираем перо "myPen" черного цвета Black
            //толщиной в 1 пиксель:
            Pen myPen = new Pen(Color.Black, 1);
            //Объявляем объект "g" класса Graphics и предоставляем
            //ему возможность рисования на pictureBox1:
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            //вызываем функцию рисования фрактала
            DrawFractalOfJulia(pictureBox1.Width, pictureBox1.Height, g, myPen);
        }


        double l, a; //Переменные используемые для построения каждого квадрата
        const int d0 = 283;
        void DrawFractalOfSquare(double c, double b) //функция построения и отрисовки квадрата
            {
            int i; //Переменная используемая в цикле, обозначает кол-во проходов
            
            const int x0 = 200, y0 = 200; //Константа, влияющая на расстояние от
                                                    //центра квадрата до углов(Не диагональ),
                                                    //подобрана экспериментальным путем*/
                                                    //Координаты центра квадрата

            int x1, x2, x3, x4, y1, y2, y3, y4; //Координаты углов каждого квадрата
                Pen myPen = new Pen(Color.Black, 1); //Выбираем перо "myPen" черного цвета Black
                                                     //толщиной в 1 пиксель
       
                x1 = (int)(x0 + c * Math.Cos(b + 1 * Math.PI / 4));//Координаты правой нижней
                y1 = (int)(y0 + c * Math.Sin(b + 1 * Math.PI / 4));//точки
                x2 = (int)(x0 + c * Math.Cos(b + 3 * Math.PI / 4)); //Координаты левой нижней 
                y2 = (int)(y0 + c * Math.Sin(b + 3 * Math.PI / 4)); //точки
                x3 = (int)(x0 + c * Math.Cos(b + 5 * Math.PI / 4)); //Координаты левой верхней
                y3 = (int)(y0 + c * Math.Sin(b + 5 * Math.PI / 4)); //точки 
                x4 = (int)(x0 + c * Math.Cos(b + 7 * Math.PI / 4));//Координаты правой верхней
                y4 = (int)(y0 + c * Math.Sin(b + 7 * Math.PI / 4)); //точки
                                                                    //Метод строящий фигуру по 4 точкам
                g.DrawPolygon(myPen, new Point[] {
                new Point(x1,y1),new Point(x2,y2),
                new Point(x3,y3),new Point(x4,y4),
            });
        }
        //Функция  построения квадратов  с заданными параметрами по нажатию кнопки 
        private void button2_Click(object sender, EventArgs e)
        {
            l = d0;
            a = 0;
            for (int i = 1; i <= 30; i++)//Цикл, рисующий 30 квадратов
            {
                DrawFractalOfSquare(l, a); //Вызов функции построения и отрисовки квадрата
                a = a + Math.PI / 19;//изменяет угол поворота следующих квадратов
                l = l * Math.Sin(Math.PI / 3);//изменяет размер следующих квадратов
            }
        }                   

        void Draw_Pentagon(double x, double y, double r, double angle)
        {            
            Pen p;            
            Graphics gr;
            p = new Pen(Color.Orange, 2);
            gr = pictureBox1.CreateGraphics();
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int i;
            double[] x1 = new double[5];
            double[] y1 = new double[5];

            for (i = 0; i < 5; i++)
            {
                x1[i] = r * Math.Cos(angle + i * pi * 2 / 5);
                y1[i] = r * Math.Sin(angle + i * pi * 2 / 5);
            }

            for (i = 0; i < 4; i++)
            {
                gr.DrawLine(p, (int)Math.Round(x + x1[i]), (int)Math.Round(y + y1[i]), (int)Math.Round(x + x1[i + 1]), (int)Math.Round(y + y1[i + 1]));
            }
        }

        void Draw_Star(double x, double y, double r, double angle, int d)
        {
            int i;
            double h;

            h = 2 * r * Math.Cos(pi / 5);

            for (i = 0; i < 5; i++)
            {
                Draw_Pentagon(x - h * Math.Cos(angle + i * pi * 2 / 5), y - h * Math.Sin(angle + i * pi * 2 / 5), r, angle + pi + i * pi * 2 / 5);

                if (d > 0)
                    Draw_Star(x - h * Math.Cos(angle + i * pi * 2 / 5), y - h * Math.Sin(angle + i * pi * 2 / 5), r / (2 * Math.Cos(pi / 5) + 1), angle + pi + (2 * i + 1) * pi * 2 / 10, d - 1);
            }
            Draw_Pentagon(x, y, r, angle);

            if (d > 0)
                Draw_Star(x, y, r / (2 * Math.Cos(pi / 5) + 1), angle + pi, d - 1);
        }     

        private void button3_Click(object sender, EventArgs e)
        {
            SolidBrush fon;
            fon = new SolidBrush(Color.Black);
            Graphics gr = pictureBox1.CreateGraphics();
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gr.FillRectangle(fon, 0, 0, pictureBox1.Width, pictureBox1.Height);
            Draw_Star(300, 300, 85, pi / 2, 4);
        }

   
        void Draw_Levy(Graphics gr, Pen p, SolidBrush fon, int x1, int x2, int y1, int y2, int i)
        {           
            if (i == 0)
            {
                gr.DrawLine(p, x1, y1, x2, y2);
            }
            else
            {
                int x3 = (x1 + x2) / 2 + (y2 - y1) / 2;
                int y3 = (y1 + y2) / 2 - (x2 - x1) / 2;

                Draw_Levy(gr, p, fon, x1, x3, y1, y3, i - 1);
                Draw_Levy(gr, p, fon, x3, x2, y3, y2, i - 1);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Pen p = new Pen(Color.Lime, 2);
            SolidBrush fon = new SolidBrush(Color.Black);
            Graphics gr = pictureBox1.CreateGraphics();
            int i = 15;
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            gr.FillRectangle(fon, 0, 0, pictureBox1.Width, pictureBox1.Height);
            Draw_Levy(gr, p, fon, 250, 400, 160, 160, i); // Строим кривую Леви
            Draw_Levy(gr, p, fon, 400, 400, 160, 310, i); // на каждой
            Draw_Levy(gr, p, fon, 400, 250, 310, 310, i); // стороне 
            Draw_Levy(gr, p, fon, 250, 250, 310, 160, i); // квадрата
        }

        private void filToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g = null;
            pictureBox1.Image = null;
            pictureBox1.Update();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            saveFileDialog1.DefaultExt = "png";
            saveFileDialog1.Filter = "Image files (.png)|*png";
            saveFileDialog1.ShowDialog();                             
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height, g);
            bmp.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);

            try
            {
                MessageBox.Show("Файл сохранен!", "Удачное сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //source = Image.FromFile(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить файл: " + ex.Message);
            }
        }


    }
}
