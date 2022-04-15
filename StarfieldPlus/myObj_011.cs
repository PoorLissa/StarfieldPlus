using System;
using System.Drawing;
using System.Collections.Generic;


/*
    - Randomly Roaming Squares (Snow Like)
*/


namespace my
{
    public class myObj_011 : myObject
    {
        static int removeTraces = 0, mode = 0, t = 0;
        static List<myObject> list = null;

        private int dx1, dy1, dx2, dy2, x, y;
        int A = 0, R = 0, G = 0, B = 0;

        private float dfx1, dfy1, dfx2, dfy2, fx1, fy1, fx2, fy2;


        public myObj_011()
        {
            if (colorPicker == null)
            {
                p = new Pen(Color.Red);
                br = new SolidBrush(Color.Red);
                colorPicker = new myColorPicker(Width, Height);
                list = new List<myObject>();

                t = 50;
                mode = rand.Next(2);

                // floats are a bit slower, so make [t] less as well
                if (mode == 1)
                {
                    t = 11 + rand.Next(22);
                }

                removeTraces = rand.Next(3);

                Log($"myObj_011: colorPicker({colorPicker.getMode()})");
            }

            // ints
            x = rand.Next(Width);
            X = rand.Next(Width);
            y = rand.Next(Height);
            Y = rand.Next(Height);

            // floats
            fx1 = rand.Next(Width);
            fy1 = rand.Next(Width);
            fx2 = rand.Next(Height);
            fy2 = rand.Next(Height);

            int maxSpeed = 20;
            float dSpeed = 0.05f;

            dx1 = (rand.Next(maxSpeed) + 1) * my.myUtils.getRandomSign(rand);
            dy1 = (rand.Next(maxSpeed) + 1) * my.myUtils.getRandomSign(rand);
            dx2 = (rand.Next(maxSpeed) + 1) * my.myUtils.getRandomSign(rand);
            dy2 = (rand.Next(maxSpeed) + 1) * my.myUtils.getRandomSign(rand);

            maxSpeed = 200;

            dfx1 = (rand.Next(maxSpeed) + 1) * my.myUtils.getRandomSign(rand) * dSpeed;
            dfx2 = (rand.Next(maxSpeed) + 1) * my.myUtils.getRandomSign(rand) * dSpeed;
            dfy1 = (rand.Next(maxSpeed) + 1) * my.myUtils.getRandomSign(rand) * dSpeed;
            dfy2 = (rand.Next(maxSpeed) + 1) * my.myUtils.getRandomSign(rand) * dSpeed;

            Size = 1;

            if (removeTraces == 1)
            {
                A = rand.Next(66) + 66;
            }
            else
            {
                A = rand.Next(33) + 33;
            }
            
            colorPicker.getColor(X, Y, ref R, ref G, ref B);
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            switch (mode)
            {
                case 0:
                    X += dx1;
                    Y += dy1;
                    x += dx2;
                    y += dy2;

                    if (X < 0 || X > Width)
                        dx1 *= -1;

                    if (Y < 0 || Y > Height)
                        dy1 *= -1;

                    if (x < 0 || x > Width)
                        dx2 *= -1;

                    if (y < 0 || y > Height)
                        dy2 *= -1;
                    break;

                case 1:
                    fx1 += dfx1;
                    fy1 += dfy1;
                    fx2 += dfx2;
                    fy2 += dfy2;

                    if (fx1 < 0 || fx1 > Width)
                        dfx1 *= -1;

                    if (fy1 < 0 || fy1 > Height)
                        dfy1 *= -1;

                    if (fx2 < 0 || fx2 > Width)
                        dfx2 *= -1;

                    if (fy2 < 0 || fy2 > Height)
                        dfy2 *= -1;
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            p.Color = Color.FromArgb(A, R, G, B);

            switch (mode)
            {
                case 0:
                    g.DrawLine(p, X, Y, x, y);
                    break;

                case 1:
                    g.DrawLine(p, fx1, fy1, fx2, fy2);
                    //g.DrawRectangle(Pens.DarkRed, fx1-1, fy1-1, 3, 3);
                    //g.DrawRectangle(Pens.DarkOrange, fx2-1, fy2-1, 3, 3);
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int cnt = 0, Cnt = 10;

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            while (isAlive)
            {
                // Dim traces constantly (if needed)
                if (removeTraces > 0 && cnt % 3 == 0)
                {
                    br.Color = Color.FromArgb(5, 0, 0, 0);
                    g.FillRectangle(br, 0, 0, Width, Height);
                }

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

                foreach (myObj_011 s in list)
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
