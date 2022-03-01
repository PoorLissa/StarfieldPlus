using System;
using System.Drawing;



namespace my
{
    public class myObj_002 : myObject
    {
        private int dx, dy;

        public myObj_002()
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

            var list = new System.Collections.Generic.List<myObj_002>();

            for (int i = 0; i < Count; i++)
            {
                list.Add(new myObj_002());
            }

            while (isAlive)
            {
                g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                foreach (var s in list)
                {
                    g.FillRectangle(Brushes.Yellow, s.X, s.Y, s.Size, s.Size);
                    s.Move();
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(50);
            }

            g.Dispose();
            isAlive = true;

            return;
        }
    };
};
