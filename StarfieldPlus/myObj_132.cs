using System;
using System.Drawing;

/*
    - 
*/

namespace my
{
    public class myObj_132 : myObject
    {
        static int max_dSize = 0;
        static int shape = 0;

        protected int maxSize = 0, A = 0, R = 0, G = 0, B = 0, dSize = 0, dA = 0, dA_Filling = 0;

        float time, dx, dy, float_A, float_B;

        // -------------------------------------------------------------------------

        public myObj_132()
        {
            if (p == null)
            {
                p = new Pen(Color.Red);
                br = new SolidBrush(Color.Red);
                colorPicker = new myColorPicker(Width, Height, 3);

                shape = rand.Next(5);
                max_dSize = rand.Next(15) + 3;

shape = 1;

                Log($"myObj_132: colorPicker({colorPicker.getMode()}), shape({shape}), max_dSize({max_dSize})");
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
            if (shape < 2)
            {
                move_0();

                if (float_A < 0)
                {
                    generateNew();
                }
            }
            else
            {
                Size += dSize;

                // Increase disappearing speed when max size is reached
                if (Size > maxSize)
                    dA++;

                // Decrease opacity until fully invisible
                A -= dA;

                if (A < 0)
                {
                    generateNew();
                }
            }
        }

        // -------------------------------------------------------------------------

        private void move_0()
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
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            //p.Color = Color.FromArgb(A, R, G, B);

            p.Color = Color.FromArgb(100, R, G, B);

            switch (shape)
            {
                case 0:
                    g.DrawLine(p, X, Y, 2 * Size, 2 * Size - dx);
                    g.DrawLine(p, X, Y, Size * dx, Size * dy);
                    break;

                case 1:

                    float dx2 = (float)(Math.Cos(time)) * 10;
                    float dy2 = (float)(Math.Sin(time)) * 10;

                    //g.DrawLine(p, X, Y, Width/2, 10);

                    //g.DrawLine(p, Width / 2, 10, Y, X);
                    //g.DrawLine(p, X/2, Y/2, Size + dx, Size + dy);


                    // ok?
                    if (false)
                    {
                        g.DrawRectangle(Pens.DarkOrange, Size + dx2, Size + dy2, 3, 3);
                        g.DrawLine(p, X / 2, Y / 2, Size + dx2, Size + dy2);
                    }

                    if (false)
                    {
                        float x1 = Size + dx2;
                        float y1 = Size + dy2;
                        float x2 = Width  - Size + dx2;
                        float y2 = Height - Size + dy2;

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
                    }

                    if (false)
                    {
                        float x1 = Size + dx2 * dx/10;
                        float y1 = Size + dy2;
                        float x2 = Width  - Size + dx2 * dx/10;
                        float y2 = Height - Size + dy2;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);

                        x1 = Width - Size + dx2 * dx/10;
                        y1 = Size + dy2;// * dy/10;
                        x2 = Size + dx2 * dx / 10;
                        y2 = Height - Size + dy2;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    if (false)
                    {
                        float x1 = Size + dx2;
                        float y1 = Size + dy2 * dy/20;

                        x1 += float_B;

                        float x2 = Width - x1;
                        float y2 = Height - y1;

                        float_B += 1.123f;  // try changing this value

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    if (false)
                    {
                        float x1 = Size + dx2;
                        float y1 = Size + dy2 * dy / 20;

                        x1 += (float)Math.Sin(float_B) * 10;
                        y1 += (float)Math.Cos(time) * 5;

                        float x2 = Width - x1;
                        float y2 = Height - y1;

                        float_B += 0.23f;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    if (true)
                    {
                        float x1 = Size + dx2;
                        float y1 = Size + dy2 * dy / 20;

                        x1 += (float)Math.Sin(float_B) * 10;
                        y1 += (float)Math.Cos(time) * 5;

                        x1 += 2*Width/5;

                        float x2 = Width - x1;
                        float y2 = y1;

                        float_B += 0.23f;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    // ok
                    if (false)
                    {
                        float x1 = Size + dx2;
                        float y1 = Size + dy2;
                        float x2 = 4 * Width  / 5 + dx2 * Size / 20;
                        float y2 = 2 * Height / 5 + dy2 * Size / 20;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    if (false)
                    {
                        int aaa = 50;

                        float x1 = 1 * Width  / 5 + dx2 * Size / aaa;
                        float y1 = 1 * Height / 2 + dy2 * Size / aaa;
                        float x2 = 4 * Width  / 5 - dx2 * Size / aaa;
                        float y2 = 1 * Height / 2 - dy2 * Size / aaa;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    if (false)
                    {
                        int aaa = 50;

                        float x1 = 1 * Width  / 5 + dx2 * Size / aaa;
                        float y1 = 1 * Height / 2 - dy2 * Size / aaa;
                        float x2 = 4 * Width  / 5 + dx2 * Size / aaa;
                        float y2 = 1 * Height / 2 - dy2 * Size / aaa;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    if (false)
                    {
                        int aaa = 50;

                        float x1 = 1 * Width  / 5 - dy2 * Size / aaa;
                        float y1 = 1 * Height / 2 + dx2 * Size / aaa;
                        float x2 = 4 * Width  / 5 + dx2 * Size / aaa;
                        float y2 = 1 * Height / 2 - dy2 * Size / aaa;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    if (false)
                    {
                        int aaa = 50;

                        float x1 = 1 * Width  / 5 + dx  * Size / aaa;
                        float y1 = 1 * Height / 2 + dy  * Size / aaa;
                        float x2 = 4 * Width  / 5 + dx2 * Size / aaa;
                        float y2 = 1 * Height / 2 - dy2 * Size / aaa;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    if (false)
                    {
                        int aaa = 50;

                        float x1 = 1 * Width  / 5 + dx  * Size / aaa / aaa;
                        float y1 = 1 * Height / 2 + dy  * Size / aaa / aaa;
                        float x2 = 4 * Width  / 5 + dx2 * Size / aaa;
                        float y2 = 1 * Height / 2 - dy2 * Size / aaa;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    if (false)
                    {
                        int aaa = 50;

                        float x1 = 1 * Width  / 5 - dx  * Size / aaa / aaa;
                        float y1 = 1 * Height / 2 - dy  * Size / aaa / aaa;
                        float x2 = 4 * Width  / 5 + dx2 * Size / aaa;
                        float y2 = 1 * Height / 2 - dy2 * Size / aaa;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    if (false)
                    {
                        int aaa = 50;

                        float x1 = 1 * Width  / 5 + dx  * Size / aaa / aaa;
                        float y1 = 1 * Height / 2 - dy  * Size / aaa / aaa;
                        float x2 = 4 * Width  / 5 + dx2 * Size / aaa;
                        float y2 = 1 * Height / 2 - dy2 * Size / aaa;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    if (false)
                    {
                        int aaa = 50;

                        float x1 = 1 * Width  / 5 + dx  * Size / aaa / 2;
                        float y1 = 1 * Height / 2 - dy  * Size / aaa / 2;
                        float x2 = 4 * Width  / 5 + dx2 * Size / aaa / 1;
                        float y2 = 1 * Height / 2 - dy2 * Size / aaa / 1;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    if (false)
                    {
                        int aaa = 50;

                        float x1 = 1 * Width  / 5 + dx  * Size / aaa / 2;
                        float y1 = 1 * Height / 2 - dy  * Size / aaa / 2;
                        float x2 = 4 * Width  / 5 + dx2 * Size / aaa * float_B;
                        float y2 = 1 * Height / 2 - dy2 * Size / aaa * float_B;

                        float_B += 0.0001f;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    if (false)
                    {
                        int aaa = 50;

                        float x1 = 1 * Width  / 5 + dx  * Size / aaa / 2;
                        float y1 = 1 * Height / 2 - dy  * Size / aaa / 2;
                        float x2 = 4 * Width  / 5 + dx2 * 33;
                        float y2 = 1 * Height / 2 - dy2 * 33;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    if (false)
                    {
                        int aaa = 50;

                        float x1 = 1 * Width  / 5 + dx  * Size / aaa / 33;
                        float y1 = 1 * Height / 2 - dy  * Size / aaa / 33;
                        float x2 = 4 * Width  / 5 + dx2 * 33;
                        float y2 = 1 * Height / 2 - dy2 * 33;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    if (false)
                    {
                        int aaa = 50;

                        float x1 = 1 * Width  / 5 + dx  * Size / aaa / 33;
                        float y1 = 1 * Height / 2 - dy  * Size / aaa / 33;
                        float x2 = 4 * Width  / 5 + dx2 * 33 / float_B;
                        float y2 = 1 * Height / 2 - dy2 * 33 / float_B;

                        float_B += 0.001f;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    if (false)
                    {
                        float x1 = 1 * Width  / 5 + dx2 * 33;
                        float y1 = 1 * Height / 2 - dy2 * 33;
                        float x2 = 4 * Width  / 5 + dx2 * 33;
                        float y2 = 1 * Height / 2 - dy2 * 33;

                        float_B += 0.001f;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    if (false)
                    {
                        float x1 = 1 * Width  / 5 + dy2 * 33 / float_B;
                        float y1 = 1 * Height / 2 - dx2 * 33 / float_B;
                        float x2 = 4 * Width  / 5 + dx2 * 33 * float_B;
                        float y2 = 1 * Height / 2 - dy2 * 33 * float_B;

                        float_B += 0.01f;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    if (false)
                    {
                        float x1 = 1 * Width  / 5 - dy2 * 33 / float_B;
                        float y1 = 1 * Height / 2 - dx2 * 33 / float_B;
                        float x2 = 4 * Width  / 5 + dx2 * 33 * float_B;
                        float y2 = 1 * Height / 2 - dy2 * 33 * float_B;

                        float_B += 0.01f;

                        g.DrawLine(p, x1, y1, x2, y2);
                        g.DrawRectangle(Pens.DarkOrange, x1, y1, 3, 3);
                        g.DrawRectangle(Pens.DarkOrange, x2, y2, 3, 3);
                    }

                    //g.DrawLine(p, X * dx2, Y * dy2, Size * dx, Size * dy);

                    //g.DrawLine(p, X - Size, Y - Size, 2 * Size + dx, 2 * Size + dy);
                    //g.DrawLine(p, X, Y, 2 * Size + dx, 2 * Size + dy);
                    break;

                case 5:
                    break;
            }
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int t = 33;
            int num = rand.Next(333) + 33;

            num = 1;

            var list = new System.Collections.Generic.List<myObj_132>();

            list.Add(new myObj_132());

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            while (isAlive)
            {
                //g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                foreach (var s in list)
                {
                    s.Show();
                    s.Move();
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
