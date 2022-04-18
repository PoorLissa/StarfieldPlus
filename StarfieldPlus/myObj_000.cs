using System;
using System.Drawing;
using System.Collections.Generic;

/*
    - Star Field

    todo:
        - random XY generation using only width (as in obj_100)
        - add comets (not used now)
*/

namespace my
{
    public class myObj_000 : myObject
    {
        protected static List<myObject> list = null;
        protected static SolidBrush dimBrush = null;
        protected float x, y, dx, dy, acceleration = 1.0f;
        protected int cnt = 0, max = 0, color = 0;

        protected static int x0 = 0, y0 = 0, drawMode = 0;

        // -------------------------------------------------------------------------

        public myObj_000()
        {
            if (br == null)
            {
                p = new Pen(Color.White);
                br = new SolidBrush(Color.Black);
                dimBrush = new SolidBrush(Color.Black);
                list = new List<myObject>();

                drawMode = rand.Next(2);

                x0 = Width  / 2;
                y0 = Height / 2;

                Log($"myObj_000");
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

		protected override void generateNew()
        {
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            switch (color)
            {
                case 0: br.Color = Color.Red;    break;
                case 1: br.Color = Color.Yellow; break;
                case 2: br.Color = Color.Blue;   break;
                case 3: br.Color = Color.Orange; break;
                case 4: br.Color = Color.Aqua;   break;
                case 5: br.Color = Color.Violet; break;

                default:
                    br.Color = Color.White;
                    break;
            }

            switch (drawMode)
            {
                case 0:
                    g.FillRectangle(br, X, Y, Size, Size);
                    break;

                case 1:
                    p.Color = br.Color;
                    g.DrawRectangle(p, X, Y, Size - 1, Size - 1);
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            form.Invalidate();
            System.Threading.Thread.Sleep(666);

            int staticStarsCnt = rand.Next(333) + 333;

            // Add static stars
            Count += staticStarsCnt;

            for (int i = 0; i < staticStarsCnt; i++)
            {
                list.Add(new myObj_000_b());
            }

            list.Add(new myObj_000_c());
            list.Add(new myObj_000_c());
            list.Add(new myObj_000_c());

            while (isAlive)
            {
                g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                foreach (myObj_000 obj in list)
                {
                    obj.Show();
                    obj.Move();
                }

                System.Threading.Thread.Sleep(33);
                form.Invalidate();

                // Gradually increase number of moving stars, until the limit is reached
                if (list.Count < Count)
                {
                    list.Add(new myObj_000_a());
                }
            }

            return;
        }
    }


    // ===========================================================================================================
    // ===========================================================================================================


    // Moving stars
    class myObj_000_a : myObj_000
    {
		protected override void generateNew()
        {
#if true

            max = rand.Next(75) + 20;
            cnt = 0;
            color = rand.Next(50);
            acceleration = 1.005f + (rand.Next(100) * 0.0005f);

            x = X = rand.Next(Width);
            y = Y = rand.Next(Width);

            int speed = rand.Next(10) + 1;

            double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - x0) * (Y - x0));
            double sp_dist = speed / dist;

            dx = (float)((X - x0) * sp_dist);
            dy = (float)((Y - x0) * sp_dist);

            y = Y = Y - (Width - Height) / 2;

#else

            max = rand.Next(75) + 20;
            cnt = 0;
            color = rand.Next(50);
            acceleration = 1.005f + (rand.Next(100) * 0.0005f);

            x = X = rand.Next(Width);
            y = Y = rand.Next(Height);

            int speed = rand.Next(10) + 1;

            double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));
            double sp_dist = speed / dist;

            dx = (float)((X - x0) * sp_dist);
            dy = (float)((Y - y0) * sp_dist);

#endif

            Size = 0;
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            x += dx;
            y += dy;

            X = (int)x;
            Y = (int)y;

            if (cnt++ > max)
            {
                cnt = 0;
                Size++;

                // Accelerate acceleration rate
                acceleration *= (1.0f + (Size * 0.001f));
            }

            // Accelerate our moving stars
            dx *= acceleration;
            dy *= acceleration;

            if (X < 0 || X > Width || Y < 0 || Y > Height)
            {
                generateNew();
            }

            return;
        }
    };


    // ===========================================================================================================
    // ===========================================================================================================


    // Static stars
    public class myObj_000_b : myObj_000
    {
        private int lifeCounter = 0;
        private int alpha = 0;
        private static int factor = 1;
        private static bool doMove = true;

        protected override void generateNew()
        {
            lifeCounter = (rand.Next(500) + 500) * factor;

            X = rand.Next(Width);
            Y = rand.Next(Height);
            color = rand.Next(50);
            alpha = rand.Next(50) + 175;

            max = (rand.Next(200) + 100) * factor;
            cnt = 0;
            Size = 0;

            // Make our static stars not so static
            if (doMove)
            {
                // linear speed outwards:
                double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));
                double sp_dist = 0.1f / dist;

                dx = (float)((X - x0) * sp_dist);
                dy = (float)((Y - y0) * sp_dist);

                x = X;
                y = Y;
            }
        }

        protected override void Move()
        {
            if (doMove)
            {
                x += dx;
                y += dy;

                X = (int)x;
                Y = (int)y;
            }

            if (lifeCounter-- == 0)
            {
                factor = 3;
                generateNew();
            }
            else
            {
                if (cnt++ > max)
                {
                    cnt = 0;
                    Size = rand.Next(5) + 1;
                }
            }

            return;
        }

        protected override void Show()
        {
            // Draw static stars ...
            base.Show();

            if (cnt % 100 == 0)
            {
                // ... and make them semitransparent
                alpha = rand.Next(50) + 175;

                if (rand.Next(11) == 0)
                {
                    alpha -= rand.Next(alpha);
                }
            }

            dimBrush.Color = Color.FromArgb(alpha, 0, 0, 0);
            g.FillRectangle(dimBrush, X, Y, Size, Size);
        }
    };


    // ===========================================================================================================
    // ===========================================================================================================


    // Comets
    public class myObj_000_c : myObj_000
    {
        private int lifeCounter = 0, xOld = 0, yOld = 0;

        protected override void generateNew()
        {
            lifeCounter = rand.Next(1000) + 666;

            int x0 = rand.Next(Width);
            int y0 = rand.Next(Height);
            int x1 = rand.Next(Width);
            int y1 = rand.Next(Height);

            float a = (float)(y1 - y0) / (float)(x1 - x0);
            float b = y1 - a * x1;

            int speed = rand.Next(100) + 50;

            double dist = Math.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0));
            double sp_dist = speed / dist;

            dx = (float)((x1 - x0) * sp_dist);
            dy = (float)((y1 - y0) * sp_dist);

            if (dx > 0)
            {
                x = X = xOld = 0;
                y = Y = yOld = (int)b;
            }
            else
            {
                x = xOld = X = Width;
                y = a * x + b;
                Y = yOld = (int)y;
            }

            Size = rand.Next(3) + 1;
        }

        protected override void Move()
        {
            xOld = X;
            yOld = Y;

            if (lifeCounter-- < 0)
            {
                x += dx;
                y += dy;

                X = (int)x;
                Y = (int)y;

                if (dx > 0 && X > Width)
                {
                    generateNew();
                }

                if (dx < 0 && X < 0)
                {
                    generateNew();
                }
            }

            return;
        }

        protected override void Show()
        {
            if (lifeCounter < 0)
            {
                //g.DrawEllipse(Pens.DarkOrange, X, Y, Size, Size);
                g.DrawLine(Pens.Red, X-1, Y, xOld, yOld);
                g.DrawLine(Pens.Red, X+0, Y, xOld, yOld);
                g.DrawLine(Pens.Red, X+1, Y, xOld, yOld);
                g.FillRectangle(Brushes.OrangeRed, X - Size, Y - Size, 2*Size, 2*Size);
            }
        }

    };

};
