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
        static int t = 0, shape = 0, x0 = 0, y0 = 0, si1 = 0, si2 = 0;
        static bool isDimmable = true, doStartNew = false;
        static float sf1 = 0, sf2 = 0, sf3 = 0, sf4 = 0, a = 0, b = 0, c = 0, fLifeCnt = 0, fdLifeCnt = 0;
        static List<myObject> list = null;

        protected int maxSize = 0, A = 0, R = 0, G = 0, B = 0, dSize = 0, dA = 0, dA_Filling = 0;

        float time, time2, dt2, dx, dy, float_B, x1, y1, x2, y2;

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
            shape = rand.Next(25);
            isDimmable = rand.Next(2) == 0;

shape = 1300;

            Size = 1;
            dSize = rand.Next(max_dSize) + 1;
            dA = rand.Next(5) + 1;
dA = 1;
            dA_Filling = rand.Next(5) + 2;
            time = 0.0f;
            time2 = 0.0f;
            dt2 = 0.01f;

            doStartNew = true;

            changeConstants();

            return;
        }

        // -------------------------------------------------------------------------

        private void changeConstants()
        {
            switch (shape)
            {
                case 115:
                    sf1 = rand.Next(333) + 100;
                    sf2 = rand.Next(10) + 10;
                    sf3 = rand.Next(100) * 0.25f;
                    fdLifeCnt = 0.5f;
                    break;

                case 116:
                case 117:
                case 118:
                    sf1 = rand.Next(333) + 100;
                    sf2 = rand.Next(333) + 10;
                    sf2 = 5;
                    sf3 = rand.Next(100) * 0.25f;
                    fdLifeCnt = 0.5f;
                    break;

                case 119:
                    sf1 = rand.Next(333) + 100;
                    sf2 = rand.Next(333) + 100;
                    sf3 = rand.Next(1000) * 0.05f;
                    sf4 = rand.Next(1000) * 0.05f;
                    fdLifeCnt = 0.5f;
                    break;

                case 120:
                case 121:
                case 122:
                case 123:
                case 124:
                case 125:
                case 126:
                case 127:
                case 128:
                case 129:
                case 130:
                case 131:
                case 132:
                case 133:
                case 134:
                case 135:
                case 136:
                case 137:
                case 138:
                case 139:
                case 140:
                case 141:
                case 142:
                case 143:
                case 144:
                case 145:
                case 146:
                case 147:


                case 1300:

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
                    si2 = rand.Next(100) + 1;

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
t = 3;
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
                    break;

                case 115:
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

                case 116:
                    x1 = 1 * Width  / 5 + (float)(Math.Sin(time)) * sf1;
                    y1 = 1 * Height / 2 + (float)(Math.Cos(time)) * sf1;

                    x2 = 4 * Width  / 5 + (float)(Math.Cos(time)) * sf2;
                    y2 = 1 * Height / 2 + (float)(Math.Sin(time)) * sf2;
                    break;

                case 117:
                    x1 = 1 * Width  / 5 + (float)(Math.Sin(time)) * sf1;
                    y1 = 1 * Height / 2 + (float)(Math.Cos(time)) * sf1;

                    x2 = 4 * Width  / 5 + (float)(Math.Cos(fLifeCnt + time)) * sf2;
                    y2 = 1 * Height / 2 + (float)(Math.Sin(fLifeCnt)) * sf2;
                    break;

                case 118:
                    x1 = 1 * Width  / 5 + (float)(Math.Sin(time)) * sf1;
                    y1 = 1 * Height / 2 + (float)(Math.Cos(time)) * sf1;

                    x2 = 4 * Width  / 5 + (float)(Math.Cos(fLifeCnt + time)) * sf2;
                    y2 = 1 * Height / 2 + (float)(Math.Sin(fLifeCnt + time)) * sf2;
                    break;

                case 119:
                    x1 = 1 * Width  / 5 + (int)(Math.Sin(time * 1.0f) * sf3) / sf3 * sf1;
                    y1 = 1 * Height / 2 + (int)(Math.Cos(time * 1.0f) * sf3) / sf3 * sf1;

                    x2 = 4 * Width  / 5 + (int)(Math.Sin(time * 1.1f) * sf4) / sf4 * 333;
                    y2 = 1 * Height / 2 + (int)(Math.Cos(time * 1.1f) * sf4) / sf4 * 333;
                    break;

                // Cool one -- ok
                case 120:
                    x1 = x0 + (float)(Math.Sin(time * sf3)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf3)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                // Cool one -- ok
                case 121:
                    x1 = x0 + (float)(Math.Sin(time * sf3)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf3)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;

                    sf1 += a;
                    sf2 += b;
                    break;

                // ok
                case 122:
                    x1 = x0 + (float)(Math.Sin(time + sf3)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time + sf3)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time + sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time + sf4)) * sf2;

                    sf3 += a;
                    sf4 += b;
                    break;

                // ok
                case 123:
                    x1 = x0 + (float)(Math.Sin(time * sf3)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf4)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf3)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;

                    sf3 += a;
                    sf4 += b;
                    break;

                // ok
                case 124:
                    x1 = x0 + (float)(Math.Sin(time * sf3)) * sf1 + (float)(Math.Cos(time + sf3)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf3)) * sf1 + (float)(Math.Sin(time + sf3)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                // ok
                case 125:
                    x1 = x0 + (float)(Math.Sin(time * sf3)) * sf1 + (float)(Math.Sin(time / si1)) * sf1 / si1;
                    y1 = y0 + (float)(Math.Cos(time * sf3)) * sf1 + (float)(Math.Cos(time / si1)) * sf1 / si1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                // ok
                case 126:
                    sf1 /= sf1 > 1000 ? 2 : 1;

                    x1 = x0 + (float)(Math.Sin(time * sf3)) * sf1 + (float)(Math.Sin(time * si1)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf3)) * sf1 + (float)(Math.Cos(time * si1)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                // ok
                case 127:
                    x1 = x0 + (float)(Math.Sin(time * sf3)) * sf1 * (float)(Math.Sin(time * si1)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf3)) * sf1 * (float)(Math.Cos(time * si1)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                // ok
                case 128:
                    x1 = (float)(Math.Sin(time * sf3)) * (float)(Math.Sin(time * si1)) * sf1 * 2;
                    y1 = (float)(Math.Cos(time * sf3)) * (float)(Math.Cos(time * si1)) * sf1 * 2;

                    x1 /= si1;
                    y1 /= si1;
                    x1 += x0;
                    y1 += y0;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                // ok
                case 129:
                    x1 = x0 + (float)(Math.Sin(time * sf3)) * (float)(Math.Cos(time * si1)) * sf1 / 2;
                    y1 = y0 + (float)(Math.Cos(time * sf3)) * (float)(Math.Sin(time * si1)) * sf1 / 2;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                // ok
                case 130:
                    x1 = x0 + (float)(Math.Sin(time * sf3) * Math.Sin(1 + time / si1)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf3) * Math.Sin(1 + time / si1)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                // ok
                case 131:
                    x1 = x0 + (float)(Math.Sin(time * sf3) * Math.Sin(si1 + time / si1)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf3) * Math.Sin(si1 + time / si1)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                // ok
                case 132:
                    x1 = x0 + (float)(Math.Sin(time * sf3) + Math.Sin(time / si1 / si1)) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf3) + Math.Cos(time / si1 / si1)) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                // ok
                case 133:
                    x1 = x0 + (int)(Math.Sin(a * time) * sf1);
                    y1 = y0 + (int)(Math.Cos(b * time) * sf1);

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                // ok
                case 134:
                    sf1 /= sf1 > 1000 ? 2 : 1;
                    sf2 /= sf1 > 1000 ? 2 : 1;

                    x1 = x0 + (int)(Math.Sin(a * time) * sf1);
                    y1 = y0 + (int)(Math.Cos(b * time) * sf1);

                    x2 = x0 + (float)(Math.Sin(time * sf4 * a)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4 * b)) * sf2;
                    break;

                // ok
                case 135:
                    sf1 /= sf1 > 1000 ? 2 : 1;
                    sf2 /= sf1 > 1000 ? 2 : 1;

                    x1 = x0 + (float)(Math.Sin(a * time) * sf1);
                    y1 = y0 + (float)(Math.Cos(b * time) * sf1);

                    x2 = x0 + (float)(Math.Sin(time * sf4 * a)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4 * b)) * sf2;
                    break;

                // ok
                case 136:
                    x1 = x0 + (float)(Math.Sin(a * time) * Math.Sin(b * time) * sf1);
                    y1 = y0 + (float)(Math.Cos(a * time) * Math.Cos(b * time) * sf1);

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                // ok
                case 137:
                    x1 = x0 + (float)((Math.Sin(a * time2) + Math.Cos(b * time2)) * sf1);
                    y1 = y0 + (float)((Math.Cos(a * time2) + Math.Sin(b * time2)) * sf1);

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                // ok -- probably is the same as 137
                case 138:
                    x1 = x0 + (float)((Math.Sin(a * time * sf3) + Math.Cos(b * time * sf3)) * sf1);
                    y1 = y0 + (float)((Math.Cos(a * time * sf3) + Math.Sin(b * time * sf3)) * sf1);

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 139:
                    x1 = x0 + (int)((Math.Sin(a * time * sf3) + Math.Cos(b * time * sf3))) * sf1;
                    y1 = y0 + (int)((Math.Cos(a * time * sf3) + Math.Sin(b * time * sf3))) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 140:
                    x1 = x0 + (int)((Math.Sin(a * time * sf3) + Math.Cos(b * time * sf3)) * a) * sf1;
                    y1 = y0 + (int)((Math.Cos(a * time * sf3) + Math.Sin(b * time * sf3)) * a) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 141:
                    x1 = x0 + (int)((Math.Sin(a * time * sf3) + Math.Cos(b * time * sf3)) * c) * sf1;
                    y1 = y0 + (int)((Math.Cos(a * time * sf3) + Math.Sin(b * time * sf3)) * c) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 142:
                    x1 = x0 + (int)((Math.Sin(a * time * sf3) * Math.Cos(b * time * sf3)) * c) * sf1;
                    y1 = y0 + (int)((Math.Cos(a * time * sf3) * Math.Sin(b * time * sf3)) * c) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 143:
                    x1 = x0 + (int)(Math.Sin(time * sf3) * c) * sf1;
                    y1 = y0 + (int)(Math.Cos(time * sf3) * c) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 144:
                    x1 = x0 + (int)(Math.Sin(time * sf3 / 10) * c) * sf1;
                    y1 = y0 + (int)(Math.Cos(time * sf3 / 10) * c) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 145:
                    x1 = x0 + (float)(Math.Sin(time * sf3) * c) * sf1;
                    y1 = y0 + (float)(Math.Cos(time * sf3) * c) * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 146:
                    x1 = x0 + (int)(Math.Sin(time * sf3) * 10) / 10 * sf1;
                    y1 = y0 + (int)(Math.Cos(time * sf3) * 10) / 10 * sf1;

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 147:
                    x1 = x0 + (int)(Math.Sin(time * sf3) * si1) * sf1 / (1.0f * si1);
                    y1 = y0 + (int)(Math.Cos(time * sf3) * si1) * sf1 / (1.0f * si1);

                    x2 = x0 + (float)(Math.Sin(time * sf4)) * sf2;
                    y2 = y0 + (float)(Math.Cos(time * sf4)) * sf2;
                    break;

                case 1300:

                    //sf1 = 300;

                    //sf1 /= sf1 > 1000 ? 2 : 1;
                    //sf2 /= sf1 > 1000 ? 2 : 1;

                    x1 = x0 + (int)(Math.Sin(time * sf3) * si1) * sf1 / (1.0f * si1);
                    y1 = y0 + (int)(Math.Cos(time * sf3) * si1) * sf1 / (1.0f * si1);

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
                case 114:
                case 115:
                default:
                    g.DrawLine(p, x1, y1, x2, y2);
                    g.DrawRectangle(Pens.DarkRed, x1, y1, 3, 3);
                    g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    break;
            }
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int cnt = 0;
            t = 33;
            t -= isDimmable ? 13 : 0;

            int num = rand.Next(333) + 33;
            num = 1;
            br.Color = Color.FromArgb(3, 0, 0, 0);

            list.Add(new myObj_132());

            while (isAlive)
            {
                if (doStartNew)
                {
                    doStartNew = false;
                    g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
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
    }
};
