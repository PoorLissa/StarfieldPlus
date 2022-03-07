using System;
using System.Drawing;



namespace my
{
    public class myObj_002 : myObject
    {
        protected float x, y, dx, dy;
        protected int cnt = 0;
        protected int max = 0;
        protected int color = 0;

        public myObj_002()
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
        }

        // -------------------------------------------------------------------------

        // Using form's background image as our drawing surface
        public static void Process(System.Windows.Forms.Form form, ref bool isAlive)
        {
            Bitmap buffer = new Bitmap(Width, Height);      // set the size of the image
            Graphics g = Graphics.FromImage(buffer);        // set the graphics to draw on the image
            form.BackgroundImage = buffer;                  // set the PictureBox's image to be the buffer

            var list = new System.Collections.Generic.List<myObj_002>();
            list.Add(new myObj_002_a());

            int alpha = rand.Next(255);
            int R     = rand.Next(255);
            int G     = rand.Next(255);
            int B     = rand.Next(255);

            using (Brush br = new SolidBrush(Color.FromArgb(alpha, R, G, B)))
            {
                while (isAlive)
                {
                    g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                    foreach (var s in list)
                    {
                        s.Show(g);
                        s.Move();
                    }

                    form.Invalidate();
                    System.Threading.Thread.Sleep(50);

                    // Gradually increase number of moving stars, until the limit is reached
                    if (list.Count < Count)
                    {
                        list.Add(new myObj_002_a());
                    }
                }
            }

            g.Dispose();
            isAlive = true;

            return;
        }
    };

    // ===========================================================================================================
    // ===========================================================================================================

    public class myObj_002_a : myObj_002
    {
        private int lifeCounter = 0;
        private int cnt = 0;
        private int growSpeed = 1;
        private int alpha = 0;
        private static Pen p = null;

        protected override void generateNew()
        {
            lifeCounter = rand.Next(100) + 100;
            cnt = 5;

            int x0 = rand.Next(Width);
            int y0 = rand.Next(Height);
            int speed = rand.Next(30) + 50;
            growSpeed = rand.Next(3) + 1;

            speed = rand.Next(20) + 10;

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

            int R = 0, G = 0, B = 0, max = 256;
            alpha = rand.Next(max - 75) + 75;

            if (p == null)
            {
                p = new Pen(Color.Black);

                while (R + G + B < 100)
                {
                    R = rand.Next(max);
                    G = rand.Next(max);
                    B = rand.Next(max);
                }

                p.Color = Color.FromArgb(255, R, G, B);
            }
        }

        public override void Move()
        {
            if (X != -111)
            {
                if (cnt-- == 0)
                {
                    Size += growSpeed;
                    cnt = 2;
                }

                x += dx;
                y += dy;

                X = (int)x;
                Y = (int)y;

                if (X < -10 || X > Width || Y < -10 || Y > Height)
                {
                    X = -111;
                    Y = -111;
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
            p.Color = Color.FromArgb(alpha, p.Color.R, p.Color.G, p.Color.B);
            g.DrawEllipse(p, X, Y, Size, Size);

//            g.DrawEllipse(Pens.DarkOrange, X, Y, Size + 2, Size + 2);
        }

    };
};
