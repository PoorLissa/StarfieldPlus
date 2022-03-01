using System;
using System.Drawing;



namespace my
{
    public class myObj_004 : myObject
    {
        private int dx, dy;

        public myObj_004()
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
            X += dx;
            Y += dy;

            if (X < 0 || X > Width || Y < 0 || Y > Height)
            {
                X = rand.Next(Width);
                Y = rand.Next(Height);

                int speed = rand.Next(20) + 1;

                speed = 2;

                int x0 = Width  / 2;
                int y0 = Height / 2;

                int dist = (int)Math.Sqrt((X - x0)* (X - x0) + (Y - y0) * (Y - y0));

                dx = (X - x0) * speed / dist;
                dy = (Y - y0) * speed / dist;

                Size = 3;
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

            var list = new System.Collections.Generic.List<myObj_004>();

            for (int i = 0; i < Count; i++)
            {
                list.Add(new myObj_004());
            }

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            while (isAlive)
            {
                //g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                foreach (var s in list)
                {
                    g.FillRectangle(Brushes.White, s.X, s.Y, s.Size, s.Size);
                    s.Move();
                }

                form.Invalidate();
                //System.Threading.Thread.Sleep(50);
                System.Threading.Thread.Sleep(5);
            }

            g.Dispose();
            isAlive = true;

            return;
        }
    };
};
