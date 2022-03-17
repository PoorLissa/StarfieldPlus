using System;
using System.Drawing;

/*
    - moving vertical and horizontal lines
*/

namespace my
{
    public class myObj_130 : myObject
    {
        static Pen p = null;
        static SolidBrush br = null;
        static myColorPicker colorPicker = null;
        static int shape = 0;
        static int moveMode = 0;
        static int A_Filling = 0;

        int maxSize = 0, A = 0, R = 0, G = 0, B = 0, dSize = 0, dA = 0, mult = 0;

        // -------------------------------------------------------------------------

        public myObj_130()
        {
            if (p == null)
            {
                p = new Pen(Color.Red);
                br = new SolidBrush(Color.Red);
                colorPicker = new myColorPicker(Width, Height);

                moveMode = rand.Next(4);
                shape = rand.Next(6);
                A_Filling = rand.Next(11) + 1;
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected virtual void generateNew()
        {
            X = rand.Next(Width);
            Y = rand.Next(Height);

            Size = 1;
            maxSize = rand.Next(333) + 33;
            dSize = rand.Next(5) + 1;
            dA = rand.Next(5) + 1;
            mult = rand.Next(3) + 1;

            A = rand.Next(256);
            colorPicker.getColor(X, Y, ref R, ref G, ref B);

            return;
        }

        // -------------------------------------------------------------------------

        public override void Move()
        {
            switch (moveMode)
            {
                case 0:
                    move1();
                    break;

                case 1:
                    move2();
                    break;

                case 2:
                    move3();
                    break;

                case 3:
                    move4();
                    break;
            }
        }

        // -------------------------------------------------------------------------

        private void move1()
        {
            Size += dSize;

            // Increase disappearing speed when max size is reached
            if (Size > maxSize)
                dA++;

            // Decrease opacity until fully invisible
            A -= dA;

            if (A < 0)
            {
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        private void move2()
        {
            X += (rand.Next(3) - 1) * mult;
            Y += (rand.Next(3) - 1) * mult;

            Size += dSize;

            // Increase disappearing speed when max size is reached
            if (Size > maxSize)
                dA++;

            // Decrease opacity until fully invisible
            A -= dA;

            if (A < 0)
            {
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        private void move3()
        {
            X += (rand.Next(3) - 1) * mult * 5;
            Y += (rand.Next(3) - 1) * mult * 5;

            Size += dSize;

            // Increase disappearing speed when max size is reached
            if (Size > maxSize)
                dA++;

            // Decrease opacity until fully invisible
            A -= dA;

            if (A < 0)
            {
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        private void move4()
        {
            //X += (int)(Math.Sin(Y)*10);
            //Y += (int)(Math.Cos(X)*10);

            //X += (int)(Math.Sin(Y) * Math.Sin(Y) * 10);
            //Y += (int)(Math.Cos(X) * Math.Cos(X) * 10);

            //X += (int)(Math.Sin(X) * Math.Sin(X) * 10);
            //Y += (int)(Math.Cos(Y) * Math.Cos(Y) * 10);

            //X += (int)(Math.Sin(X) * Math.Cos(X) * 10);
            //Y += (int)(Math.Sin(Y) * Math.Cos(Y) * 10);

            //X += (int)(Math.Sin(X) * Math.Cos(X) * 10);
            //Y += (int)(Math.Sin(Y) * Math.Cos(Y) * 10);
            //X += (int)(Math.Tan(X) * 10);

            X += (int)(Math.Sin(X) * Math.Cos(X) * 10);
            Y += (int)(Math.Sin(Y) * Math.Cos(Y) * 10);
            X += (int)(Math.Tan(X) * 10);
            Y += (int)(Math.Tan(Y) * 10);

            Size += dSize;

            // Increase disappearing speed when max size is reached
            if (Size > maxSize)
                dA++;

            // Decrease opacity until fully invisible
            A -= dA;

            if (A < 0)
            {
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        protected void Show(Graphics g)
        {
            p.Color = Color.FromArgb(A, R, G, B);

            switch (shape)
            {
                case 0:
                    g.DrawEllipse(p, X - Size, Y - Size, 2 * Size, 2 * Size);
                    break;

                case 1:
                    g.DrawRectangle(p, X - Size, Y - Size, 2 * Size, 2 * Size);
                    break;

                case 2:
                    br.Color = Color.FromArgb(A_Filling, R, G, B);
                    g.FillEllipse(br, X - Size, Y - Size, 2 * Size, 2 * Size);
                    break;

                case 3:
                    br.Color = Color.FromArgb(A_Filling, R, G, B);
                    g.FillRectangle(br, X - Size, Y - Size, 2 * Size, 2 * Size);
                    break;

                case 4:
                    br.Color = Color.FromArgb(A_Filling, R, G, B);
                    g.FillEllipse(br, X - Size, Y - Size, 2 * Size, 2 * Size);
                    g.DrawEllipse(p, X - Size, Y - Size, 2 * Size, 2 * Size);
                    break;

                case 5:
                    br.Color = Color.FromArgb(A_Filling, R, G, B);
                    g.FillRectangle(br, X - Size, Y - Size, 2 * Size, 2 * Size);
                    g.DrawRectangle(p, X - Size, Y - Size, 2 * Size, 2 * Size);
                    break;
            }
        }

        // -------------------------------------------------------------------------

        public override void Process(System.Windows.Forms.Form form, ref bool isAlive)
        {
            Bitmap buffer = new Bitmap(Width, Height);      // set the size of the image
            Graphics g = Graphics.FromImage(buffer);        // set the graphics to draw on the image
            form.BackgroundImage = buffer;                  // set the PictureBox's image to be the buffer

            int t = 50;
            int num = 10;

            var list = new System.Collections.Generic.List<myObj_130>();

            list.Add(new myObj_130());

            if (rand.Next(2) == 0)
            {
                g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            }

            while (isAlive)
            {
                foreach (var s in list)
                {
                    s.Show(g);
                    s.Move();
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);

                if(list.Count < num)
                {
                    list.Add(new myObj_130());
                }
            }

            g.Dispose();
            isAlive = true;

            return;
        }
    }
};
