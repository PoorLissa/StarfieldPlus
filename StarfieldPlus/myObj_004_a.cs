using System;
using System.Drawing;



namespace my
{
    public class myObj_004_a : myObject
    {
        private int dx, dy;

        public myObj_004_a()
        {
            X = rand.Next(Width);
            Y = rand.Next(Height);

            int speed = rand.Next(20) + 1;

            speed = 2;

            int x0 = Width / 2;
            int y0 = Height / 2;

            int dist = (int)Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));

            dx = (X - x0) * speed / dist;
            dy = (Y - y0) * speed / dist;

            Size = 3;
        }

        // -------------------------------------------------------------------------

        public override void Move()
        {
            X += dx + (int)(Math.Sin(Y) * 5);
            Y += dy + (int)(Math.Sin(X) * 5);

            if (X < 0 || X > Width || Y < 0 || Y > Height)
            {
                X = rand.Next(Width);
                Y = rand.Next(Height);

                int speed = 5;

                int x0 = Width  / 2;
                int y0 = Height / 2;

                //int dist = (int)Math.Sqrt((X - x0)*(X - x0) + (Y - y0)*(Y - y0));

                double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));

                dx = (int)((X - x0) * speed / dist);
                dy = (int)((Y - y0) * speed / dist);

                Size = 3;
            }

            return;
        }

        // -------------------------------------------------------------------------

        // Using form's background image as our drawing surface
        public override void Process(System.Windows.Forms.Form form, ref bool isAlive)
        {
            Bitmap buffer = new Bitmap(Width, Height);      // set the size of the image
            Graphics g = Graphics.FromImage(buffer);        // set the graphics to draw on the image
            form.BackgroundImage = buffer;                  // set the PictureBox's image to be the buffer

            int cnt = 0;

            var list = new System.Collections.Generic.List<myObj_004>();

            for (int i = 0; i < Count; i++)
            {
                list.Add(new myObj_004());
            }

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            using (SolidBrush br = new SolidBrush(Color.FromArgb(250, 250, 250, 250)))
            {
                while (isAlive)
                {
                    //g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                    foreach (var s in list)
                    {
                        g.FillRectangle(br, s.X, s.Y, s.Size, s.Size);
                        s.Move();
                    }

                    form.Invalidate();
                    System.Threading.Thread.Sleep(33);

                    if (++cnt > 1000)
                    {
                        getNewBrush(br);
                        cnt = 0;
                    }
                }
            }

            g.Dispose();
            isAlive = true;

            return;
        }

        // -------------------------------------------------------------------------

        private void getNewBrush(SolidBrush br)
        {
            int alpha = 0, R = 0, G = 0, B = 0, max = 256;

            while (alpha + R + G + B < 100)
            {
                alpha = rand.Next(max - 75) + 75;
                R = rand.Next(max);
                G = rand.Next(max);
                B = rand.Next(max);
            }

            br.Color = Color.FromArgb(alpha, R, G, B);
        }
    };
};
