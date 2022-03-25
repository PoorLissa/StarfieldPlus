using System;
using System.Drawing;

/*
    - moving vertical and horizontal lines
*/

namespace my
{
    public class myObj_120 : myObject
    {
        int length = 0, dir = 0, dx = 0, dy = 0, A = 0, R = 0, G = 0, B = 0;
        static int colorMode = 0;
        static int gl_R = 0, gl_G = 0, gl_B = 0;

        // -------------------------------------------------------------------------

        public myObj_120()
        {
            if (p == null)
            {
                p = new Pen(Color.Red);
                Log($"myObj_120");
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            getColor();

            length = rand.Next(Width + Height) + 100;
            dir = rand.Next(4);

            X = rand.Next(Width);
            Y = rand.Next(Height);

            switch (dir)
            {
                case 0:
                    dx = 1;
                    dy = 0;
                    X = -33 - length - rand.Next(Width);
                    break;

                case 1:
                    dx = -1;
                    dy = 0;
                    X = Width + 33 + length + rand.Next(Width);
                    break;

                case 2:
                    dx = 0;
                    dy = 1;
                    Y = -33 - length - rand.Next(Height);
                    break;

                case 3:
                    dx = 0;
                    dy = -1;
                    Y = Height + 33 + length + rand.Next(Height);
                    break;
            }

            dx *= rand.Next(10) + 7;
            dy *= rand.Next(10) + 7;

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            X += dx;
            Y += dy;

            if ((dir == 0 && X > Width) || (dir == 1 && X < 0) || (dir == 2 && Y > Height) || (dir == 3 && Y < 0))
            {
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            int x2 = 0, y2 = 0;

            switch (dir)
            {
                case 0:
                    x2 = X + length;
                    y2 = Y;
                    break;

                case 1:
                    x2 = X - length;
                    y2 = Y;
                    break;

                case 2:
                    x2 = X;
                    y2 = Y + length;
                    break;

                case 3:
                    x2 = X;
                    y2 = Y - length;
                    break;
            }

            p.Color = Color.FromArgb(A, R, G, B);

            g.DrawLine(p, X, Y, x2, y2);
        }

        // -------------------------------------------------------------------------

        private void getColor()
        {
            A = rand.Next(255) + 1;

            switch (colorMode)
            {
                // Shades of Red
                case 0:
                    R = 233;
                    G = 11;
                    B = 11;
                    break;

                // Shades of Green
                case 1:
                    R = 11;
                    G = 233;
                    B = 11;
                    break;

                // Shades of Blue
                case 2:
                    R = 11;
                    G = 11;
                    B = 233;
                    break;

                // Shades of Random Color
                case 3:
                    while (gl_R + gl_G + gl_B < 500)
                    {
                        // Run once per session
                        gl_R = rand.Next(256);
                        gl_G = rand.Next(256);
                        gl_B = rand.Next(256);
                    }

                    R = gl_R;
                    G = gl_G;
                    B = gl_B;
                    break;

                // Random Color
                case 4:
                    R = rand.Next(256);
                    G = rand.Next(256);
                    B = rand.Next(256);
                    break;

                // Shades of Gray
                default:
                    R = rand.Next(256);
                    G = R;
                    B = G;
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int t = 50;
            int num = rand.Next(333) + 33;
            colorMode = rand.Next(5);

            var list = new System.Collections.Generic.List<myObj_120>();

            for (int i = 0; i < num; i++)
            {
                list.Add(new myObj_120());
            }

            while (isAlive)
            {
                g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                foreach (var s in list)
                {
                    s.Show();
                    s.Move();
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);
            }

            return;
        }
    }
};
