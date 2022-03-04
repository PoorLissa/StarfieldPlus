using System;
using System.Drawing;



namespace my
{
    public class myObj_003 : myObject
    {
        protected float x, y, dx, dy;
        protected int cnt = 0;
        protected int max = 0;
        protected int color = 0;

        public static SolidBrush br = new SolidBrush(Color.White);

        public myObj_003()
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

            var list = new System.Collections.Generic.List<myObj_003>();

            int R = 0, G = 0, B = 0, max = 256;

            while (R + G + B < 100)
            {
                R = rand.Next(max);
                G = rand.Next(max);
                B = rand.Next(max);
            }

            br.Color = Color.FromArgb(100, R, G, B);

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

                if (list.Count < Count)
                {
                    list.Add(new myObj_003_a());
                }
            }

            br.Dispose();
            g.Dispose();
            isAlive = true;

            return;
        }
    };

    // ===========================================================================================================
    // ===========================================================================================================

    public class myObj_003_a : myObj_003
    {
        private int lifeCounter = -1;
        private int alpha = 0;

        protected override void generateNew()
        {
            if (lifeCounter == -1)
            {
                lifeCounter = rand.Next(100);
            }
            else
            {
                lifeCounter = rand.Next(100) + 100;
            }

            X = rand.Next(Width);
            Y = 11;

            x = X;
            y = Y;

            dx = 0;
            dy = 0;

            Size = rand.Next(10) + 1;
            alpha = rand.Next(256);
        }

        public override void Move()
        {
            if (dy != 0)
            {
                y += dy;
                Y = (int)y;

                dy += (1.0f + Size / 10.0f);

                if (Y >= Height)
                {
                    generateNew();
                }
            }
            else
            {
                if (lifeCounter-- == 0)
                {
                    dy = 0.0001f;
                }
            }

            return;
        }

        protected override void Show(Graphics g)
        {
            br.Color = Color.FromArgb(alpha, br.Color.R, br.Color.G, br.Color.B);

            g.FillRectangle(br, X, Y, Size, Size);
        }

    };
};
