using System;
using System.Drawing;

/*
    - Splines
*/

namespace my
{
    public class myObj_132 : myObject
    {
        static int max_dSize = 0;
        static int shape = 0;
        static bool isDimmable = true;

        protected int maxSize = 0, A = 0, R = 0, G = 0, B = 0, dSize = 0, dA = 0, dA_Filling = 0;

        float time, dx, dy, float_A, float_B, x1, y1, x2, y2;

        // -------------------------------------------------------------------------

        public myObj_132()
        {
            if (p == null)
            {
                p = new Pen(Color.Red);
                br = new SolidBrush(Color.Red);
                colorPicker = new myColorPicker(Width, Height, 3);
                max_dSize = rand.Next(15) + 3;

                isDimmable = rand.Next(2) == 0;

                Log($"myObj_132: colorPicker({colorPicker.getMode()}), max_dSize({max_dSize})");
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            X = rand.Next(Width);
            Y = rand.Next(Height);

            A = rand.Next(250) + 6;
A = 255;
float_A = 255.0f;
float_B = 1.0f;

            colorPicker.getColor(X, Y, ref R, ref G, ref B);
            maxSize = rand.Next(333) + 33;
            shape = rand.Next(25);

            Size = 1;
            dSize = rand.Next(max_dSize) + 1;
            dA = rand.Next(5) + 1;
dA = 1;
            dA_Filling = rand.Next(5) + 2;
            time = 0.0f;

            if (true && g != null)
                g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            Size += dSize;
            A -= dA;
            time += 0.1f;
            float_A -= 0.25f;

            int aaa = System.DateTime.Now.Millisecond;

            dx = (float)(Math.Sin(time)) * 5 * Size / 10;
            dy = (float)(Math.Cos(time)) * 5 * Size / 10;

            X += (int)dx;
            Y += (int)dy;

            move();

            if (float_A < 0)
            {
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        private void move()
        {
            //p.Color = Color.FromArgb(A, R, G, B);
            p.Color = Color.FromArgb(100, R, G, B);

            float dx2 = (float)(Math.Cos(time)) * 10;
            float dy2 = (float)(Math.Sin(time)) * 10;

            int const1 = 0;

            //g.DrawLine(p, X, Y, Width/2, 10);
            //g.DrawLine(p, Width / 2, 10, Y, X);
            //g.DrawLine(p, X/2, Y/2, Size + dx, Size + dy);
            //g.DrawLine(p, X * dx2, Y * dy2, Size * dx, Size * dy);
            //g.DrawLine(p, X - Size, Y - Size, 2 * Size + dx, 2 * Size + dy);
            //g.DrawLine(p, X, Y, 2 * Size + dx, 2 * Size + dy);

            shape = 114;

            switch (shape)
            {
                case 0:
                    x1 = 2 * Size;
                    y1 = 2 * Size - dx;
                    x2 = Size * dx;
                    y2 = Size * dy;
                    g.DrawLine(p, X, Y, x1, y1);
                    g.DrawLine(p, X, Y, x2, y2);
                    break;

                case 1:
                    x1 = Size + dx2;
                    y1 = Size + dy2;
                    x2 = X / 2;
                    y2 = Y / 2;
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawLine(p, x2, y2, x1, y1);
                    break;

                case 2:
                    x1 = Size + dx2;
                    y1 = Size + dy2;

                    x1 = Size;
                    y1 = 1 * Height / 5 + dy2 * dx / 100;

                    x2 = X / 2;
                    y2 = Y / 2;

                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawLine(p, x2, y2, x1, y1);
                    break;

                case 3:
                    x1 = Size + dx2;
                    y1 = Size + dy2;
                    x2 = Width - Size + dx2;
                    y2 = Height - Size + dy2;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);

                    x1 = Width - Size + dx2;
                    y1 = Size + dy2;
                    x2 = Size + dx2;
                    y2 = Height - Size + dy2;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 4:
                    x1 = Size + dx2 * dx / 10;
                    y1 = Size + dy2;
                    x2 = Width - Size + dx2 * dx / 10;
                    y2 = Height - Size + dy2;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);

                    x1 = Width - Size + dx2 * dx / 10;
                    y1 = Size + dy2;// * dy/10;
                    x2 = Size + dx2 * dx / 10;
                    y2 = Height - Size + dy2;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 5:
                    x1 = Size + dx2;
                    y1 = Size + dy2 * dy / 20;

                    x1 += float_B;

                    x2 = Width - x1;
                    y2 = Height - y1;

                    float_B += 1.123f;  // try changing this value

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 6:
                    x1 = Size + dx2;
                    y1 = Size + dy2 * dy / 20;

                    x1 += (float)Math.Sin(float_B) * 10;
                    y1 += (float)Math.Cos(time) * 5;

                    x2 = Width - x1;
                    y2 = Height - y1;

                    float_B += 0.23f;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 7:
                    x1 = Size + dx2;
                    y1 = Size + dy2 * dy / 20;

                    x1 += (float)Math.Sin(float_B) * 10;
                    y1 += (float)Math.Cos(time) * 5;

                    x1 += 2 * Width / 5;

                    x2 = Width - x1;
                    y2 = y1;

                    float_B += 0.23f;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 8:
                    x1 = Size + dx2;
                    y1 = Size + dy2;
                    x2 = 4 * Width  / 5 + dx2 * Size / 20;
                    y2 = 2 * Height / 5 + dy2 * Size / 20;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 9:
                    const1 = 50;

                    x1 = 1 * Width  / 5 + dx2 * Size / const1;
                    y1 = 1 * Height / 2 + dy2 * Size / const1;
                    x2 = 4 * Width  / 5 - dx2 * Size / const1;
                    y2 = 1 * Height / 2 - dy2 * Size / const1;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 10:
                    const1 = 50;

                    x1 = 1 * Width  / 5 + dx2 * Size / const1;
                    y1 = 1 * Height / 2 - dy2 * Size / const1;
                    x2 = 4 * Width  / 5 + dx2 * Size / const1;
                    y2 = 1 * Height / 2 - dy2 * Size / const1;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 11:
                    const1 = 50;

                    x1 = 1 * Width  / 5 - dy2 * Size / const1;
                    y1 = 1 * Height / 2 + dx2 * Size / const1;
                    x2 = 4 * Width  / 5 + dx2 * Size / const1;
                    y2 = 1 * Height / 2 - dy2 * Size / const1;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 12:
                    const1 = 50;

                    x1 = 1 * Width  / 5 + dx  * Size / const1;
                    y1 = 1 * Height / 2 + dy  * Size / const1;
                    x2 = 4 * Width  / 5 + dx2 * Size / const1;
                    y2 = 1 * Height / 2 - dy2 * Size / const1;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 13:
                    const1 = 50;

                    x1 = 1 * Width  / 5 + dx  * Size / const1 / const1;
                    y1 = 1 * Height / 2 + dy  * Size / const1 / const1;
                    x2 = 4 * Width  / 5 + dx2 * Size / const1;
                    y2 = 1 * Height / 2 - dy2 * Size / const1;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 14:
                    const1 = 50;

                    x1 = 1 * Width  / 5 - dx  * Size / const1 / const1;
                    y1 = 1 * Height / 2 - dy  * Size / const1 / const1;
                    x2 = 4 * Width  / 5 + dx2 * Size / const1;
                    y2 = 1 * Height / 2 - dy2 * Size / const1;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 15:
                    const1 = 50;

                    x1 = 1 * Width  / 5 + dx  * Size / const1 / const1;
                    y1 = 1 * Height / 2 - dy  * Size / const1 / const1;
                    x2 = 4 * Width  / 5 + dx2 * Size / const1;
                    y2 = 1 * Height / 2 - dy2 * Size / const1;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 16:
                    const1 = 50;

                    x1 = 1 * Width  / 5 + dx  * Size / const1 / 2;
                    y1 = 1 * Height / 2 - dy  * Size / const1 / 2;
                    x2 = 4 * Width  / 5 + dx2 * Size / const1 / 1;
                    y2 = 1 * Height / 2 - dy2 * Size / const1 / 1;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 17:
                    const1 = 50;

                    x1 = 1 * Width  / 5 + dx  * Size / const1 / 2;
                    y1 = 1 * Height / 2 - dy  * Size / const1 / 2;
                    x2 = 4 * Width  / 5 + dx2 * Size / const1 * float_B;
                    y2 = 1 * Height / 2 - dy2 * Size / const1 * float_B;

                    float_B += 0.0001f;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 18:
                    const1 = 50;

                    x1 = 1 * Width  / 5 + dx  * Size / const1 / 2;
                    y1 = 1 * Height / 2 - dy  * Size / const1 / 2;
                    x2 = 4 * Width  / 5 + dx2 * 33;
                    y2 = 1 * Height / 2 - dy2 * 33;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 19:
                    const1 = 50;
                    
                    x1 = 1 * Width  / 5 + dx  * Size / const1 / 33;
                    y1 = 1 * Height / 2 - dy  * Size / const1 / 33;
                    x2 = 4 * Width  / 5 + dx2 * 33;
                    y2 = 1 * Height / 2 - dy2 * 33;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 20:
                    const1 = 50;

                    x1 = 1 * Width  / 5 + dx  * Size / const1 / 33;
                    y1 = 1 * Height / 2 - dy  * Size / const1 / 33;
                    x2 = 4 * Width  / 5 + dx2 * 33 / float_B;
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    float_B += 0.001f;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 21:
                    x1 = 1 * Width  / 5 + dx2 * 33;
                    y1 = 1 * Height / 2 - dy2 * 33;
                    x2 = 4 * Width  / 5 + dx2 * 33;
                    y2 = 1 * Height / 2 - dy2 * 33;

                    float_B += 0.001f;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 22:
                    x1 = 1 * Width  / 5 + dy2 * 33 / float_B;
                    y1 = 1 * Height / 2 - dx2 * 33 / float_B;
                    x2 = 4 * Width  / 5 + dx2 * 33 * float_B;
                    y2 = 1 * Height / 2 - dy2 * 33 * float_B;

                    float_B += 0.01f;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 23:
                    x1 = 1 * Width  / 5 - dy2 * 33 / float_B;
                    y1 = 1 * Height / 2 - dx2 * 33 / float_B;
                    x2 = 4 * Width  / 5 + dx2 * 33 * float_B;
                    y2 = 1 * Height / 2 - dy2 * 33 * float_B;

                    float_B += 0.01f;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 24:
                    x1 = 1 * Width  / 5 + dx2 * 33 * (float)Math.Sin(float_B);
                    y1 = 1 * Height / 2 + dy2 * 33 * (float)Math.Sin(float_B);

                    x2 = 4 * Width  / 5 + dx2 * 33 * (float)Math.Sin(float_B);
                    y2 = 1 * Height / 2 + dy2 * 33 * (float)Math.Sin(float_B);

                    float_B += 0.01f;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 25:
                    x1 = 1 * Width  / 5 + dx2 * 33 * (float)Math.Sin(float_B);
                    y1 = 2 * Height / 5 + dy2 * 33 * (float)Math.Sin(float_B);

                    x2 = 4 * Width  / 5 + dy2 * 33 * (float)Math.Sin(float_B);
                    y2 = 3 * Height / 5 + dx2 * 33 * (float)Math.Sin(float_B);

                    float_B += 0.01f;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 26:
                    x1 = 1 * Width  / 5 + dx2 * 33 * (float)Math.Sin(float_B);
                    y1 = 1 * Height / 2 + dy2 * 33;

                    x2 = 4 * Width  / 5 + dy2 * 33 * (float)Math.Sin(float_B);
                    y2 = 1 * Height / 2 + dx2 * 33;

                    float_B += 0.01f;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 27:
                    x1 = 1 * Width  / 5 + dx2 * 33 * (float)Math.Sin(float_B);
                    y1 = 1 * Height / 2 + dy2 * 33 / float_B;

                    x2 = 4 * Width  / 5 + dx2 * 33 * (float)Math.Sin(float_B);
                    y2 = 1 * Height / 2 + dy2 * 33 / float_B;

                    float_B += 0.01f;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 99:
                    x1 = 1 * Width  / 5 + 600 * (float)Math.Sin(float_B);
                    y1 = 1 * Height / 2 + dy2 * 66 / float_B;

                    x2 = 4 * Width  / 5 - 600 * (float)Math.Sin(float_B);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    float_B += 0.01f;       // try changing it
                    //float_B += 0.001f;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 100:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) - 50;
                    y1 = Height / 2 + (int)(dy2 * 33 / float_B) * 10;

                    float_B += 0.01f;

                    x2 = 2 * Width  / 5 - 1500 * (float)Math.Sin(float_B);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 101:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) - 100;
                    y1 = Height / 2 + (int)(dy2 * 33 / float_B) * 10 - 10000;

                    float_B += 0.01f;

                    x2 = 6 * Width / 12 - 1500 * (float)(Math.Sin(Math.Cos(float_B) * Math.Cos(1 / float_B)) * 1.25f);
                    y2 = 3 * Height / 4 - dy2 * 33 / float_B;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 102:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) - 100;
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 10;

                    float_B = 1.0f;

                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 103:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) - 100;
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 10;

                    float_B = 1.0f;

                    x2 = 7 * Width / 12 - 1000 * (float)(Math.Sin(Math.Cos(time) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 104:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) - 100;
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 10;

                    float_B = 1.0f;

                    x2 = 7 * Width / 12 - 1000 * (float)(Math.Sin(Math.Sin(time) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 105:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) - 100;
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 10;

                    float_B = 1.0f;

                    // try different values of 2
                    x2 = 7 * Width / 12 - 1000 * (float)(Math.Sin(Math.Sin(2 * time) + Math.Cos(2 * time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 106:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) - 100;
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 10;

                    float_B = 1.0f;

                    // try different values of 2
                    x2 = 7 * Width / 12 - 1000 * (float)(Math.Sin(2.5f * Math.Sin(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 107:
                    // based on 102
                    x1 = (int)(Math.Sin(float_B) * 5.0f);
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 2; // <-- diff

                    float_B = 1.0f;

                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 108:
                    // based on 102
                    x1 = (int)(Math.Sin(float_B) * 5.0f) + 1111;
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 2; // <-- diff

                    float_B = 1.0f;

                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 109:
                    // based on 102
                    x1 = (int)(Math.Sin(float_B) * 5.0f) + 2500; // randomize this value 2500
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 2; // <-- diff

                    float_B = 1.0f;

                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 110:
                    // based on 102
                    x1 = (int)(Math.Sin(float_B) * 5.0f) + 2500; // randomize this value 2500
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 2; // <-- diff

                    float_B = 1.0f;

                    x2 = 6 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(0.2f*time) + Math.Cos(time)) * 1.0f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 111:
                    // based on 109
                    x1 = (float)(Math.Sin(time + time) * 100.0f) + 2500; // randomize this value 2500
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 2; // <-- diff

                    float_B = 1.0f;

                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkRed, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 112:
                    // based on 109
                    x1 = (float)(Math.Sin(time + 1.33f) * 100.0f) + 2500; // randomize this value 2500
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 2; // <-- diff

                    float_B = 1.0f;

                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkRed, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 113:
                    // based on 109
                    x1 = (float)(Math.Sin(time + 2.0f) * 300.0f) + 2500; // randomize this value 2500
                    y1 = 1 * Height / 2 + (float)(dy2 * 33 / float_B) * 0.5f; // <-- diff

                    float_B = 1.0f;

                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkRed, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 114:
                    // based on 109
                    x1 = (float)(Math.Sin(time + 2.0f) * 1500.0f) + 2000; // randomize this value 2500
                    y1 = 1 * Height / 2 + (float)(dy2 * 33 / float_B) * 1.5f; // <-- diff

                    float_B = 1.0f;

                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkRed, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                default:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) - 100;
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 10;

                    float_B += 0.00001f;

                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B) + Math.Cos(time * float_B)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;
            }
#if false
// turn this 90 degrees
                case 1021:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) - 100;
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 10;

                    float_B += 0.0f;

                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
#endif

        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int t = 33, cnt = 0;
            int num = rand.Next(333) + 33;
            t -= isDimmable ? 13 : 0;

            num = 1;

            var list = new System.Collections.Generic.List<myObj_132>();

            list.Add(new myObj_132());

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            br.Color = Color.FromArgb(3, 0, 0, 0);

            while (isAlive)
            {
                foreach (var s in list)
                {
                    s.Show();
                    s.Move();
                }

                if (++cnt % 5 == 0 && isDimmable)
                {
                    g.FillRectangle(br, 0, 0, Width, Height);
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);

                // Gradually increase the number of objects
                if (list.Count < num)
                {
                    list.Add(new myObj_132());
                }
            }

            return;
        }
    }
};
