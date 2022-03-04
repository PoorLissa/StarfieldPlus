using System;
using System.Drawing;



namespace my
{
    public class myObj_000 : myObject
    {
        protected float x, y, dx, dy;
        protected int cnt = 0;
        protected int max = 0;
        protected int color = 0;

        // -------------------------------------------------------------------------

        public myObj_000()
        {
            generateNew();
        }

        // -------------------------------------------------------------------------

        protected virtual void generateNew()
        {
        }

        // -------------------------------------------------------------------------

        protected virtual void Show(Graphics g)
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

        // Using form's background image as our drawing surface
        public static void Process(System.Windows.Forms.Form form, ref bool isAlive)
        {
            var list = new System.Collections.Generic.List<myObj_000>();

            Bitmap buffer = new Bitmap(Width, Height);      // set the size of the image
            Graphics g = Graphics.FromImage(buffer);        // set the graphics to draw on the image
            form.BackgroundImage = buffer;                  // set the PictureBox's image to be the buffer

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

            while (isAlive)
            {
                g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                foreach (var s in list)
                {
                    s.Show(g);
                    s.Move();
                }

                System.Threading.Thread.Sleep(33);
                form.Invalidate();

                // Gradually increase number of moving stars, until the limit is reached
                if (list.Count < Count)
                {
                    list.Add(new myObj_000_a());
                }
            }

            g.Dispose();
            isAlive = true;

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
            X = rand.Next(Width);
            Y = rand.Next(Height);
            max = rand.Next(75) + 20;
            cnt = 0;
            color = rand.Next(50);

            x = X;
            y = Y;

            int speed = rand.Next(10) + 1;

            int x0 = Width / 2;
            int y0 = Height / 2;

            double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));

            double sp_dist = speed / dist;

            dx = (float)((X - x0) * sp_dist);
            dy = (float)((Y - y0) * sp_dist);

            Size = 0;
        }

        // -------------------------------------------------------------------------

        public override void Move()
        {
            x += dx;
            y += dy;

            X = (int)x;
            Y = (int)y;

            if (cnt++ > max)
            {
                cnt = 0;
                Size++;
            }

#if false
            float dAcc = 1.0f + (1.0f * acc / 1000.0f);
            dx *= dAcc;
            dy *= dAcc;
#else
            dx *= 1.005f;
            dy *= 1.005f;
#endif
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
        static SolidBrush br = new SolidBrush(Color.Black);

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
                int x0 = Width  / 2;
                int y0 = Height / 2;

                double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));
                double sp_dist = 0.25 / dist;

                dx = (float)((X - x0) * sp_dist);
                dy = (float)((Y - y0) * sp_dist);

                x = X;
                y = Y;
            }
        }

        public override void Move()
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

        protected override void Show(Graphics g)
        {
            // Draw static stars ...
            base.Show(g);

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

    // ===========================================================================================================
    // ===========================================================================================================

    // Comets
    public class myObj_000_c : myObj_000
    {
        private int lifeCounter = 0;
        private int cnt = 0;

        protected override void generateNew()
        {
            //lifeCounter = (rand.Next(500) + 500) * factor;
            //lifeCounter = rand.Next(500) + 500;

            lifeCounter = 100;
            cnt = 5;

            int x0 = rand.Next(Width);
            int y0 = rand.Next(Height);
            int speed = rand.Next(30) + 50;

            speed = 20;

            do
            {
                X = rand.Next(Width);
                Y = rand.Next(Height);
            }
            while (X == x0 && Y == y0);

            double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));
            double sp_dist = speed / dist;

            dx = (float)((X - x0) * sp_dist);
            dy = (float)((Y - y0) * sp_dist);

            x = X;
            y = Y;

            Size = 1;
        }

        public override void Move()
        {
            if (X != -11)
            {
                if (cnt-- == 0)
                {
                    Size += 1;
                    cnt = 5;
                }

                x += dx;
                y += dy;

                X = (int)x;
                Y = (int)y;

                if (X < 0 || X > Width || Y < 0 || Y > Height)
                {
                    X = -11;
                    Y = -11;
                }
            }
            else
            {
                if (lifeCounter-- == 0)
                {
                    generateNew();
                }
            }

            return;
        }

        protected override void Show(Graphics g)
        {
            g.DrawEllipse(Pens.DarkOrange, X, Y, Size, Size);
            //g.FillRectangle(Brushes.OrangeRed, X, Y, Size, Size);
        }

    };

};
