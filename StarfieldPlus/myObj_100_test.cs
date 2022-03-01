using System;
using System.Drawing;



namespace my
{
    public class myObj_100 : myObject
    {
        protected float x, y, dx, dy;
        protected int cnt = 0;
        protected int max = 0;
        protected int color = 0;

        // -------------------------------------------------------------------------

        public myObj_100()
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
            var list = new System.Collections.Generic.List<myObj_100>();

            Bitmap buffer = new Bitmap(Width, Height);      // set the size of the image
            Graphics g = Graphics.FromImage(buffer);        // set the graphics to draw on the image
            form.BackgroundImage = buffer;                  // set the PictureBox's image to be the buffer

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            form.Invalidate();
            System.Threading.Thread.Sleep(666);

            int staticStarsCnt = 1111;

            // Add static stars
            Count += staticStarsCnt;

            for (int i = 0; i < staticStarsCnt; i++)
            {
                list.Add(new myObj_100_b());
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
            }

            g.Dispose();
            isAlive = true;

            return;
        }
    }

    // ===========================================================================================================
    // ===========================================================================================================

    // Static stars
    public class myObj_100_b : myObj_100
    {
        static SolidBrush br = new SolidBrush(Color.Black);

        private int lifeCounter = 0;
        private int alpha = 0;

        protected override void generateNew()
        {
            lifeCounter = rand.Next(500) + 500;

            X = rand.Next(Width);
            Y = rand.Next(Height);
            color = rand.Next(50);
            alpha = rand.Next(50) + 175;

            max = rand.Next(200) + 100;
            cnt = 0;
            Size = 0;

            {
                int speed = 1;

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

        public override void Move()
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

};
