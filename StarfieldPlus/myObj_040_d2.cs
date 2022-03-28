using System;
using System.Drawing;


namespace my
{
    public class myObj_004_d2 : myObject
    {
        private int dxi, dyi, A, oldX, oldY;
        private int R = -1, G = -1, B = -1;
        private float dxf = 0, dyf = 0, x = 0, y = 0, time, dt;
        private bool isActive = false;

        static int x0 = 0, y0 = 0, moveMode = -1, drawMode = -1, speedMode = -1, maxA = 255, t = -1;
        static bool generationAllowed = false;

        static float time_static = 0;

        // -------------------------------------------------------------------------

        public myObj_004_d2()
        {
            if (colorPicker == null)
            {
                p = new Pen(Color.White);
                br = new SolidBrush(Color.White);
                colorPicker = new myColorPicker(Width, Height);

                drawMode = rand.Next(6);
                moveMode = rand.Next(9);
                speedMode = rand.Next(2);
                maxA = rand.Next(100) + 100;
                t = rand.Next(15) + 10;

                x0 = Width / 2;
                y0 = Height / 2;

                generationAllowed = true;

                Log($"myObj_004_d2");
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            if (generationAllowed)
            {
                int speed = (speedMode == 0) ? 5 : 3 + rand.Next(5);

                dxi = 0;
                dyi = 0;
                dxf = 0;
                dyf = 0;

                A = rand.Next(maxA) + 1;

                X = rand.Next(Width);
                Y = rand.Next(Height);

                double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));

                dxi = (int)((X - x0) * speed / dist);
                dyi = (int)((Y - y0) * speed / dist);

                getDxDy(speed, ref dxf, ref dyf);

                Size = rand.Next(6) + 1;

                X = x0;
                Y = y0;
                oldX = x0;
                oldY = y0;
                x = x0;
                y = y0;

                isActive = true;
                time = 0;
                dt = 0;
            }

            return;
        }

        // -------------------------------------------------------------------------

        void getDxDy(int speed, ref float dxf, ref float dyf)
        {
            int x0 = Width  / 2;
            int y0 = Height / 2;

            int x = rand.Next(Width);
            int y = rand.Next(Height);

            double dist = Math.Sqrt((x - x0) * (x - x0) + (y - y0) * (y - y0));

            dxf = (float)((x - x0) * speed / dist);
            dyf = (float)((y - y0) * speed / dist);

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            oldX = X;
            oldY = Y;

            int const1 = 1;
            float a = 0.25f;
            float b = 1.55f;
            float c = 0.10f;
            float sinx;
            float cosy;

#if true
            moveMode = 99;
            drawMode = 2;
            t = 1;
#endif

            switch (moveMode)
            {
                case 99:
                    const1 = 1;
                    dt = 0.25f;
                    time += dt;

                    a = 0.25f;
                    b = 1.55f;
                    c = 0.10f;

                    x += dxf * c;
                    y += dyf * c;

                    sinx = (float)(Math.Sin(a * time + b * dxf));
                    cosy = (float)(Math.Cos(a * time + b * dxf));

                    x += sinx * const1 * time_static;
                    y += cosy * const1 * time_static;
                    break;

                case 10006:
                    const1 = 1;
                    dt = 0.25f;
                    time += dt;

                    a = 0.25f;
                    b = 1.55f;
                    c = 0.10f;

                    x += dxf * c;
                    y += dyf * c;

                    sinx = (float)(Math.Sin(a * time + b * dxf));
                    cosy = (float)(Math.Cos(a * time + b * dxf));

                    x += sinx * const1 * time_static;
                    y += cosy * const1 * time_static;
                    break;

                case 10005:
                    const1 = 12;
                    dt = 0.05f;
                    time += dt;

                    x += (float)(Math.Sin(time + dxf) * const1 * time) * time;
                    y += (float)(Math.Cos(time + dxf) * const1 * time) * time;
                    break;

                case 10004:
                    const1 = 12;

                    dt += (dt == 0) ? 0.25f : 0.05f;
                    time += dt;   // try various const1 and dt

                    time += (float)(Math.Sin(time)) / 100;

                    x += (float)(Math.Sin(time + dxf) * dyf) * time;
                    y += (float)(Math.Cos(time + dxf) * dyf) * time;
                    break;

                case 10003:
                    const1 = 12;

                    dt += (dt == 0) ? 0.25f : 0.05f;
                    time += dt;   // try various const1 and dt

                    //time += (float)(Math.Sin(time)) / 100;

                    x += (float)(Math.Sin(time + dxf) * dyf) * time;
                    y += (float)(Math.Cos(time + dxf) * dyf) * time;
                    break;

                case 10002:
                    const1 = 12;
                    dt = 0.25f;
                    time += dt;   // try various const1 and dt

                    time += (float)(Math.Tan(time)) / const1;

                    x += (float)(Math.Sin(time + dxf) * dyf) * time;
                    y += (float)(Math.Cos(time + dxf) * dyf) * time;
                    break;

                case 10001:
                    const1 = 1;

                    time += 0.0001f;
                    time += (float)(Math.Sin(time) * 10);

                    x += (float)(Math.Sin(time + dxf) * dyf) * time;
                    y += (float)(Math.Cos(time + dxf) * dyf) * time;
                    break;

                case 10000:
                    const1 = 1;

                    time += 0.25f;   // try 0.2, 0.3, 0.5

                    x += (float)(Math.Sin(time + dxf) * dyf) * time;
                    y += (float)(Math.Cos(time + dxf) * dyf) * time;
                    break;
            }

            if (isActive)
            {
                X = (int)x;
                Y = (int)y;
            }

            if (A != 0)
                A--;

            if (X < 0 || X > Width || Y < 0 || Y > Height || A == 0)
            {
                isActive = false;
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

                case 3:
                    p.Color = Color.FromArgb(A, br.Color.R, br.Color.G, br.Color.B);
                    g.DrawRectangle(p, X, Y, 3, 3);
                    break;

                case 4:
                    p.Color = Color.FromArgb(A, br.Color.R, br.Color.G, br.Color.B);
                    g.DrawRectangle(p, X, Y, Size, Size);
                    break;

                case 5:
                    p.Color = Color.FromArgb(A, br.Color.R, br.Color.G, br.Color.B);
                    g.DrawLine(p, oldX, oldY, X, Y);
                    g.DrawRectangle(p, X - 1, Y - 1, 3, 3);
                    break;
            }
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int cnt = 0, threshold = 200;
            var list = new System.Collections.Generic.List<myObj_004_d2>();

            Count = 50;

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            while (isAlive)
            {
                int cntActive = 0;

                if (list.Count < Count)
                {
                    list.Add(new myObj_004_d2());
                }

                foreach (var obj in list)
                {
                    obj.Show();
                    obj.Move();

                    cntActive += obj.isActive ? 1 : 0;
                }

                time_static += 0.15f;

                form.Invalidate();
                System.Threading.Thread.Sleep(t);

                // Wait untill every object finishes, then start from new point
                if (++cnt > threshold)
                {
                    generationAllowed = false;

                    threshold = rand.Next(100) + 50;

                    if (cntActive == 0)
                    {
                        x0 = rand.Next(Width);
                        y0 = rand.Next(Height);

                        getNewBrush(br);

                        time_static = 0.0f;

                        generationAllowed = true;
                        cnt = 0;
                        System.Threading.Thread.Sleep(333);
                    }
                }
            }

            return;
        }

        // -------------------------------------------------------------------------

        private void getNewBrush(SolidBrush br)
        {
            int alpha = 0, R = 0, G = 0, B = 0, max = 256;

            while (alpha + R + G + B < 100)
            {
                alpha = rand.Next(max - 75) + 75;
                R = rand.Next(max);
                G = rand.Next(max);
                B = rand.Next(max);
            }

            br.Color = Color.FromArgb(alpha, R, G, B);
        }

        // -------------------------------------------------------------------------

        private bool getNewBrush(SolidBrush br, bool doGenerate)
        {
            if (doGenerate)
            {
                while (R + G + B < 100)
                {
                    R = rand.Next(256);
                    G = rand.Next(256);
                    B = rand.Next(256);
                }
            }

            int r = br.Color.R;
            int g = br.Color.G;
            int b = br.Color.B;

            r += r == R ? 0 : r > R ? -1 : 1;
            g += g == G ? 0 : g > G ? -1 : 1;
            b += b == B ? 0 : b > B ? -1 : 1;

            br.Color = Color.FromArgb(255, r, g, b);

            return r == R && g == G && b == B;
        }

        // -------------------------------------------------------------------------
    };
};
