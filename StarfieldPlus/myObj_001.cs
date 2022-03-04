using System;
using System.Drawing;



namespace my
{
    public class myObj_001 : myObject
    {
        private int dx, dy;

        public myObj_001()
        {
            X = rand.Next(Width);
            Y = rand.Next(Height);

            int speed = 20;

            dx = (rand.Next(speed) + 1) * (rand.Next(2) == 0 ? 1 : -1);
            dy = (rand.Next(speed) + 1) * (rand.Next(2) == 0 ? 1 : -1);

            Size = rand.Next(11) + 1;
        }

        // -------------------------------------------------------------------------

        public override void Move()
        {
            X += dx;
            Y += dy;

            if (X < 0 || X > Width)
            {
                dx *= -1;
            }

            if (Y < 0 || Y > Height)
            {
                dy *= -1;
            }

            return;
        }

        // -------------------------------------------------------------------------

        // Using form's background image as our drawing surface
        public static void Process(System.Windows.Forms.Form form, ref bool isAlive)
        {
            Bitmap buffer = new Bitmap(Width, Height);      // set the size of the image
            Graphics g = Graphics.FromImage(buffer);        // set the graphics to draw on the image
            form.BackgroundImage = buffer;                  // set the PictureBox's image to be the buffer

            var list = new System.Collections.Generic.List<myObj_001>();

            for (int i = 0; i < Count; i++)
            {
                list.Add(new myObj_001());
            }

            int alpha = 0, R = 0, G = 0, B = 0, max = 256;

            while (alpha + R + G + B < 100)
            {
                alpha = rand.Next(max-75) + 75;
                R = rand.Next(max);
                G = rand.Next(max);
                B = rand.Next(max);
            }

            using (Brush br = new SolidBrush(Color.FromArgb(alpha, R, G, B)))
            {
                while (isAlive)
                {
                    g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                    foreach (var s in list)
                    {
                        g.FillRectangle(br, s.X, s.Y, s.Size, s.Size);

                        if (s.Size > 3)
                        {
                            g.FillRectangle(Brushes.Black, s.X,              s.Y,              1, 1);
                            g.FillRectangle(Brushes.Black, s.X + s.Size - 1, s.Y,              1, 1);
                            g.FillRectangle(Brushes.Black, s.X,              s.Y + s.Size - 1, 1, 1);
                            g.FillRectangle(Brushes.Black, s.X + s.Size - 1, s.Y + s.Size - 1, 1, 1);
                        }

                        s.Move();
                    }

                    form.Invalidate();
                    System.Threading.Thread.Sleep(50);
                }
            }

            g.Dispose();
            isAlive = true;

            return;
        }
    };
};
