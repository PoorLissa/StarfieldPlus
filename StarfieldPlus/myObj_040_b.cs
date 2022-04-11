using System;
using System.Drawing;


namespace my
{
    public class myObj_004_b : myObject
    {
        private int dx, dy, A, oldX, oldY;
        private float dx2 = 0, dy2 = 0, x = 0, y = 0, time = 0;

        static int x0 = 0, y0 = 0, moveMode = -1, drawMode = -1, speedMode = -1, maxA = 255, t = -1;
        static string strInfo = "";

        // -------------------------------------------------------------------------

        public myObj_004_b()
        {
            if (colorPicker == null)
            {
                p = new Pen(Color.White);
                br = new SolidBrush(Color.White);
                colorPicker = new myColorPicker(Width, Height);
                f = new Font("Segoe UI", 9, FontStyle.Regular, GraphicsUnit.Point);

                drawMode = rand.Next(3);
                moveMode = rand.Next(12);
                speedMode = rand.Next(2);
                maxA = rand.Next(maxA-11) + 11;
                t = rand.Next(15) + 1;

                x0 = Width  / 2;
                y0 = Height / 2;

                x0 += rand.Next(Width) - x0;

                Log($"myObj_004_b");
            }

            generateNew();

            time = 0;
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            int speed = 5;

            if (speedMode == 0)
            {
                speed = 3 + rand.Next(5);
            }

            dx = 0;
            dy = 0;

            dx2 = 0;
            dy2 = 0;

            do
            {
                A = 0;

                X = rand.Next(Width);
                Y = rand.Next(Height);

                oldX = X;
                oldY = Y;

                double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));

                dx = (int)((X - x0) * speed / dist);
                dy = (int)((Y - y0) * speed / dist);

                // floats
                {
                    x = X;
                    y = Y;

                    dx2 = (float)((X - x0) * speed / dist);
                    dy2 = (float)((Y - y0) * speed / dist);
                }

                Size = rand.Next(6) + 1;
            }
            while (dx == 0 && dy == 0);

            time = 0.001f * rand.Next(1111);

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            oldX = X;
            oldY = Y;

            switch (moveMode)
            {
                case 0:
                    X += dx * 2;
                    Y += dy * 2;

                    X += (int)(Math.Sin(Y) * 5);
                    Y += (int)(Math.Sin(X) * 5);
                    break;

                case 1:
                    X += dx;
                    Y += dy;
                    break;

                case 2:
                    x += dx2;
                    y += dy2;
                    X = (int)x;
                    Y = (int)y;
                    break;

                case 3:
                    time += (float)(rand.Next(999) / 1000.0f);

                    x += dx2 + (float)(Math.Sin(time) * 1);
                    y += dy2 + (float)(Math.Cos(time) * 1);

                    X = (int)x;
                    Y = (int)y;
                    break;

                case 4:
                    time += 0.1f;

                    x += dx2 + (float)(Math.Sin(time) * 1);
                    y += dy2 + (float)(Math.Cos(time) * 1);

                    X = (int)x;
                    Y = (int)y;
                    break;

                case 5:
                    time += 0.01f;

                    x += dx2 + (float)(Math.Sin(time) * 1);
                    y += dy2 + (float)(Math.Cos(time) * 1);

                    X = (int)x;
                    Y = (int)y;
                    break;

                case 6:
                    time += 0.1f;   // change this

                    x += dx2 + (float)(Math.Sin(time * dx2) * 2);   // change const
                    y += dy2 + (float)(Math.Sin(time * dy2) * 2);

                    X = (int)x;
                    Y = (int)y;
                    break;

                case 7:
                    time += 0.01f;

                    x += dx2 + (float)(Math.Sin(time * dx2) * 2);
                    y += dy2 + (float)(Math.Sin(time * dy2) * 2);

                    X = (int)x;
                    Y = (int)y;
                    break;

                case 8:
                    time += 0.01f;

                    x += dx2 + (float)(Math.Sin(time * dx2) * 3);
                    y += dy2 + (float)(Math.Sin(time * dy2) * 3);

                    X = (int)x;
                    Y = (int)y;
                    break;

                case 9:
                    time += 0.01f;

                    x += dx2 + (float)(Math.Sin(time * dy2) * 2);
                    y += dy2 + (float)(Math.Sin(time * dx2) * 2);

                    X = (int)x;
                    Y = (int)y;
                    break;

                case 10:
                    // need low alpha
                    time += 0.01f;

                    x -= dx2 + (float)(Math.Sin(time * 1) * 1);
                    y -= dy2 + (float)(Math.Sin(time * 2) * 1);

                    X = (int)x;
                    Y = (int)y;
                    break;

                case 11:
                    // need low alpha
                    time += 0.1f;

                    x -= dx2 + (float)(Math.Sin(time * 1) * 1);
                    y -= dy2 + (float)(Math.Sin(time * 2) * 1);

                    X = (int)x;
                    Y = (int)y;
                    break;

                // Research mode, don't use
                case 99:
                    time += 0.5f;

                    x += dx2 + (float)(Math.Sin(1/time) * 1);
                    y += dy2 + (float)(Math.Sin(1/time) * 1);

                    X = (int)x;
                    Y = (int)y;
                    break;
            }

            if (A != maxA)
                A++;

            if (X < 0 || X > Width || Y < 0 || Y > Height)
            {
                generateNew();
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            switch (drawMode)
            {
                case 0:
                    br.Color = Color.FromArgb(A, br.Color.R, br.Color.G, br.Color.B);
                    g.FillRectangle(br, X, Y, Size, Size);
                    break;

                case 1:
                    br.Color = Color.FromArgb(A, br.Color.R, br.Color.G, br.Color.B);
                    g.FillRectangle(br, X, Y, 2, 2);
                    break;

                case 2:
                    p.Color = Color.FromArgb(A, br.Color.R, br.Color.G, br.Color.B);
                    g.DrawLine(p, oldX, oldY, X, Y);
                    break;
            }
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int cnt = 0;
            var list = new System.Collections.Generic.List<myObj_004_b>();
            var brDim = new SolidBrush(Color.FromArgb(3, 0, 0, 0));

            for (int i = 0; i < Count; i++)
            {
                list.Add(new myObj_004_b());
            }

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            while (isAlive)
            {
                foreach (var obj in list)
                {
                    obj.Show();
                    obj.Move();
                }

                // Display some info
                if (my.myObject.ShowInfo)
                {
                    if (cnt % 100 == 0 || strInfo.Length == 0)
                    {
                        strInfo = $" obj = myObj_140_b\n drawMode = {drawMode}\n moveMode = {moveMode}\n speedMode = {speedMode}";
                        g.FillRectangle(Brushes.Black, 30, 33, 155, 200);
                        g.DrawString(strInfo, f, Brushes.Red, 35, 33);
                    }
                }
                else
                {
                    if (strInfo.Length > 0)
                    {
                        g.FillRectangle(Brushes.Black, 30, 33, 155, 200);
                        strInfo = string.Empty;
                    }
                }

                // Dim the screen
                if (cnt % 5 == 0)
                {
                    g.FillRectangle(brDim, 0, 0, Width, Height);
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);

                if (++cnt > 1000)
                {
                    bool gotNewBrush = colorPicker.getNewBrush(br, cnt == 1001);

                    if (gotNewBrush)
                    {
                        cnt = 0;
                    }

                }
            }

            return;
        }

        // -------------------------------------------------------------------------
    };
};
