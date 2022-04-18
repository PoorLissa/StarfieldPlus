using System;
using System.Drawing;
using System.Collections.Generic;

/*
    - Big Bang
*/

namespace my
{
    public class myObj_100 : myObject
    {
        protected static int maxLife = 500, maxSpeed = 0, explosionSpeed = 0, staticStarsCnt = 1111;
        protected static int x0 = 0, y0 = 0;
        protected static bool isExplosionMode = false;

        static List<myObject> list = null;

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
                f = new Font("Segoe UI", 9, FontStyle.Regular, GraphicsUnit.Point);
                list = new List<myObject>();

                x0 = Width  / 2;
                y0 = Height / 2;

                maxSpeed = rand.Next(11);
                explosionSpeed = rand.Next(100) + 33;

                switch (rand.Next(5))
                {
                    // Original mode with very slow expansion -- no explosion
                    case 0:
                        maxSpeed = 0;
                        explosionSpeed = 0;
                        break;

                    // Original mode with very slow expansion -- plus explosion
                    case 1:
                        maxSpeed = 0;
                        break;
                }
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
                case 0: brush = Brushes.Red;    break;
                case 1: brush = Brushes.Yellow; break;
                case 2: brush = Brushes.Blue;   break;
                case 3: brush = Brushes.Orange; break;
                case 4: brush = Brushes.Aqua;   break;
                case 5: brush = Brushes.Violet; break;
            }

            g.FillRectangle(brush, X, Y, Size, Size);

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            string strInfo = "";

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            form.Invalidate();
            System.Threading.Thread.Sleep(666);

            // Add static stars
            Count += staticStarsCnt;

            setExplosionMode(true);

            for (int i = 0; i < Count; i++)
            {
                list.Add(new myObj_100_b());
            }

            setExplosionMode(false);

            while (isAlive)
            {
                g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                foreach (myObj_100_b s in list)
                {
                    s.Show();
                    s.Move();
                }

                // Display some info
                if (my.myObject.ShowInfo)
                {
                    if (cnt % 1 == 0 || strInfo.Length == 0)
                    {
                        strInfo = $" obj = myObj_100\n maxSpeed = {maxSpeed}\n explosionSpeed = {explosionSpeed}";
                        g.FillRectangle(Brushes.Black, 30, 33, 155, 150);
                        g.DrawString(strInfo, f, Brushes.Red, 35, 33);
                    }
                }
                else
                {
                    if (strInfo.Length > 0)
                    {
                        g.FillRectangle(Brushes.Black, 30, 33, 155, 150);
                        strInfo = string.Empty;
                    }
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(33);

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


    // Stars
    public class myObj_100_b : myObj_100
    {
        private int lifeCounter = 0;
        private int alpha = 0;

        // -------------------------------------------------------------------------

        public myObj_100_b()
        {
            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            lifeCounter = rand.Next(maxLife) + maxLife;

            X = rand.Next(Width);
            Y = rand.Next(Width);
            color = rand.Next(50);
            alpha = rand.Next(50) + 175;

            max = rand.Next(200) + 100;
            cnt = 0;
            Size = 0;

            int speed = 1 + (isExplosionMode ? rand.Next(explosionSpeed) : rand.Next(maxSpeed));

            // As X and Y are generated within a square [Width x Width],
            // both dx and dy will be calculated using point [x0, x0]
            double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - x0) * (Y - x0));
            double sp_dist = speed / dist;

            dx = (float)((X - x0) * sp_dist);
            dy = (float)((Y - x0) * sp_dist);

            // Move each start to the center point:
            x = x0;
            y = y0;

            if (isExplosionMode)
            {
                Size = rand.Next(3) + 1;
                lifeCounter = rand.Next(100) + 33;
            }
        }

        // -------------------------------------------------------------------------

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

        // -------------------------------------------------------------------------

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

        // -------------------------------------------------------------------------
    };

};
