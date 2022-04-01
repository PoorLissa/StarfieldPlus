using System;
using System.Drawing;

/*
    - Patchwork / Micro Schematics
*/

namespace my
{
    public class myObj_004_c : myObject
    {
        private int dx, dy, a, oldx, oldy, iterCounter;
        private bool isStatic = false;

        private static int moveMode = 0, maxA = 33, maxSize = 0, spd = 0, divider = 0, t = 0;
        private static float moveConst = 0.0f, time = 0.0f;
        private static bool showStatics = false;

        public myObj_004_c()
        {
            if (colorPicker == null)
            {
                p = new Pen(Color.White);
                br = new SolidBrush(Color.White);
                f = new Font("Segoe UI", 8, FontStyle.Regular, GraphicsUnit.Point);
                colorPicker = new myColorPicker(Width, Height);

                spd = (rand.Next(2) == 0) ? -1 : rand.Next(20) + 1;
                maxSize = rand.Next(4) + 1;
                moveMode = rand.Next(18);

                // Get moveConst as a Gaussian distribution [1 .. 10] skewed to the left
                {
                    int n = 3, moveConst_i = 0;

                    for (int i = 0; i < n; i++)
                    {
                        // Get symmetrical distribution...
                        moveConst_i += rand.Next(999 / n);

                        if (rand.Next(2) == 0)
                        {
                            // ... and skew it to the left
                            moveConst_i -= 2 * rand.Next(moveConst_i) / 3;
                        }
                    }

                    moveConst = 1.0f + moveConst_i * 0.01f;
                }

                divider = rand.Next(10) + 1;

                // More often this divider will be 1, but sometimes it can be [1..4]
                divider = divider > 4 ? 1 : divider;

                showStatics = rand.Next(3) == 0;
                isStatic = false;
                iterCounter = 0;

                t = rand.Next(4) - 1;

                // Override defaults
#if false
                spd = 3;
                maxSize = 3;
                moveMode = 0;
                moveConst = 1.23f;
                divider = 1;
                showStatics = true;
                t = 0;
#endif

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

                int x0 = Width / 2;
                int y0 = Height / 2;

                int speed = (spd > 0) ? spd : rand.Next(20) + 1;

                int dist = (int)Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));

                dx = (X - x0) * speed / dist;
                dy = (Y - y0) * speed / dist;

                oldx = X;
                oldy = Y;

            }
            //while (dx == 0 && dy == 0);
            //while (dx == 0 || dy == 0);
            while (false);
        }

        // -------------------------------------------------------------------------

        private void getNew()
        {
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

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            switch (moveMode)
            {
                case 0:
                    X += (int)(Math.Sin(Y) * moveConst) / divider;
                    Y += (int)(Math.Sin(X) * moveConst) / divider;
                    break;

                case 1:
                    X += (int)(Math.Sin(Y) * moveConst) / divider;
                    Y += (int)(Math.Cos(X) * moveConst) / divider;
                    break;

                case 2:
                    X += (int)(Math.Sin(Y + dy) * moveConst) / divider;
                    Y += (int)(Math.Sin(X + dx) * moveConst) / divider;
                    break;

                case 3:
                    X += (int)(Math.Sin(Y + dy) * moveConst) / divider;
                    Y += (int)(Math.Cos(X + dx) * moveConst) / divider;
                    break;

                case 4:
                    X += (int)(Math.Sin(Y + dx) * moveConst) / divider;
                    Y += (int)(Math.Sin(X + dy) * moveConst) / divider;
                    break;

                case 5:
                    X += (int)(Math.Sin(Y + dx) * moveConst) / divider;
                    Y += (int)(Math.Cos(X + dy) * moveConst) / divider;
                    break;

                case 6:
                    X += (int)(Math.Sin(Y + dx) * moveConst) / divider;
                    Y += (int)(Math.Sin(X + dx) * moveConst) / divider;
                    break;

                case 7:
                    X += (int)(Math.Sin(Y + dx) * moveConst) / divider;
                    Y += (int)(Math.Cos(X + dx) * moveConst) / divider;
                    break;

                case 8:
                    X += (int)(Math.Sin(Y + dy) * moveConst) / divider;
                    Y += (int)(Math.Sin(X + dy) * moveConst) / divider;
                    break;

                case 9:
                    X += (int)(Math.Sin(Y + dy) * moveConst) / divider;
                    Y += (int)(Math.Cos(X + dy) * moveConst) / divider;
                    break;

                case 10:
                    X += (int)(Math.Sin(Y * dy) * moveConst) / divider;
                    Y += (int)(Math.Sin(X * dx) * moveConst) / divider;
                    break;

                case 11:
                    X += (int)(Math.Sin(Y * dy) * moveConst) / divider;
                    Y += (int)(Math.Cos(X * dx) * moveConst) / divider;
                    break;

                case 12:
                    X += (int)(Math.Sin(Y * dx) * moveConst) / divider;
                    Y += (int)(Math.Sin(X * dy) * moveConst) / divider;
                    break;

                case 13:
                    X += (int)(Math.Sin(Y * dx) * moveConst) / divider;
                    Y += (int)(Math.Cos(X * dy) * moveConst) / divider;
                    break;

                case 14:
                    X += (int)(Math.Sin(Y * dx) * moveConst) / divider;
                    Y += (int)(Math.Sin(X * dx) * moveConst) / divider;
                    break;

                case 15:
                    X += (int)(Math.Sin(Y * dx) * moveConst) / divider;
                    Y += (int)(Math.Cos(X * dx) * moveConst) / divider;
                    break;

                case 16:
                    X += (int)(Math.Sin(Y * dy) * moveConst) / divider;
                    Y += (int)(Math.Sin(X * dy) * moveConst) / divider;
                    break;

                case 17:
                    X += (int)(Math.Sin(Y * dy) * moveConst) / divider;
                    Y += (int)(Math.Cos(X * dy) * moveConst) / divider;
                    break;
            }

            if (!isStatic)
            {
                // Find the shapes that are relatively small and static
                // Set their opacity to random low values
                if (X == oldx && Y == oldy && iterCounter < 1000)
                {
                    isStatic = true;
                    a = rand.Next(10) + 1;
                    oldx = oldy = -12345;
                }

                iterCounter++;
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            if (showStatics)
            {
                br.Color = Color.FromArgb(a, br.Color.R, br.Color.G, br.Color.B);
                g.FillRectangle(br, X, Y, Size, Size);
            }
            else
            {
                if (!isStatic)
                {
                    br.Color = Color.FromArgb(a, br.Color.R, br.Color.G, br.Color.B);
                    g.FillRectangle(br, X, Y, Size, Size);
                }
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            string strInfo = "";

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            System.Threading.Thread.Sleep(500);

            int cnt = 0, maxIter = 500 + rand.Next(1500);

            var dimBrush = new SolidBrush(Color.FromArgb(7, 0, 0, 0));
            var list = new System.Collections.Generic.List<myObj_004_c>();

            for (int i = 0; i < Count; i++)
            {
                list.Add(new myObj_004_c());
            }

            while (isAlive)
            {
                int staticsCnt = 0;

                foreach (var s in list)
                {
                    s.Show();
                    s.Move();

                    if (s.isStatic)
                        staticsCnt++;
                }

                // View some info (need to press Tab)
                if (my.myObject.ShowInfo)
                {
                    if (strInfo.Length == 0)
                    {
                        strInfo = $" obj = myObj_004_c\n moveMode = {moveMode}\n spd = {spd}\n maxSize = {maxSize}\n moveConst = {moveConst}\n divider = {divider}\n showStatics = {showStatics}\n t = {t}\n staticsCnt = {staticsCnt}";
                    }

                    if (cnt % 100 == 0)
                    {
                        g.FillRectangle(Brushes.Black, 30, 33, 155, 200);
                        g.DrawString(strInfo, f, Brushes.Red, 35, 33);
                    }
                }
                else
                {
                    if (strInfo.Length > 0)
                    {
                        strInfo = "";
                        g.FillRectangle(Brushes.Black, 30, 33, 155, 200);
                    }
                }

                form.Invalidate();

                if (t >= 0)
                {
                    System.Threading.Thread.Sleep(t);
                }

                if (++cnt > maxIter)
                {
                    bool gotNewBrush = colorPicker.getNewBrush(br, cnt == (maxIter + 1));

                    if (gotNewBrush)
                    {
                        g.FillRectangle(dimBrush, 0, 0, Width, Height);
                        cnt = 0;
                    }
                }
            }

            return;
        }
    };
};

