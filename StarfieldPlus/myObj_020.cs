using System;
using System.Drawing;


/*
    - Linearly Moving Circles (Soap Bubbles)
*/


namespace my
{
    public class myObj_020 : myObject
    {
        static Pen p = null;
        static SolidBrush br = null;
        static int shape = 0;

        float x, y, dx, dy;
        int cnt = 0, A = 0, R = 0, G = 0, B = 0, lifeCounter = 0, growSpeed = 1, drawMode = 0, A_Filling = 0;

        // -------------------------------------------------------------------------

        public myObj_020()
        {
            if (p == null)
            {
                p = new Pen(Color.Red);
                br = new SolidBrush(Color.Red);
                colorPicker = new myColorPicker(Width, Height);
                shape = rand.Next(2);
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected virtual void generateNew()
        {
            lifeCounter = rand.Next(100) + 100;

            int x0 = rand.Next(Width);
            int y0 = rand.Next(Height);
            int speed = rand.Next(30) + 50;
            growSpeed = rand.Next(5) + 1;
            cnt = growSpeed;
            drawMode = rand.Next(2);

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

            A = rand.Next(250) + 6;
            A_Filling = A / (rand.Next(10) + 2);

            colorPicker.getColor(X, Y, ref R, ref G, ref B);
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            if (X != -1111)
            {
                if (cnt-- == 0)
                {
                    Size += 3;
                    cnt = growSpeed;
                }

                x += dx;
                y += dy;

                X = (int)x;
                Y = (int)y;

                if (X < -Size || X > Width + Size || Y < -Size || Y > Height + Size)
                {
                    X = -1111;
                    Y = -1111;
                    Size = 0;
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

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            if (Size > 0)
            {
                p.Color = Color.FromArgb(A, R, G, B);

                switch (shape)
                {
                    case 0:
                        g.DrawEllipse(p, X, Y, Size, Size);

                        if (drawMode == 1)
                        {
                            br.Color = Color.FromArgb(A_Filling, R, G, B);
                            g.FillEllipse(br, X, Y, Size, Size);
                        }
                        break;

                    case 1:
                        g.DrawRectangle(p, X, Y, Size, Size);

                        if (drawMode == 1)
                        {
                            br.Color = Color.FromArgb(A_Filling, R, G, B);
                            g.FillRectangle(br, X, Y, Size, Size);
                        }
                        break;
                }
            }
        }

        // -------------------------------------------------------------------------

        // Using form's background image as our drawing surface
        protected override void Process(System.Windows.Forms.Form form, Graphics g, ref bool isAlive)
        {
            var list = new System.Collections.Generic.List<myObj_020>();

            while (isAlive)
            {
                g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                foreach (var s in list)
                {
                    s.Show();
                    s.Move();
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(50);

                // Gradually increase number of objects, until the limit is reached
                if (list.Count < Count)
                {
                    list.Add(new myObj_020());
                }
            }

            return;
        }
    };
};
