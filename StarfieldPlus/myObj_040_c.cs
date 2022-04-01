using System;
using System.Drawing;

/*
    - Patchwork
*/

namespace my
{
    public class myObj_004_c : myObject
    {
        private int dx, dy, a;

        private static int moveMode = 0, maxA = 33, maxSize = 0, spd = 0;
        private static float moveConst = 0;

        public myObj_004_c()
        {
            if (colorPicker == null)
            {
                p = new Pen(Color.White);
                br = new SolidBrush(Color.White);
                colorPicker = new myColorPicker(Width, Height);

                spd = (rand.Next(2) == 0) ? -1 : rand.Next(20) + 1;
                maxSize = rand.Next(4) + 1;
                moveMode = rand.Next(18);
                moveConst = 1.0f + rand.Next(999) * 0.01f;

                Log($"myObj_004_c");
            }

            dx = 0;
            dy = 0;
            a = maxA;
            Size = maxSize;

            do
            {

                X = rand.Next(Width);
                Y = rand.Next(Height);

                int x0 = Width  / 2;
                int y0 = Height / 2;

                int speed = (spd > 0) ? spd : rand.Next(20) + 1;

                int dist = (int)Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));
                //double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));

                dx = (X - x0) * speed / dist;
                dy = (Y - y0) * speed / dist;

            }
            while (dx == 0 && dy == 0);
        }

        // -------------------------------------------------------------------------

        private void getNew()
        {
            return;

            dx = 0;
            dy = 0;
            a = maxA;
            Size = maxSize;

            do
            {

                X = rand.Next(Width);
                Y = rand.Next(Height);

                int speed = 5;

                int x0 = Width / 2;
                int y0 = Height / 2;

                double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));

                dx = (int)((X - x0) * speed / dist);
                dy = (int)((Y - y0) * speed / dist);

            }
            while (dx == 0 && dy == 0);
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
#if false
            moveMode = 0;
            moveConst = 5.0f;
#endif
            switch (moveMode)
            {
                case 0:
                    X += (int)(Math.Sin(Y) * moveConst);
                    Y += (int)(Math.Sin(X) * moveConst);
                    break;

                case 1:
                    X += (int)(Math.Sin(Y) * moveConst);
                    Y += (int)(Math.Cos(X) * moveConst);
                    break;

                case 2:
                    X += (int)(Math.Sin(Y + dy) * moveConst);
                    Y += (int)(Math.Sin(X + dx) * moveConst);
                    break;

                case 3:
                    X += (int)(Math.Sin(Y + dy) * moveConst);
                    Y += (int)(Math.Cos(X + dx) * moveConst);
                    break;

                case 4:
                    X += (int)(Math.Sin(Y + dx) * moveConst);
                    Y += (int)(Math.Sin(X + dy) * moveConst);
                    break;

                case 5:
                    X += (int)(Math.Sin(Y + dx) * moveConst);
                    Y += (int)(Math.Cos(X + dy) * moveConst);
                    break;

                case 6:
                    X += (int)(Math.Sin(Y + dx) * moveConst);
                    Y += (int)(Math.Sin(X + dx) * moveConst);
                    break;

                case 7:
                    X += (int)(Math.Sin(Y + dx) * moveConst);
                    Y += (int)(Math.Cos(X + dx) * moveConst);
                    break;

                case 8:
                    X += (int)(Math.Sin(Y + dy) * moveConst);
                    Y += (int)(Math.Sin(X + dy) * moveConst);
                    break;

                case 9:
                    X += (int)(Math.Sin(Y + dy) * moveConst);
                    Y += (int)(Math.Cos(X + dy) * moveConst);
                    break;

                case 10:
                    X += (int)(Math.Sin(Y * dy) * moveConst);
                    Y += (int)(Math.Sin(X * dx) * moveConst);
                    break;

                case 11:
                    X += (int)(Math.Sin(Y * dy) * moveConst);
                    Y += (int)(Math.Cos(X * dx) * moveConst);
                    break;

                case 12:
                    X += (int)(Math.Sin(Y * dx) * moveConst);
                    Y += (int)(Math.Sin(X * dy) * moveConst);
                    break;

                case 13:
                    X += (int)(Math.Sin(Y * dx) * moveConst);
                    Y += (int)(Math.Cos(X * dy) * moveConst);
                    break;

                case 14:
                    X += (int)(Math.Sin(Y * dx) * moveConst);
                    Y += (int)(Math.Sin(X * dx) * moveConst);
                    break;

                case 15:
                    X += (int)(Math.Sin(Y * dx) * moveConst);
                    Y += (int)(Math.Cos(X * dx) * moveConst);
                    break;

                case 16:
                    X += (int)(Math.Sin(Y * dy) * moveConst);
                    Y += (int)(Math.Sin(X * dy) * moveConst);
                    break;

                case 17:
                    X += (int)(Math.Sin(Y * dy) * moveConst);
                    Y += (int)(Math.Cos(X * dy) * moveConst);
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int cnt = 0, t = 0;

            var dimBrush = new SolidBrush(Color.FromArgb(11, 0, 0, 0));
            var list = new System.Collections.Generic.List<myObj_004_c>();

            for (int i = 0; i < Count; i++)
            {
                list.Add(new myObj_004_c());
            }

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            while (isAlive)
            {
                if (cnt % 1000 == 0)
                {
                    
                }

                foreach (var s in list)
                {
                    br.Color = Color.FromArgb(s.a, br.Color.R, br.Color.G, br.Color.B);

                    g.FillRectangle(br, s.X, s.Y, s.Size, s.Size);

                    //p.Color = Color.FromArgb(s.a + 25, br.Color.R, br.Color.G, br.Color.B);
                    //g.DrawRectangle(p, s.X, s.Y, s.Size, s.Size);
                    //g.DrawRectangle(p, s.X-1, s.Y-1, s.Size+1, s.Size+1);

                    s.Move();
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);

                if (++cnt > 1000)
                {
                   bool gotNewBrush = colorPicker.getNewBrush(br, cnt == 1001);

                    if (gotNewBrush)
                    {
                        g.FillRectangle(dimBrush, 0, 0, Width, Height);
                        cnt = 0;
                    }
                }
            }

            return;
        }

        // -------------------------------------------------------------------------

        private void getNewBrush(SolidBrush br)
        {
            int alpha = 0, R = 0, G = 0, B = 0, max = 100;

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

