using System;
using System.Drawing;


/*
    - Randomly Roaming Squares (Snow Like)
*/


namespace my
{
    public class myObj_011 : myObject
    {
        private int dx1, dy1, dx2, dy2, x, y;
        int A = 0, R = 0, G = 0, B = 0;



        public myObj_011()
        {
            if (colorPicker == null)
            {
                p = new Pen(Color.Red);
                br = new SolidBrush(Color.Red);
                colorPicker = new myColorPicker(Width, Height);

                Log($"myObj_011: colorPicker({colorPicker.getMode()})");
            }

            x = rand.Next(Width);
            y = rand.Next(Height);
            X = rand.Next(Width);
            Y = rand.Next(Height);

            int maxSpeed = 20;

            dx1 = (rand.Next(maxSpeed) + 1) * (rand.Next(2) == 0 ? 1 : -1);
            dy1 = (rand.Next(maxSpeed) + 1) * (rand.Next(2) == 0 ? 1 : -1);
            dx2 = (rand.Next(maxSpeed) + 1) * (rand.Next(2) == 0 ? 1 : -1);
            dy2 = (rand.Next(maxSpeed) + 1) * (rand.Next(2) == 0 ? 1 : -1);

            Size = 1;

            A = rand.Next(256 - 75) + 75;
            colorPicker.getColor(X, Y, ref R, ref G, ref B);
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            X += dx1;
            Y += dy1;
            x += dx2;
            y += dy2;

            if (X < 0 || X > Width)
            {
                dx1 *= -1;
            }

            if (Y < 0 || Y > Height)
            {
                dy1 *= -1;
            }

            if (x < 0 || x > Width)
            {
                dx2 *= -1;
            }

            if (y < 0 || y > Height)
            {
                dy2 *= -1;
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            p.Color = Color.FromArgb(33, R, G, B);

            g.DrawLine(p, X, Y, x, y);
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int t = 50, cnt = 0, Cnt = 10;

            var list = new System.Collections.Generic.List<myObj_011>();

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            while (isAlive)
            {
                // Darken all the picture
                if (++cnt > 5000)
                {
                    br.Color = Color.FromArgb(1, 0, 0, 0);
                    g.FillRectangle(br, 0, 0, Width, Height);

                    if (cnt > 5200)
                    {
                        cnt = 0;
                    }
                }

                if (list.Count < Cnt)
                {
                    list.Add(new myObj_011());
                }

                foreach (var s in list)
                {
                    s.Show();
                    s.Move();
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);
            }

            return;
        }
    };
};
