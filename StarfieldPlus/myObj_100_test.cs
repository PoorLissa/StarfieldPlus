using System;
using System.Drawing;

/*
    - Big Bang
*/

namespace my
{
    public class myObj_100 : myObject
    {
        protected static int maxLife = 500;
        protected static int maxSpeed = 0;
        protected static int explosionSpeed = 0;
        protected static int staticStarsCnt = 1111;
        protected static bool isExplosionMode = false;

        protected float x, y, dx, dy;
        protected int cnt = 0;
        protected int max = 0;
        protected int color = 0;

        // -------------------------------------------------------------------------

        public myObj_100()
        {
            if (br == null)
            {
                br = new SolidBrush(Color.Black);
                maxSpeed = rand.Next(11);
                explosionSpeed = rand.Next(100) + 33;
                Log($"myObj_100");
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
        }

        // -------------------------------------------------------------------------

        public static void setExplosionMode(bool mode)
        {
            isExplosionMode = mode;
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            var brush = Brushes.White;

            switch (color)
            {
                case 0:
                    brush = Brushes.Red;
                    break;

                case 1:
                    brush = Brushes.Yellow;
                    break;

                case 2:
                    brush = Brushes.Blue;
                    break;

                case 3:
                    brush = Brushes.Orange;
                    break;

                case 4:
                    brush = Brushes.Aqua;
                    break;

                case 5:
                    brush = Brushes.Violet;
                    break;
            }

            g.FillRectangle(brush, X, Y, Size, Size);

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            form.Invalidate();
            System.Threading.Thread.Sleep(666);

            // Add static stars
            Count += staticStarsCnt;
            var list = new System.Collections.Generic.List<myObj_100>();

            setExplosionMode(true);

            for (int i = 0; i < Count; i++)
            {
                list.Add(new myObj_100_b());
            }

            setExplosionMode(false);

            while (isAlive)
            {
                g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                foreach (var s in list)
                {
                    s.Show();
                    s.Move();
                }

                System.Threading.Thread.Sleep(33);
                form.Invalidate();

                if (list.Count < Count)
                {
                    list.Add(new myObj_100_b());
                }
            }

            return;
        }
    }

    // ===========================================================================================================
    // ===========================================================================================================

    // Static stars
    public class myObj_100_b : myObj_100
    {
        private int lifeCounter = 0;
        private int alpha = 0;

        public myObj_100_b()
        {
            generateNew();

            if (isExplosionMode)
            {
                Size = rand.Next(3) + 1;
            }
        }

        protected override void generateNew()
        {
            lifeCounter = rand.Next(maxLife) + maxLife;

            if (isExplosionMode)
            {
                lifeCounter = rand.Next(100) + 33;
            }

            X = rand.Next(Width);
            Y = rand.Next(Height);
            color = rand.Next(50);
            alpha = rand.Next(50) + 175;

            max = rand.Next(200) + 100;
            cnt = 0;
            Size = 0;

            {
                int speed = (isExplosionMode ? rand.Next(explosionSpeed) : rand.Next(maxSpeed)) + 1;

                int x0 = Width  / 2;
                int y0 = Height / 2;

                double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));

                double sp_dist = speed / dist;

                dx = (float)((X - x0) * sp_dist);
                dy = (float)((Y - y0) * sp_dist);

                x = x0;
                y = y0;
            }
        }

        protected override void Move()
        {
            x += dx;
            y += dy;

            X = (int)x;
            Y = (int)y;

            if (lifeCounter-- == 0)
            {
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

            br.Color = Color.FromArgb(alpha, 0, 0, 0);
            g.FillRectangle(br, X, Y, Size, Size);
        }
    };

};
