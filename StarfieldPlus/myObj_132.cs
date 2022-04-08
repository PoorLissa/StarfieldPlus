using System;
using System.Drawing;
using System.Collections.Generic;
/*
    - Splines
*/

namespace my
{
    public class myObj_132 : myObject
    {
        static int max_dSize = 0;
        static int t = 0, tDefault = 0, shape = 0, x0 = 0, y0 = 0, si1 = 0;
        static bool isDimmable = true, needNewScreen = false;
        static float sf1 = 0, sf2 = 0, sf3 = 0, sf4 = 0, sf5 = 0, sf6 = 0, sf7 = 0, sf8 = 0;
        static float a = 0, b = 0, c = 0;
        static float fLifeCnt = 0, fdLifeCnt = 0;
        static List<myObject> list = null;

        protected int maxSize = 0, A = 0, R = 0, G = 0, B = 0, dSize = 0, dA = 0, dA_Filling = 0;

        float time, time2, dt2, dx, dy, float_B, x1, y1, x2, y2, x3, y3, x4, y4;

        // -------------------------------------------------------------------------

        public myObj_132()
        {
            if (p == null)
            {
                p = new Pen(Color.Red);
                br = new SolidBrush(Color.Red);
                colorPicker = new myColorPicker(Width, Height, 3);
                list = new List<myObject>();
                max_dSize = rand.Next(15) + 3;

                isDimmable = rand.Next(2) == 0;

                tDefault = 33;

                Log($"myObj_132: colorPicker({colorPicker.getMode()}), max_dSize({max_dSize})");
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            fLifeCnt = 255.0f;
            fdLifeCnt = 0.25f;

            X = rand.Next(Width);
            Y = rand.Next(Height);

            A = rand.Next(250) + 6;
A = 255;
float_B = 1.0f;

            colorPicker.getColor(X, Y, ref R, ref G, ref B);
            p.Color = Color.FromArgb(100, R, G, B);
            maxSize = rand.Next(333) + 33;
            shape = rand.Next(79);
            isDimmable = rand.Next(2) == 0;

            t = tDefault;
            t -= isDimmable ? 13 : 0;

shape = 1300;
shape = 79;

            Size = 1;
            dSize = rand.Next(max_dSize) + 1;
            dA = rand.Next(5) + 1;
dA = 1;
            dA_Filling = rand.Next(5) + 2;
            time = 0.0f;
            time2 = 0.0f;
            dt2 = 0.01f;

            needNewScreen = true;

            changeConstants();

            return;
        }

        // -------------------------------------------------------------------------

        private void changeConstants()
        {
            switch (shape)
            {
                case 0:
                case 1:
                    fdLifeCnt = 0.5f;
                    break;

                case 44:
                    sf1 = rand.Next(333) + 100;
                    sf2 = rand.Next(10) + 10;
                    sf3 = rand.Next(100) * 0.25f;
                    fdLifeCnt = 0.5f;
                    break;

                case 45:
                case 46:
                case 47:
                    sf1 = rand.Next(333) + 100;
                    sf2 = rand.Next(333) + 10;
                    sf3 = rand.Next(100) * 0.25f;
                    fdLifeCnt = 0.5f;
                    break;

                case 48:
                    sf1 = rand.Next(333) + 100;
                    sf2 = rand.Next(333) + 100;
                    sf3 = rand.Next(1000) * 0.05f;
                    sf4 = rand.Next(1000) * 0.05f;
                    fdLifeCnt = 0.5f;
                    break;

                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                case 60:
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                case 70:
                case 71:
                case 72:
                case 73:
                case 74:
                case 75:
                case 76:
                    constSetUp1();
                    t = 11;
                    break;

                case 77:
                    constSetUp2();
                    t = 11;
                    break;

                case 78:
                    constSetUp3();
                    t = 11;
                    break;

                case 79:
                    constSetUp4();
                    t = 3;
                    break;

                case 1300:
                    constSetUp1();
                    t = 3;      // tmp, remove later
                    break;
            }
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            int tNow = System.DateTime.Now.Millisecond;

            Size += dSize;
            A -= dA;
            time += 0.1f;
            time2 += dt2;

            dx = (float)(Math.Sin(time)) * 5 * Size / 10;
            dy = (float)(Math.Cos(time)) * 5 * Size / 10;

            X += (int)dx;
            Y += (int)dy;

            move_0();

            if ((fLifeCnt -= fdLifeCnt) < 0)
            {
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        private void move_0()
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

            switch (shape)
            {
                case 0:
                    x1 = 2 * Size;
                    y1 = 2 * Size - dx;
                    x2 = Size * dx;
                    y2 = Size * dy;
                    break;

                case 1:
                    x1 = Size + dx2;
                    y1 = Size + dy2;
                    x2 = X / 2;
                    y2 = Y / 2;
                    break;

                case 2:
                    x1 = Size + dx2;
                    y1 = Size + dy2;

                    x1 = Size;
                    y1 = 1 * Height / 5 + dy2 * dx / 100;

                    x2 = X / 2;
                    y2 = Y / 2;
                    break;

                case 3:
                    x1 = Size + dx2;
                    y1 = Size + dy2;
                    x2 = Width - Size + dx2;
                    y2 = Height - Size + dy2;

                    x3 = Width - Size + dx2;
                    y3 = Size + dy2;
                    x4 = Size + dx2;
                    y4 = Height - Size + dy2;
                    break;

                case 4:
                    x1 = Size + dx2 * dx / 10;
                    y1 = Size + dy2;
                    x2 = Width - Size + dx2 * dx / 10;
                    y2 = Height - Size + dy2;

                    x3 = Width - Size + dx2 * dx / 10;
                    y3 = Size + dy2;// * dy/10;
                    x4 = Size + dx2 * dx / 10;
                    y4 = Height - Size + dy2;
                    break;

                case 5:
                    x1 = Size + dx2;
                    y1 = Size + dy2 * dy / 20;

                    x1 += float_B;

                    x2 = Width - x1;
                    y2 = Height - y1;

                    float_B += 1.123f;  // try changing this value
                    break;

                case 6:
                    x1 = Size + dx2;
                    y1 = Size + dy2 * dy / 20;

                    x1 += (float)Math.Sin(float_B) * 10;
                    y1 += (float)Math.Cos(time) * 5;

                    x2 = Width - x1;
                    y2 = Height - y1;

                    float_B += 0.23f;
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
                    break;

                case 8:
                    x1 = Size + dx2;
                    y1 = Size + dy2;
                    x2 = 4 * Width  / 5 + dx2 * Size / 20;
                    y2 = 2 * Height / 5 + dy2 * Size / 20;
                    break;

                case 9:
                    const1 = 50;

                    x1 = 1 * Width  / 5 + dx2 * Size / const1;
                    y1 = 1 * Height / 2 + dy2 * Size / const1;
                    x2 = 4 * Width  / 5 - dx2 * Size / const1;
                    y2 = 1 * Height / 2 - dy2 * Size / const1;
                    break;

                case 10:
                    const1 = 50;

                    x1 = 1 * Width  / 5 + dx2 * Size / const1;
                    y1 = 1 * Height / 2 - dy2 * Size / const1;
                    x2 = 4 * Width  / 5 + dx2 * Size / const1;
                    y2 = 1 * Height / 2 - dy2 * Size / const1;
                    break;

                case 11:
                    const1 = 50;

                    x1 = 1 * Width  / 5 - dy2 * Size / const1;
                    y1 = 1 * Height / 2 + dx2 * Size / const1;
                    x2 = 4 * Width  / 5 + dx2 * Size / const1;
                    y2 = 1 * Height / 2 - dy2 * Size / const1;
                    break;

                case 12:
                    const1 = 50;

                    x1 = 1 * Width  / 5 + dx  * Size / const1;
                    y1 = 1 * Height / 2 + dy  * Size / const1;
                    x2 = 4 * Width  / 5 + dx2 * Size / const1;
                    y2 = 1 * Height / 2 - dy2 * Size / const1;
                    break;

                case 13:
                    const1 = 50;

                    x1 = 1 * Width  / 5 + dx  * Size / const1 / const1;
                    y1 = 1 * Height / 2 + dy  * Size / const1 / const1;
                    x2 = 4 * Width  / 5 + dx2 * Size / const1;
                    y2 = 1 * Height / 2 - dy2 * Size / const1;
                    break;

                case 14:
                    const1 = 50;

                    x1 = 1 * Width  / 5 - dx  * Size / const1 / const1;
                    y1 = 1 * Height / 2 - dy  * Size / const1 / const1;
                    x2 = 4 * Width  / 5 + dx2 * Size / const1;
                    y2 = 1 * Height / 2 - dy2 * Size / const1;
                    break;

                case 15:
                    const1 = 50;

                    x1 = 1 * Width  / 5 + dx  * Size / const1 / const1;
                    y1 = 1 * Height / 2 - dy  * Size / const1 / const1;
                    x2 = 4 * Width  / 5 + dx2 * Size / const1;
                    y2 = 1 * Height / 2 - dy2 * Size / const1;
                    break;

                case 16:
                    const1 = 50;

                    x1 = 1 * Width  / 5 + dx  * Size / const1 / 2;
                    y1 = 1 * Height / 2 - dy  * Size / const1 / 2;
                    x2 = 4 * Width  / 5 + dx2 * Size / const1 / 1;
                    y2 = 1 * Height / 2 - dy2 * Size / const1 / 1;
                    break;

                case 17:
                    const1 = 50;

                    x1 = 1 * Width  / 5 + dx  * Size / const1 / 2;
                    y1 = 1 * Height / 2 - dy  * Size / const1 / 2;
                    x2 = 4 * Width  / 5 + dx2 * Size / const1 * float_B;
                    y2 = 1 * Height / 2 - dy2 * Size / const1 * float_B;

                    float_B += 0.0001f;
                    break;

                case 18:
                    const1 = 50;

                    x1 = 1 * Width  / 5 + dx  * Size / const1 / 2;
                    y1 = 1 * Height / 2 - dy  * Size / const1 / 2;
                    x2 = 4 * Width  / 5 + dx2 * 33;
                    y2 = 1 * Height / 2 - dy2 * 33;
                    break;

                case 19:
                    const1 = 50;
                    
                    x1 = 1 * Width  / 5 + dx  * Size / const1 / 33;
                    y1 = 1 * Height / 2 - dy  * Size / const1 / 33;
                    x2 = 4 * Width  / 5 + dx2 * 33;
                    y2 = 1 * Height / 2 - dy2 * 33;
                    break;

                case 20:
                    const1 = 50;

                    x1 = 1 * Width  / 5 + dx  * Size / const1 / 33;
                    y1 = 1 * Height / 2 - dy  * Size / const1 / 33;
                    x2 = 4 * Width  / 5 + dx2 * 33 / float_B;
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    float_B += 0.001f;
                    break;

                case 21:
                    x1 = 1 * Width  / 5 + dx2 * 33;
                    y1 = 1 * Height / 2 - dy2 * 33;
                    x2 = 4 * Width  / 5 + dx2 * 33;
                    y2 = 1 * Height / 2 - dy2 * 33;

                    float_B += 0.001f;
                    break;

                case 22:
                    x1 = 1 * Width  / 5 + dy2 * 33 / float_B;
                    y1 = 1 * Height / 2 - dx2 * 33 / float_B;
                    x2 = 4 * Width  / 5 + dx2 * 33 * float_B;
                    y2 = 1 * Height / 2 - dy2 * 33 * float_B;

                    float_B += 0.01f;
                    break;

                case 23:
                    x1 = 1 * Width  / 5 - dy2 * 33 / float_B;
                    y1 = 1 * Height / 2 - dx2 * 33 / float_B;
                    x2 = 4 * Width  / 5 + dx2 * 33 * float_B;
                    y2 = 1 * Height / 2 - dy2 * 33 * float_B;

                    float_B += 0.01f;
                    break;

                case 24:
                    x1 = 1 * Width  / 5 + dx2 * 33 * (float)Math.Sin(float_B);
                    y1 = 1 * Height / 2 + dy2 * 33 * (float)Math.Sin(float_B);

                    x2 = 4 * Width  / 5 + dx2 * 33 * (float)Math.Sin(float_B);
                    y2 = 1 * Height / 2 + dy2 * 33 * (float)Math.Sin(float_B);

                    float_B += 0.01f;
                    break;

                case 25:
                    x1 = 1 * Width  / 5 + dx2 * 33 * (float)Math.Sin(float_B);
                    y1 = 2 * Height / 5 + dy2 * 33 * (float)Math.Sin(float_B);

                    x2 = 4 * Width  / 5 + dy2 * 33 * (float)Math.Sin(float_B);
                    y2 = 3 * Height / 5 + dx2 * 33 * (float)Math.Sin(float_B);

                    float_B += 0.01f;
                    break;

                case 26:
                    x1 = 1 * Width  / 5 + dx2 * 33 * (float)Math.Sin(float_B);
                    y1 = 1 * Height / 2 + dy2 * 33;

                    x2 = 4 * Width  / 5 + dy2 * 33 * (float)Math.Sin(float_B);
                    y2 = 1 * Height / 2 + dx2 * 33;

                    float_B += 0.01f;
                    break;

                case 27:
                    x1 = 1 * Width  / 5 + dx2 * 33 * (float)Math.Sin(float_B);
                    y1 = 1 * Height / 2 + dy2 * 33 / float_B;

                    x2 = 4 * Width  / 5 + dx2 * 33 * (float)Math.Sin(float_B);
                    y2 = 1 * Height / 2 + dy2 * 33 / float_B;

                    float_B += 0.01f;
                    break;

                case 28:
                    x1 = 1 * Width  / 5 + 600 * (float)Math.Sin(float_B);
                    y1 = 1 * Height / 2 + dy2 * 66 / float_B;

                    x2 = 4 * Width  / 5 - 600 * (float)Math.Sin(float_B);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                    float_B += 0.01f;       // try changing it
                    //float_B += 0.001f;
                    break;

                case 29:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) - 50;
                    y1 = Height / 2 + (int)(dy2 * 33 / float_B) * 10;

                    float_B += 0.01f;

                    x2 = 2 * Width  / 5 - 1500 * (float)Math.Sin(float_B);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;
                    break;

                case 30:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) - 100;
                    y1 = Height / 2 + (int)(dy2 * 33 / float_B) * 10 - 10000;

                    float_B += 0.01f;

                    x2 = 6 * Width / 12 - 1500 * (float)(Math.Sin(Math.Cos(float_B) * Math.Cos(1 / float_B)) * 1.25f);
                    y2 = 3 * Height / 4 - dy2 * 33 / float_B;
                    break;

                case 31:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) - 100;
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 10;

                    float_B = 1.0f;

                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;
                    break;

                case 32:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) - 100;
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 10;

                    float_B = 1.0f;

                    x2 = 7 * Width / 12 - 1000 * (float)(Math.Sin(Math.Cos(time) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;
                    break;

                case 33:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) - 100;
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 10;

                    float_B = 1.0f;

                    x2 = 7 * Width / 12 - 1000 * (float)(Math.Sin(Math.Sin(time) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;
                    break;

                case 34:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) - 100;
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 10;

                    float_B = 1.0f;

                    // try different values of 2
                    x2 = 7 * Width / 12 - 1000 * (float)(Math.Sin(Math.Sin(2 * time) + Math.Cos(2 * time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;
                    break;

                case 35:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) - 100;
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 10;

                    float_B = 1.0f;

                    // try different values of 2
                    x2 = 7 * Width / 12 - 1000 * (float)(Math.Sin(2.5f * Math.Sin(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;
                    break;

                case 36:
                    x1 = (int)(Math.Sin(float_B) * 5.0f);
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 2; // <-- diff

                    float_B = 1.0f;

                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;
                    break;

                case 37:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) + 1111;
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 2; // <-- diff

                    float_B = 1.0f;

                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;
                    break;

                case 38:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) + 2500; // randomize this value 2500
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 2; // <-- diff

                    float_B = 1.0f;

                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;
                    break;

                case 39:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) + 2500; // randomize this value 2500
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 2; // <-- diff

                    float_B = 1.0f;

                    x2 = 6 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(0.2f*time) + Math.Cos(time)) * 1.0f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;
                    break;

                case 40:
                    x1 = (float)(Math.Sin(time + time) * 100.0f) + 2500; // randomize this value 2500
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 2; // <-- diff

                    float_B = 1.0f;

                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;
                    break;

                case 41:
                    x1 = (float)(Math.Sin(time + 1.33f) * 100.0f) + 2500; // randomize this value 2500
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 2; // <-- diff

                    float_B = 1.0f;

                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;
                    break;

                case 42:
                    x1 = (float)(Math.Sin(time + 2.0f) * 300.0f) + 2500; // randomize this value 2500
                    y1 = 1 * Height / 2 + (float)(dy2 * 33 / float_B) * 0.5f; // <-- diff

                    float_B = 1.0f;

                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;
                    break;

                case 43:
                    x1 = (float)(Math.Sin(time + 2.0f) * 1500.0f) + 2000; // randomize this value 2500
                    y1 = 1 * Height / 2 + (float)(dy2 * 33 / float_B) * 1.5f; // <-- diff

                    float_B = 1.0f;

                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;
                    break;

                case 44:
/*
                    x1 = 1 * Width  / 5 + (float)(Math.Sin(time)) * sf1;
                    y1 = 1 * Height / 2 + (float)(Math.Cos(time)) * sf1;

                    x2 = 4 * Width  / 5 + (float)(Math.Cos(time)) * sf2;
                    y2 = 1 * Height / 2 + (float)(Math.Sin(time)) * sf2;
*/
                    float_B = 1.0f;

                    // Changing shape
                    x1 = sf3 * (float)(Math.Sin(time + sf2) * sf1) + sf1 * 10;
                    y1 = 1 * Height / 2 + sf3 * (float)(dy2 * 33 / float_B) * 1;

                    // Static oval shape
                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B) + Math.Cos(time)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;
                    break;

                case 45:
                    x1 = 1 * Width  / 5 + (float)(Math.Sin(time)) * sf1;
                    y1 = 1 * Height / 2 + (float)(Math.Cos(time)) * sf1;

                    x2 = 4 * Width  / 5 + (float)(Math.Cos(time)) * sf2;
                    y2 = 1 * Height / 2 + (float)(Math.Sin(time)) * sf2;
                    break;

                case 46:
                    x1 = 1 * Width  / 5 + (float)(Math.Sin(time)) * sf1;
                    y1 = 1 * Height / 2 + (float)(Math.Cos(time)) * sf1;

                    x2 = 4 * Width  / 5 + (float)(Math.Cos(fLifeCnt + time)) * sf2;
                    y2 = 1 * Height / 2 + (float)(Math.Sin(fLifeCnt)) * sf2;
                    break;

                case 47:
                    x1 = 1 * Width  / 5 + (float)(Math.Sin(time)) * sf1;
                    y1 = 1 * Height / 2 + (float)(Math.Cos(time)) * sf1;

                    x2 = 4 * Width  / 5 + (float)(Math.Cos(fLifeCnt + time)) * sf2;
                    y2 = 1 * Height / 2 + (float)(Math.Sin(fLifeCnt + time)) * sf2;
                    break;

                case 48:
                    x1 = 1 * Width  / 5 + (int)(Math.Sin(time * 1.0f) * sf3) / sf3 * sf1;
                    y1 = 1 * Height / 2 + (int)(Math.Cos(time * 1.0f) * sf3) / sf3 * sf1;

                    x2 = 4 * Width  / 5 + (int)(Math.Sin(time * 1.1f) * sf4) / sf4 * 333;
                    y2 = 1 * Height / 2 + (int)(Math.Cos(time * 1.1f) * sf4) / sf4 * 333;
                    break;

                // Cool one -- ok
                case 49:
                    x1 = x0 + (float)(Math.Sin(time * sf3)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf3)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 50:
                    x1 = x0 + (float)(Math.Sin(time * sf3)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf3)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;

                    sf1 += a;
                    sf2 += b;
                    break;

                case 51:
                    x1 = x0 + (float)(Math.Sin(time + sf3)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time + sf3)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time + sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time + sf4)) * sf2;

                    sf3 += a;
                    sf4 += b;
                    break;

                case 52:
                    x1 = x0 + (float)(Math.Sin(time * sf3)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf4)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf3)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;

                    sf3 += a;
                    sf4 += b;
                    break;

                case 53:
                    x1 = x0 + (float)(Math.Sin(time * sf3)) * sf1 + (float)(Math.Cos(time + sf3)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf3)) * sf1 + (float)(Math.Sin(time + sf3)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 54:
                    x1 = x0 + (float)(Math.Sin(time * sf3)) * sf1 + (float)(Math.Sin(time / si1)) * sf1 / si1;
                    y1 = y0 + (float)(Math.Cos(time * sf3)) * sf1 + (float)(Math.Cos(time / si1)) * sf1 / si1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 55:
                    sf1 /= sf1 > 1000 ? 2 : 1;

                    x1 = x0 + (float)(Math.Sin(time * sf3)) * sf1 + (float)(Math.Sin(time * si1)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf3)) * sf1 + (float)(Math.Cos(time * si1)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 56:
                    x1 = x0 + (float)(Math.Sin(time * sf3)) * sf1 * (float)(Math.Sin(time * si1)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf3)) * sf1 * (float)(Math.Cos(time * si1)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 57:
                    x1 = (float)(Math.Sin(time * sf3)) * (float)(Math.Sin(time * si1)) * sf1 * 2;
                    y1 = (float)(Math.Cos(time * sf3)) * (float)(Math.Cos(time * si1)) * sf1 * 2;

                    x1 /= si1;
                    y1 /= si1;
                    x1 += x0;
                    y1 += y0;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 58:
                    x1 = x0 + (float)(Math.Sin(time * sf3)) * (float)(Math.Cos(time * si1)) * sf1 / 2;
                    y1 = y0 + (float)(Math.Cos(time * sf3)) * (float)(Math.Sin(time * si1)) * sf1 / 2;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 59:
                    x1 = x0 + (float)(Math.Sin(time * sf3) * Math.Sin(1 + time / si1)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf3) * Math.Sin(1 + time / si1)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 60:
                    x1 = x0 + (float)(Math.Sin(time * sf3) * Math.Sin(si1 + time / si1)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf3) * Math.Sin(si1 + time / si1)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 61:
                    x1 = x0 + (float)(Math.Sin(time * sf3) + Math.Sin(time / si1 / si1)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf3) + Math.Cos(time / si1 / si1)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 62:
                    x1 = x0 + (int)(Math.Sin(a * time) * sf1);
                    y1 = y0 + (int)(Math.Cos(b * time) * sf1);

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 63:
                    sf1 /= sf1 > 1000 ? 2 : 1;
                    sf2 /= sf1 > 1000 ? 2 : 1;

                    x1 = x0 + (int)(Math.Sin(a * time) * sf1);
                    y1 = y0 + (int)(Math.Cos(b * time) * sf1);

                    x2 = x0 + (float)(Math.Sin(time * sf4 * a)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4 * b)) * sf2;
                    break;

                case 64:
                    sf1 /= sf1 > 1000 ? 2 : 1;
                    sf2 /= sf1 > 1000 ? 2 : 1;

                    x1 = x0 + (float)(Math.Sin(a * time) * sf1);
                    y1 = y0 + (float)(Math.Cos(b * time) * sf1);

                    x2 = x0 + (float)(Math.Sin(time * sf4 * a)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4 * b)) * sf2;
                    break;

                case 65:
                    x1 = x0 + (float)(Math.Sin(a * time) * Math.Sin(b * time) * sf1);
                    y1 = y0 + (float)(Math.Cos(a * time) * Math.Cos(b * time) * sf1);

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 66:
                    x1 = x0 + (float)((Math.Sin(a * time2) + Math.Cos(b * time2)) * sf1);
                    y1 = y0 + (float)((Math.Cos(a * time2) + Math.Sin(b * time2)) * sf1);

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                // probably is the same as 137
                case 67:
                    x1 = x0 + (float)((Math.Sin(a * time * sf3) + Math.Cos(b * time * sf3)) * sf1);
                    y1 = y0 + (float)((Math.Cos(a * time * sf3) + Math.Sin(b * time * sf3)) * sf1);

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 68:
                    x1 = x0 + (int)((Math.Sin(a * time * sf3) + Math.Cos(b * time * sf3))) * sf1;
                    y1 = y0 + (int)((Math.Cos(a * time * sf3) + Math.Sin(b * time * sf3))) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 69:
                    x1 = x0 + (int)((Math.Sin(a * time * sf3) + Math.Cos(b * time * sf3)) * a) * sf1;
                    y1 = y0 + (int)((Math.Cos(a * time * sf3) + Math.Sin(b * time * sf3)) * a) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 70:
                    x1 = x0 + (int)((Math.Sin(a * time * sf3) + Math.Cos(b * time * sf3)) * c) * sf1;
                    y1 = y0 + (int)((Math.Cos(a * time * sf3) + Math.Sin(b * time * sf3)) * c) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 71:
                    x1 = x0 + (int)((Math.Sin(a * time * sf3) * Math.Cos(b * time * sf3)) * c) * sf1;
                    y1 = y0 + (int)((Math.Cos(a * time * sf3) * Math.Sin(b * time * sf3)) * c) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 72:
                    x1 = x0 + (int)(Math.Sin(time * sf3) * c) * sf1;
                    y1 = y0 + (int)(Math.Cos(time * sf3) * c) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 73:
                    x1 = x0 + (int)(Math.Sin(time * sf3 / 10) * c) * sf1;
                    y1 = y0 + (int)(Math.Cos(time * sf3 / 10) * c) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 74:
                    x1 = x0 + (float)(Math.Sin(time * sf3) * c) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf3) * c) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 75:
                    x1 = x0 + (int)(Math.Sin(time * sf3) * 10) / 10 * sf1;
                    y1 = y0 + (int)(Math.Cos(time * sf3) * 10) / 10 * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 76:
                    x1 = x0 + (int)(Math.Sin(time * sf3) * si1) * sf1 / (1.0f * si1);
                    y1 = y0 + (int)(Math.Cos(time * sf3) * si1) * sf1 / (1.0f * si1);

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                // Various shapes vs a circle
                // The shapes are of a single family, so we'll have a separate setup func for these
                case 77:
                    x1 = x0 + ((float)(Math.Sin(time * sf3 + c) * sf1) + (float)(Math.Sin(time + sf3) * (sf1 / 2)));
                    y1 = y0 + ((float)(Math.Cos(time * sf3 - c) * sf1) + (float)(Math.Cos(time + sf3) * (sf1 / 2)));

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                // The same as the one before, but with more params at play
                case 78:
                    x1 = x0 + ((float)(Math.Sin(time * sf3 + c) * sf1) + (float)(Math.Sin(time + sf3) * (sf1 / a)));
                    y1 = y0 + ((float)(Math.Cos(time * sf3 - c) * sf1) + (float)(Math.Cos(time + sf3) * (sf1 / a)));

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 79:
                    // 2. water drop

                    sf1 = 150;

                    // sf5 = 2.0f                           -- beaker                                       // done
                    // sf6 = 2.0f                           -- beaker                                       // done ?..
                    // sf5 = 2.0f && sf6 = 2.0f             -- eight
                    // sf7 = 2.0f                           -- smile                                        // done
                    // sf8 = 2.0f                           -- smile                                        // done
                    // sf7 = 2.0f && sf8 = 2.0f             -- parabola 1 - try with diff params
                    // sf5 = 2.0f && sf7 = 2.0f             -- parabola 2 - try with diff params
                    // sf6 = 2.0f && sf7 = 2.0f             -- apple / clover
                    // sf6 = 2.0f && sf8 = 2.0f             -- apple / clover
                    // sf5 = 2.0f && sf8 = 2.0f             -- apple / clover
                    // sf5 = 2.0f && sf7 = 2.0f             -- apple / clover

                    x1 = x0 + ((float)(Math.Sin(sf5 * time * sf3) * sf1) + (float)(Math.Sin(sf6 * time * sf3) * sf1 * a));
                    y1 = y0 + ((float)(Math.Cos(sf7 * time * sf3) * sf1) + (float)(Math.Cos(sf8 * time * sf3) * sf1 * b));

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 1300:

                    //sf1 = 300;

                    sf2 *= (sf2 > 333) ? 1 : 2;
                    sf1 = sf2 / 2;

                    sf3 = 0.5f;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 1251:

                    sf1 = 333;
                    sf2 = 666;

                    sf3 = 1.25f;
                    sf4 = 1.5f;

                    x1 = Width / 2 + (float)(Math.Sin(time * sf3)) * sf1 + (float)(Math.Cos(time * sf4)) * sf1;
                    y1 = Height / 2 + (float)(Math.Cos(time * sf3)) * sf1;

                    x2 = Width / 2 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = Height / 2 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                default:
                    x1 = (int)(Math.Sin(float_B) * 5.0f) - 100;
                    y1 = 1 * Height / 2 + (int)(dy2 * 33 / float_B) * 10;

                    float_B += 0.00001f;

                    x2 = 8 * Width / 12 - 1300 * (float)(Math.Sin(Math.Cos(float_B) + Math.Cos(time * float_B)) * 1.25f);
                    y2 = 1 * Height / 2 - dy2 * 33 / float_B;
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
            switch (shape)
            {
                case 0:
                    g.DrawLine(p, X, Y, x1, y1);
                    g.DrawLine(p, X, Y, x2, y2);
                    break;

                case 1:
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawLine(p, x2, y2, x1, y1);
                    break;

                case 2:
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawLine(p, x2, y2, x1, y1);
                    break;

                case 3:
                case 4:
                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawLine(p, x3, y3, x4, y4);

                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x3, y3, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x4, y4, 3, 3);
                    break;

                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                case 39:
                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 40:
                case 41:
                case 42:
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                case 60:
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                case 70:
                case 71:
                case 72:
                case 73:
                case 74:
                case 75:
                case 76:
                case 77:
                case 78:
                case 79:
                    p.Color = Color.FromArgb(20, p.Color.R, p.Color.G, p.Color.B);
                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkRed, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                case 1300:
                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkRed, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;

                default:
                    break;
            }
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int cnt = 0;

            int num = rand.Next(333) + 33;
            num = 1;
            br.Color = Color.FromArgb(3, 0, 0, 0);

            list.Add(new myObj_132());

            while (isAlive)
            {
                if (needNewScreen)
                {
                    g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
                    needNewScreen = false;
                }

                foreach (myObj_132 s in list)
                {
                    s.Move();
                    s.Show();
                }

                // Jump to next -- generate new
                if (my.myObject.ShowInfo)
                {
                    fLifeCnt = 0;
                    my.myObject.ShowInfo = false;
                }

                if (++cnt % 5 == 0 && isDimmable)
                {
                    g.FillRectangle(br, 0, 0, Width, Height);
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);
            }

            return;
        }

        // -------------------------------------------------------------------------

        private void constSetUp1()
        {
            switch (rand.Next(2))
            {
                case 0:
                    sf1 = rand.Next(2000) + 100;
                    break;

                case 1:
                    sf1 = rand.Next(1000) + 100;
                    break;
            }

            switch (rand.Next(2))
            {
                case 0:
                    sf2 = rand.Next(2000) + 100;
                    break;

                case 1:
                    sf2 = rand.Next(1000) + 100;
                    break;
            }

            switch (rand.Next(3))
            {
                case 0:
                    sf3 = rand.Next(5000);
                    break;

                case 1:
                    sf3 = rand.Next(500);
                    break;

                case 2:
                    sf3 = rand.Next(50);
                    break;
            }

            switch (rand.Next(3))
            {
                case 0:
                    sf4 = rand.Next(5000);
                    break;

                case 1:
                    sf4 = rand.Next(500);
                    break;

                case 2:
                    sf4 = rand.Next(50);
                    break;
            }

            sf3 = (sf3 + 1) * 0.01f;
            sf4 = (sf4 + 1) * 0.01f;

#if false
            // Old
            sf1 = rand.Next(2000) + 100;
            sf2 = rand.Next(2000) + 100;

            sf3 = rand.Next(1000) * 0.05f;
            sf4 = rand.Next(1000) * 0.05f;
#endif

            si1 = rand.Next(100) + 1;

            x0 = Width  / 2;
            y0 = Height / 2;

            a = (rand.Next(201) * 0.01f) - 1.0f;
            b = (rand.Next(201) * 0.01f) - 1.0f;
            c = 1.0f + rand.Next(300) * 0.01f;

            if (rand.Next(3) == 0)
                a *= (rand.Next(11) + 1);

            if (rand.Next(3) == 0)
                b *= (rand.Next(11) + 1);

            fdLifeCnt = 0.25f;

            // Affects moving speed of the red dot along its path
            dt2 = 0.01f + rand.Next(20) * 0.005f;

            // Affects life span of the shape
            fdLifeCnt = 0.20f;

            return;
        }

        // -------------------------------------------------------------------------

        private void constSetUp2()
        {
            // Set up all the constants
            constSetUp1();

            // Adjust both radiuses a bit
            sf2 *= (sf2 > 333) ? 1 : 2;
            sf1 = sf2 / 2;

            // sf3 here will affect the form of the shape.
            // These values were found manually and result in closed-loop shapes:

            switch (rand.Next(14))
            {
                case  0: sf3 = 2.000f; break;       //
                case  1: sf3 = 1.500f; break;       //
                case  2: sf3 = 1.000f; break;       // Star
                case  3: sf3 = 0.750f; break;       // Star
                case  4: sf3 = 0.500f; break;       // Triangle-Apple-Fish
                case  5: sf3 = 0.300f; break;       // Many-leaves spiral
                case  6: sf3 = 0.250f; break;       // 3-leaf spiral
                case  7: sf3 = 0.200f; break;       // 4-leaf spiral
                case  8: sf3 = 0.075f; break;       // elliptic spiral
                case  9: sf3 = 0.050f; break;       // elliptic spiral
                case 10: sf3 = 0.025f; break;       // spiraling circle
                case 11: sf3 = 0.010f; break;       // spiraling circle
                case 12: sf3 = 0.000f; break;       // circle

                default:
                    sf3 = 0.0001f * rand.Next(10000);
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        private void constSetUp3()
        {
            // Set up all the constants
            constSetUp1();

            // Adjust both radiuses a bit
            sf2 *= (sf2 > 333) ? 1 : 2;
            sf1 = sf2 / 2;

            // Let's change [a] and [sf3]
            // sf3 here will affect the form of the shape.
            // These values were found manually and result in closed-loop shapes:

            int aRnd = rand.Next(5);

            switch (rand.Next(13))
            {
                // circle
                // no real impact from a. a < 1 makes the circle larger than the other circle
                case 0:
                    sf3 = 0.0f;
                    break;

                // spiraling circle
                case 1:
                    sf3 = 0.01f;
                    a = (aRnd < 3)
                        ? 0.01f * (rand.Next(100) + 1)      // [0.01 .. 1]
                        : 0.10f * (rand.Next(3000) + 10);   // [1 .. 300]
                    break;

                // spiraling circle
                case 2:
                    sf3 = 0.025f;
                    a = (aRnd < 3)
                        ? 0.001f * (rand.Next(1000) + 1)        // [0.001 .. 1]
                        : 0.100f * (rand.Next(2000) + 10);      // [1 .. 200]
                    break;

                // elliptic spiral -- need more life span
                case 3:
                    sf3 = 0.05f;
                    a = 0.100f * (rand.Next(3000) + 10);      // [1 .. 300] -- no need to go less than 1.0
                    break;

                // elliptic spiral -- need more life span
                case 4:
                    sf3 = 0.075f;
                    a = 0.100f * (rand.Next(1000) + 10);      // [1 .. 100] -- no need to go less than 1.0
                    break;

                // 4-leaf spiral
                case 5:
                    sf3 = 0.2f;
                    a = (aRnd < 4)
                        ? 0.1f * (rand.Next(991) + 10)          // [1 .. 100]       -- var shapes
                        : 0.1f * (rand.Next(50000) + 1000);     // [100 .. 5000]    -- elliptical shape
                    break;

                // 3-leaf spiral
                case 6:
                    sf3 = 0.25f;

                    // [0.01 .. 1], the second circle must not be larger than the screen
                    if (aRnd == 0)
                    {
                        a = 0.01f * (rand.Next(99) + 1);

                        sf2 = rand.Next(Height/2) + Height/2;
                        sf1 = sf2 / 2;
                        break;
                    }

                    // [30 .. 1000]
                    if (aRnd == 1)
                    {
                        a = 0.1f * (rand.Next(10000) + 300);
                        break;
                    }

                    // [1..30] is the most interesting part
                    {
                        a = 0.01f * (rand.Next(2901) + 100);
                    }
                    break;

                // Many-leaves spiral
                case 7:
                    sf3 = 0.3f;
                    a = (aRnd < 3)
                        ? 0.10f * (rand.Next(5000) + 200)       // [20 .. 500]
                        : 0.01f * (rand.Next(1900) + 100);      // [1 .. 20]
                    break;

                // Triangle-Apple-Fish
                case 8:
                    sf3 = 0.5f;

                    switch (aRnd)
                    {
                        // [20 .. 100]
                        case 0:
                            a = 0.1f * (rand.Next(800) + 200);
                            break;

                        // [5 .. 20]
                        case 1:
                            a = 0.01f * (rand.Next(1500) + 500);
                            break;

                        // [1 .. 5]
                        case 2:
                            a = 0.01f * (rand.Next(401) + 100);
                            break;

                        // [0.5 .. 1]
                        case 3:
                            a = 0.5f + 0.01f * (rand.Next(51));
                            sf1 = 100;
                            break;

                        // [0.01 .. 0.5]
                        case 4:
                            a = 0.01f * (rand.Next(50) + 1);
                            sf1 = 10;
                            break;
                    }
                    break;

                // todo: finish [a]'s setup:

                // Star
                case 9:
                    sf3 = 0.75f;
                    a = 2.0f;
                    break;

                // Star
                case 10:
                    sf3 = 1.0f;
                    a = 2.0f;
                    break;

                // 
                case 11:
                    sf3 = 1.5f;
                    a = 2.0f;
                    break;

                // 
                case 12:
                    sf3 = 2.0f;
                    a = 2.0f;
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        private void constSetUp4()
        {
            void randInts(ref float val)
            {
                switch (rand.Next(9))
                {
                    case 0:
                        val = rand.Next(3001) + 1;
                        break;

                    case 1: case 2:
                        val = rand.Next(301) + 1;
                        break;

                    case 3: case 4: case 5:
                        val = rand.Next(31) + 1;
                        break;

                    case 6: case 7: case 8:
                        val = rand.Next(11) + 1;
                        break;
                }
            }

            void randFloats(ref float val)
            {
                switch (rand.Next(3))
                {
                    case 0:
                        val = 0.001f * rand.Next(3001);
                        break;

                    case 1: case 2:
                        val = 0.25f * rand.Next(41);
                        break;
                }
            }

            void set2params(ref float a, ref float b, float val)
            {
                a = b = val;
            }

            // ---------------------------

            constSetUp1();

            a = 1;
            b = 1;

            fdLifeCnt = 0.01f;

            int rnd = 1111;

            switch (rnd)
            {
                // Circle / Ellipse
                case 0:
                    sf5 = sf6 = sf7 = sf8 = 1.0f;

                    a = 0.01f * rand.Next(300);
                    b = 0.01f * rand.Next(300);
                    break;

                // Beaker - Pure Form
                case 1:
                    sf5 = sf7 = sf8 = 1.0f;
                    sf6 = 2.0f;

                    a = 0.9f + 0.01f * rand.Next(21);
                    b = 0.9f + 0.01f * rand.Next(21);
                    break;

                // Beaker - Pure Form + Randomized a, b
                case 2:
                    sf5 = sf7 = sf8 = 1.0f;
                    sf6 = 2;

                    a = 0.01f * rand.Next(300);
                    b = 0.01f * rand.Next(300);
                    break;

                // Beaker - Randomized Ints
                case 3:
                    sf5 = sf7 = sf8 = 1.0f;
                    randInts(ref sf6);
                    break;

                // Beaker - Randomized Ints + Randomized a, b
                case 4:
                    sf5 = sf7 = sf8 = 1.0f;
                    randInts(ref sf6);

                    a = 0.01f * rand.Next(300);
                    b = 0.01f * rand.Next(300);
                    break;

                // Beaker - Randomized Floats
                case 5:
                    sf5 = sf7 = sf8 = 1.0f;
                    randFloats(ref sf6);
                    break;

                // Beaker - Randomized Floats + Randomized a, b
                case 6:
                    sf5 = sf7 = sf8 = 1.0f;
                    randFloats(ref sf6);

                    a = 0.01f * rand.Next(300);
                    b = 0.01f * rand.Next(300);
                    break;

                // Rain Drop - Pure Form
                case 7:
                    sf5 = sf7 = sf8 = 1.0f;
                    sf6 = 2.0f;

                    a = 0.45f + 0.01f * rand.Next(11);
                    b = 1.9f + 0.01f * rand.Next(21);
                    break;

                // Rain Drop - Pure Form - Width randomized
                case 8:
                    sf5 = sf7 = sf8 = 1.0f;
                    sf6 = 2.0f;

                    a = 0.1f + 0.01f * rand.Next(41);
                    b = 1.0f + 0.01f * rand.Next(101);
                    break;

                // Rain Drop - Eggish
                case 9:
                    sf5 = sf7 = sf8 = 1.0f;
                    sf6 = 2.0f;

                    a = 0.10f + 0.01f * rand.Next(25);
                    b = 0.25f + 0.01f * rand.Next(75);
                    break;

                // Rain Drop - Truffle
                case 10:
                    sf5 = sf7 = sf8 = 1.0f;
                    sf6 = 2.0f;

                    a = 0.400f + 0.001f * rand.Next(101);
                    b = 0.005f * rand.Next(151);
                    break;

                // Rain Drop - Trianglic
                case 11:
                    sf5 = sf7 = sf8 = 1.0f;
                    sf6 = 2.0f;

                    a = 0.250f + 0.001f * rand.Next(101);
                    b = 0.005f * rand.Next(51);
                    break;

                // Smile - Pure Form
                case 12:
                    sf5 = sf6 = sf8 = 1.0f;
                    sf7 = 2.0f;

                    a = 0.9f + 0.01f * rand.Next(21);
                    b = 0.9f + 0.01f * rand.Next(21);
                    return;

                // Smile -- Stable Constants
                case 13:
                    sf5 = sf6 = sf8 = 1.0f;

                    switch (rand.Next(20))
                    {
                        case 00: sf7 = 0.01f; break;
                        case 01: sf7 = 0.20f; break;
                        case 02: sf7 = 0.25f; break;
                        case 03: sf7 = 0.50f; break;
                        case 04: sf7 = 0.75f; break;
                        case 05: sf7 = 1.10f; break;
                        case 06: sf7 = 1.15f; break;
                        case 07: sf7 = 1.20f; break;
                        case 08: sf7 = 1.25f; break;
                        case 09: sf7 = 1.50f; break;
                        case 10: sf7 = 1.75f; break;
                        case 11: sf7 = 2.00f; break;
                        case 12: sf7 = 2.25f; break;
                        case 13: sf7 = 2.50f; break;
                        case 14: sf7 = 2.75f; break;
                        case 15: sf7 = 3.00f; break;
                        case 16: sf7 = 3.25f; break;
                        case 17: sf7 = 3.50f; break;
                        case 18: sf7 = 4.00f; break;
                        case 19: sf7 = 5.00f; break;
                    }

                    a = 0.9f + 0.01f * rand.Next(21);
                    b = 0.9f + 0.01f * rand.Next(21);
                    return;

                // Smile - Pure Form + Size Randomized
                case 14:
                    sf5 = sf6 = sf8 = 1.0f;
                    sf7 = 2.0f;

                    a = 0.01f * rand.Next(1001);
                    b = 0.01f * rand.Next(501);
                    break;

                // Smile -- Stable Constants + Size Randomized
                case 15:
                    sf5 = sf6 = sf8 = 1.0f;

                    switch (rand.Next(20))
                    {
                        case 00: sf7 = 0.01f; break;
                        case 01: sf7 = 0.20f; break;
                        case 02: sf7 = 0.25f; break;
                        case 03: sf7 = 0.50f; break;
                        case 04: sf7 = 0.75f; break;
                        case 05: sf7 = 1.10f; break;
                        case 06: sf7 = 1.15f; break;
                        case 07: sf7 = 1.20f; break;
                        case 08: sf7 = 1.25f; break;
                        case 09: sf7 = 1.50f; break;
                        case 10: sf7 = 1.75f; break;
                        case 11: sf7 = 2.00f; break;
                        case 12: sf7 = 2.25f; break;
                        case 13: sf7 = 2.50f; break;
                        case 14: sf7 = 2.75f; break;
                        case 15: sf7 = 3.00f; break;
                        case 16: sf7 = 3.25f; break;
                        case 17: sf7 = 3.50f; break;
                        case 18: sf7 = 4.00f; break;
                        case 19: sf7 = 5.00f; break;
                    }

                    a = 0.01f * rand.Next(1001);
                    b = 0.01f * rand.Next(501);
                    break;

                // Smile -- Random constant + Size Randomized
                case 16:
                    sf5 = sf6 = sf8 = 1.0f;
                    sf7 = 1.0f + 0.01f * rand.Next(1001);

                    a = 0.01f * rand.Next(1001);
                    b = 0.01f * rand.Next(501);
                    break;

                // Apple-Clover-Mushroom-Mouse-1 - Pure Form
                case 17:
                    sf5 = sf6 = sf7 = sf8 = 1.0f;
                    set2params(ref sf5, ref sf7, 2.0f);

                    a = 0.9f + 0.01f * rand.Next(21);
                    b = 0.9f + 0.01f * rand.Next(21);
                    break;

                // Apple-Clover-Mushroom-Mouse-1 - Pure Form + Size Randomized
                case 18:
                    sf5 = sf6 = sf7 = sf8 = 1.0f;
                    set2params(ref sf5, ref sf7, 2.0f);

                    a = 0.01f * rand.Next(1001);
                    b = 0.01f * rand.Next(501);
                    break;

                // Apple-Clover-Mushroom-Mouse-1 - Param Randomized
                case 19:
                    sf5 = sf6 = sf7 = sf8 = 1.0f;

                    switch (rand.Next(5))
                    {
                        case 0: case 1:
                            c = 0.01f * rand.Next(201);
                            break;

                        case 2: case 3:
                            c = 2.0f + 0.01f * rand.Next(501);
                            break;

                        case 4:
                            c = 2.0f + 0.01f * rand.Next(5001);
                            break;
                    }

                    set2params(ref sf5, ref sf7, c);

                    a = 0.9f + 0.01f * rand.Next(21);
                    b = 0.9f + 0.01f * rand.Next(21);
                    break;

                // Apple-Clover-Mushroom-Mouse-1 - Param Randomized + Size Randomized
                case 20:
                    sf5 = sf6 = sf7 = sf8 = 1.0f;

                    switch (rand.Next(5))
                    {
                        case 0: case 1:
                            c = 0.01f * rand.Next(201);
                            break;

                        case 2: case 3:
                            c = 2.0f + 0.01f * rand.Next(501);
                            break;

                        case 4:
                            c = 2.0f + 0.01f * rand.Next(5001);
                            break;
                    }

                    set2params(ref sf5, ref sf7, c);

                    a = 0.01f * rand.Next(1001);
                    b = 0.01f * rand.Next(501);
                    break;

                // Apple-Clover-Mushroom-Mouse-1 - Both Params Randomized
                case 21:
                    sf5 = sf6 = sf7 = sf8 = 1.0f;

                    switch (rand.Next(5))
                    {
                        case 0: case 1:
                            sf5 = 0.01f * rand.Next(201);
                            break;

                        case 2: case 3:
                            sf5 = 2.0f + 0.01f * rand.Next(501);
                            break;

                        case 4:
                            sf5 = 2.0f + 0.01f * rand.Next(5001);
                            break;
                    }

                    switch (rand.Next(5))
                    {
                        case 0: case 1:
                            sf7 = 0.01f * rand.Next(201);
                            break;

                        case 2: case 3:
                            sf7 = 2.0f + 0.01f * rand.Next(501);
                            break;

                        case 4:
                            sf7 = 2.0f + 0.01f * rand.Next(5001);
                            break;
                    }

                    if (rand.Next(2) == 0)
                    {
                        a = 0.01f * rand.Next(1001);
                        b = 0.01f * rand.Next(501);
                    }
                    else
                    {
                        a = 0.9f + 0.01f * rand.Next(21);
                        b = 0.9f + 0.01f * rand.Next(21);
                    }
                    break;

                // Apple-Clover-Mushroom-Mouse-1 - One param from predefined list, the second is its multiplicative
                case 22:
                    sf5 = sf6 = sf7 = sf8 = 1.0f;

                    // Get value from [0.025, 0.05, 0.1, 0.15, 0.20 ... 1.0]
                    switch (rand.Next(21))
                    {
                        case 0: sf5 = 0.025f; break;
                        case 1: sf5 = 0.050f; break;

                        case 2: case 3: case 4: case 5: case 6: case 7: case 8: case 9:
                        case 10: case 11: case 12: case 13: case 14: case 15: case 16:
                        case 17: case 18: case 19: case 20:
                            sf5 = 0.10f + 0.05f * rand.Next(19);
                            break;
                    }

                    // Increase by int sometimes
                    if (rand.Next(50) < 10)
                    {
                        sf5 += rand.Next(5);
                    }

                    sf7 = sf5 * (rand.Next(5) + 1);

                    // Swap
                    if (rand.Next(2) == 0)
                    {
                        c = sf5;
                        sf5 = sf7;
                        sf7 = c;
                    }

                    if (rand.Next(2) == 0)
                    {
                        a = 0.01f * rand.Next(1001);
                        b = 0.01f * rand.Next(501);
                    }
                    else
                    {
                        a = 0.9f + 0.01f * rand.Next(21);
                        b = 0.9f + 0.01f * rand.Next(21);
                    }
                    break;

                // Apple-Clover-Mushroom-Mouse-1 - Multiplicatives of 0.5f
                case 23:
                    sf5 = sf6 = sf7 = sf8 = 1.0f;

                    sf5 = 0.5f * (rand.Next(21) + 1);
                    sf7 = 0.5f * (rand.Next(21) + 1);

                    if (rand.Next(2) == 0)
                    {
                        a = 0.01f * rand.Next(1001);
                        b = 0.01f * rand.Next(501);
                    }
                    else
                    {
                        a = 0.9f + 0.01f * rand.Next(21);
                        b = 0.9f + 0.01f * rand.Next(21);
                    }
                    break;

                case 24:
joppa:
                    // mouse is lost :(
                    // try mults of other than 0.5f
                    sf5 = sf6 = sf7 = sf8 = 1.0f;

                    sf5 = 3.0f;
                    sf7 = 1.5f;

                    a = 1;
                    b = 1;

                    a = 0.9f + 0.01f * rand.Next(21);
                    b = 0.9f + 0.01f * rand.Next(21);

                    break;

                case 1111:
                    goto joppa;
                    break;

                // ---- later ----

                // Eight - Pure Form
                case 90:
                    sf5 = sf6 = sf7 = sf8 = 1.0f;

                    sf5 = 2.0f;
                    sf6 = 2.0f;

                    a = 0.9f + 0.01f * rand.Next(21);
                    b = 0.9f + 0.01f * rand.Next(21);
                    return; // <-- need that

                // Parabola 1 -- Pure Form
                case 98:
                    sf5 = sf6 = sf7 = sf8 = 1.0f;
                    sf5 = sf7 = 2.0f;

                    //a = 0.9f + 0.01f * rand.Next(21);
                    //b = 0.9f + 0.01f * rand.Next(21);
                    break;

                // Parabola 2 -- Pure Form
                case 99:
                    sf5 = sf6 = sf7 = sf8 = 1.0f;
                    sf7 = sf8 = 2.0f;

                    //a = 0.9f + 0.01f * rand.Next(21);
                    //b = 0.9f + 0.01f * rand.Next(21);
                    break;

                // Randoms
                case 100:
                    sf5 = 0.01f * rand.Next(100);
                    sf6 = 0.01f * rand.Next(100);
                    sf7 = 0.01f * rand.Next(100);
                    sf8 = 0.01f * rand.Next(100);
                    break;

            }

            // Random sign for each of the params
            sf5 *= my.myUtils.getRandomSign(rand);
            sf6 *= my.myUtils.getRandomSign(rand);
            sf7 *= my.myUtils.getRandomSign(rand);
            sf8 *= my.myUtils.getRandomSign(rand);

            return;
        }

        // -------------------------------------------------------------------------
    }
};
