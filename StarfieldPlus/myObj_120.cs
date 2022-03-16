using System;
using System.Drawing;

/*
    - moving vertical and horizontal lines
*/

namespace my
{
    public class myObj_120 : myObject
    {
        Pen p = null;
        int length = 0, dir = 0, dx = 0, dy = 0;

        // -------------------------------------------------------------------------

        public myObj_120()
        {
            generateNew();
        }

        // -------------------------------------------------------------------------

        protected virtual void generateNew()
        {
            length = rand.Next(1000) + 333;
            dir = rand.Next(4);

            X = rand.Next(Width);
            Y = rand.Next(Height);

            switch (dir)
            {
                case 0:
                    dx = 1;
                    dy = 0;
                    X = -33 - length;
                    break;

                case 1:
                    dx = -1;
                    dy = 0;
                    X = Width + 33 + length;
                    break;

                case 2:
                    dx = 0;
                    dy = 1;
                    Y = -33 - length;
                    break;

                case 3:
                    dx = 0;
                    dy = -1;
                    Y = Height + 33 + length;
                    break;
            }

            dx *= 7;
            dy *= 7;

            return;
        }

        // -------------------------------------------------------------------------

        public override void Move()
        {
            X += dx;
            Y += dy;

            if (dir == 0 && X > Width)
            {
                generateNew();
            }

            if (dir == 1 && X < 0)
            {
                generateNew();
            }

            if (dir == 2 && Y > Height)
            {
                generateNew();
            }

            if (dir == 3 && Y < 0)
            {
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        protected void Show(Graphics g)
        {
            int x2 = 0, y2 = 0;

            switch (dir)
            {
                case 0:
                    x2 = X + length;
                    y2 = Y;
                    break;

                case 1:
                    x2 = X - length;
                    y2 = Y;
                    break;

                case 2:
                    x2 = X;
                    y2 = Y + length;
                    break;

                case 3:
                    x2 = X;
                    y2 = Y - length;
                    break;
            }

            g.DrawLine(Pens.Gray, X, Y, x2, y2);
        }

        // -------------------------------------------------------------------------

        public override void Process(System.Windows.Forms.Form form, ref bool isAlive)
        {
            Bitmap buffer = new Bitmap(Width, Height);      // set the size of the image
            Graphics g = Graphics.FromImage(buffer);        // set the graphics to draw on the image
            form.BackgroundImage = buffer;                  // set the PictureBox's image to be the buffer

            int t = 50;

            var list = new System.Collections.Generic.List<myObj_120>();

            for (int i = 0; i < 33; i++)
            {
                list.Add(new myObj_120());
            }

            while (isAlive)
            {
                g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                foreach (var s in list)
                {
                    s.Show(g);
                    s.Move();
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);
            }

            g.Dispose();
            isAlive = true;

            return;
        }
    }
};
