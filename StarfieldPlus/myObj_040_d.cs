using System;
using System.Drawing;

/*
    - Various shapes growing out from a single starting point
    - Based initially on the starfield class -- where all the stars are generated at a center point
*/

namespace my
{
    public class myObj_004_d : myObject
    {
        private const int N = 31;       // Number of cases in Move() function -- update as needed

        private int dxi, dyi, A, oldX, oldY;
        private float dxf = 0, dyf = 0, x = 0, y = 0, time, dt;
        private bool isActive = false;

        static int x0 = 0, y0 = 0, moveMode = -1, drawMode = -1, speedMode = -1, colorMode = -1, maxA = 255, t = -1;
        static bool generationAllowed = false;
        static bool isRandomMove = false;
        static bool doUpdateConstants = true;
        static float time_static = 0, dtStatic = 0;

        static int   const1 = 0;
        static float const2 = 0;
        static float a = 0.25f, b = 1.55f, c = 0.10f;
        static float sinx, cosy;

        // -------------------------------------------------------------------------

        public myObj_004_d()
        {
            if (colorPicker == null)
            {
                p = new Pen(Color.White);
                br = new SolidBrush(Color.White);
                colorPicker = new myColorPicker(Width, Height);
                f = new Font("Segoe UI", 7, FontStyle.Regular, GraphicsUnit.Point);

                drawMode = rand.Next(6);
                moveMode = rand.Next(N);
                speedMode = rand.Next(2);
                colorMode = rand.Next(2);
                maxA = rand.Next(100) + 100;
                t = rand.Next(15) + 10;
                dtStatic = 0.15f;

                x0 = Width  / 2;
                y0 = Height / 2;

                getNewBrush(br);
                updateConstants();

                generationAllowed = true;
                isRandomMove = rand.Next(3) == 0;

#if true
                // Override Move()
                moveMode = 99;
                moveMode = 0;
                drawMode = 2;
                t = 1;
                isRandomMove = false;
                updateConstants();
#endif

                Log($"myObj_004_d");
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            if (generationAllowed)
            {
                int speed = (speedMode == 0) ? 5 : 3 + rand.Next(5);

                A    = rand.Next(maxA) + 1;
                X    = rand.Next(Width);
                Y    = rand.Next(Height);
                Size = rand.Next(6) + 1;

                //getDxDy(speed, ref dxi, ref dyi);
                getDxDy(speed, ref dxf, ref dyf);

                x = X = oldX = x0;
                y = Y = oldY = y0;
                time = dt = 0;

                isActive = true;
            }

            return;
        }

        // -------------------------------------------------------------------------

        void getDxDy(int speed, ref int dxi, ref int dyi)
        {
            int x0 = Width  / 2;
            int y0 = Height / 2;

            int x = rand.Next(Width);
            int y = rand.Next(Height);

            double dist = Math.Sqrt((x - x0) * (x - x0) + (y - y0) * (y - y0));

            dxi = (int)((x - x0) * speed / dist);
            dyi = (int)((y - y0) * speed / dist);

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

        float zz = 0.01f;

        protected override void Move()
        {
            oldX = X;
            oldY = Y;

            switch (moveMode)
            {
                case 0:

/*
                    const1 = 0;                 // old 5
                    const2 = 0.03f;              // old 2

                    const2 += zz;
                    //zz += 0.001f;
*/
                    x += dxf * const2;
                    y += dyf * const2;

                    x += (int)(Math.Sin(Y) * const1);
                    y += (int)(Math.Sin(X) * const1);
                    break;

                case 1:
                    x += dxf;
                    y += dyf;
                    break;

                case 2:
                    time += (float)(rand.Next(999) / 1000.0f);

                    x += dxf + (float)(Math.Sin(time) * 1);
                    y += dyf + (float)(Math.Cos(time) * 1);
                    break;

                case 3:
                    time += 0.1f;

                    x += dxf + (float)(Math.Sin(time) * 1);
                    y += dyf + (float)(Math.Cos(time) * 1);
                    break;

                case 4:
                    time += 0.01f;

                    x += dxf + (float)(Math.Sin(time) * 1);
                    y += dyf + (float)(Math.Cos(time) * 1);
                    break;

                case 5:
                    time += 0.1f;   // change this

                    x += dxf + (float)(Math.Sin(time * dxf) * 2);   // change const
                    y += dyf + (float)(Math.Sin(time * dyf) * 2);
                    break;

                case 6:
                    time += 0.01f;

                    x += dxf + (float)(Math.Sin(time * dxf) * 2);
                    y += dyf + (float)(Math.Sin(time * dyf) * 2);
                    break;

                case 7:
                    time += 0.1f;

                    x += (float)(Math.Sin(time + dyf) * 2) * time;
                    y += (float)(Math.Cos(time + dyf) * 2) * time;
                    break;

                case 8:
                    time += 0.1f;

                    x += (float)(Math.Sin(time * dyf) * 2) * time;
                    y += (float)(Math.Cos(time * dyf) * 2) * time;
                    break;

                case 9:
                    time += 0.25f;   // try 0.2, 0.3, 0.5

                    x += (float)(Math.Sin(time + dxf) * dyf) * time;
                    y += (float)(Math.Cos(time + dxf) * dyf) * time;
                    break;

                case 10:
                    time += 0.0001f;
                    time += (float)(Math.Sin(time) * 10);

                    x += (float)(Math.Sin(time + dxf) * dyf) * time;
                    y += (float)(Math.Cos(time + dxf) * dyf) * time;
                    break;

                case 11:
                    const1 = 12;
                    dt = 0.25f;
                    time += dt;   // try various const1 and dt

                    time += (float)(Math.Tan(time)) / const1;

                    x += (float)(Math.Sin(time + dxf) * dyf) * time;
                    y += (float)(Math.Cos(time + dxf) * dyf) * time;
                    break;

                case 12:
                    dt += (dt == 0) ? 0.25f : 0.05f;
                    time += dt;   // try various const1 and dt

                    //time += (float)(Math.Sin(time)) / 100;

                    x += (float)(Math.Sin(time + dxf) * dyf) * time;
                    y += (float)(Math.Cos(time + dxf) * dyf) * time;
                    break;

                case 13:
                    dt += (dt == 0) ? 0.25f : 0.05f;
                    time += dt;   // try various const1 and dt

                    time += (float)(Math.Sin(time)) / 100;

                    x += (float)(Math.Sin(time + dxf) * dyf) * time;
                    y += (float)(Math.Cos(time + dxf) * dyf) * time;
                    break;

                // ?..
                case 14:
                    const1 = 12;
                    dt = 0.05f;
                    time += dt;

                    x += (float)(Math.Sin(time + dxf) * const1 * time) * time;
                    y += (float)(Math.Cos(time + dxf) * const1 * time) * time;
                    break;

                // Spiral
                case 15:
                    const2 = 0.33f;     // General size of the shape

                    dt = 0.08f;         // more straight <--> more curving towards the circle
                    dt = 0.25f;         // todo: see if we already have this circlic pattern and this more straight one, and add it we don't
                    time += dt;

                    a = 0.25f;          // a's sign affects the direction of spiralling. Value +/- acts like b
                    b = 1.55f;          // b affects distribution of spirals around the central point
                    c = 0.10f;          // c here affects straight/curve ratio of the spiral

                    x += dxf * c;
                    y += dyf * c;

                    sinx = (float)(Math.Sin(a * time + b * dxf));
                    cosy = (float)(Math.Cos(a * time + b * dxf));

                    x += sinx * const2 * time_static;
                    y += cosy * const2 * time_static;
                    break;

                case 16:
                    time += 0.999f;

                    x += (float)(Math.Sin(time + dyf * 1) * 1) * time;
                    y += (float)(Math.Cos(time + dyf * 1) * 1) * time;
                    break;

                case 17:
                    time += 0.1f;   // try 0.2, 0.3, 0.5

                    x += (float)(Math.Sin(time) * dxf) * time;
                    y += (float)(Math.Cos(time) * dyf) * time;
                    break;

                case 18:
                    time += 0.05f;

                    x += dxf;
                    y += dyf * (float)(Math.Cos(time) * 2);
                    break;

                case 19:
                    time += 0.01f;

                    x += dxf * (float)(Math.Sin(time) * 3);
                    y += dyf;
                    break;

                case 20:
                    time += 0.1f;

                    x += dxf + (int)(Math.Sin(time) * 5);
                    y += dyf + (int)(Math.Cos(time) * 5);
                    break;

                case 21:
                    const1 = 1;
                    time += 0.01f;

                    x += (int)(Math.Sin(time * dyf) * 21 + time * const1);
                    y += (int)(Math.Cos(time * dxf) * 21 + time * const1);
                    break;

                case 22:
                    const1 = 1;
                    time += 0.01f;

                    x += (int)(Math.Sin(time + dyf) * 21 + time * const1);
                    y += (int)(Math.Cos(time + dxf) * 21 + time * const1);
                    break;

                case 23:
                    const1 = 1;
                    time += 0.01f;

                    x += (int)(Math.Sin(time + dyf) * 3 + time * const1);
                    y += (int)(Math.Cos(time + dxf) * 3 + time * const1);
                    break;

                case 24:
                    const1 = 1;
                    time += 0.1f;

                    x += (int)(Math.Sin(time + dyf) * 3 + time * const1);
                    y += (int)(Math.Cos(time + dxf) * 3 + time * const1);
                    break;

                case 25:
                    time += 0.01f;

                    x += (int)(Math.Sin(Y + time) * 30 * time);
                    y += (int)(Math.Cos(X + time) * 30 * time);
                    break;

                case 26:
                    time += 0.1f;

                    x += dxf * 3;
                    y += dyf * 3;

                    x += (int)(Math.Sin(time) * 21);    // change const
                    y += (int)(Math.Cos(time) * 21);
                    break;

                case 27:
                    time += 0.01f;

                    x += dxf + (float)(Math.Sin(time * dxf) * 3);
                    y += dyf + (float)(Math.Sin(time * dyf) * 3);
                    break;

                case 28:
                    time += 0.01f;

                    x += dxf + (float)(Math.Sin(time * dyf) * 2);
                    y += dyf + (float)(Math.Sin(time * dxf) * 2);
                    break;

                case 29:
                    // needs low alpha
                    time += 0.01f;

                    x -= dxf + (float)(Math.Sin(time * 1) * 1);
                    y -= dyf + (float)(Math.Sin(time * 2) * 1);
                    break;

                case 30:
                    // needs low alpha
                    time += 0.1f;

                    x -= dxf + (float)(Math.Sin(time * 1) * 1);
                    y -= dyf + (float)(Math.Sin(time * 2) * 1);
                    break;

                // Research mode, don't use
                case 99:
                    // case 15
                    const2 = 0.33f;     // General size of the shape
                    //dt = 0.25f;
                    dt = 0.08f;         // more straight <--> more curving towards the circle
                    time += dt;

                    a = 0.25f;          // a's sign affects the direction of spiralling. Value +/- acts like b
                    b = 1.55f;          // b affects distribution of spirals around the central point
                    c = 0.10f;          // c here affects straight/curve ratio of the spiral

                    x += dxf * c;
                    y += dyf * c;

                    sinx = (float)(Math.Sin(a * time + b * dxf));
                    cosy = (float)(Math.Cos(a * time + b * dxf));

                    x += sinx * const2 * time_static;
                    y += cosy * const2 * time_static;
                    break;

                case 991:
                    // case 15
                    const2 = 0.33f;     // General size of the shape
                    //dt = 0.25f;
                    dt = 0.08f;         // more straight <--> more curving towards the circle
                    time += dt;

                    a = 0.25f;
                    b = 1.55f;
                    c = (float)(Math.Sin(time_static));     // this turns spirals into random tentacles

                    x += dxf * c;
                    y += dyf * c;

                    sinx = (float)(Math.Sin(a * time + b * dxf));
                    cosy = (float)(Math.Cos(a * time + b * dxf));

                    x += sinx * const2 * time_static;
                    y += cosy * const2 * time_static;
                    break;

                case 992:
                    // case 15
                    const2 = 1.33f;     // General size of the shape
                    //dt = 0.25f;
                    dt = 0.08f;         // more straight <--> more curving towards the circle
                    time += dt;

                    a = 0.25f;          // a
                    b = 1.55f;          // b affects distribution of spirals around the central point

                    b = time_static * time; // this makes it fractal-like

                    c = 0.10f;          // c here affects straight/curve ratio of the spiral

                    x += dxf * c;
                    y += dyf * c;

                    sinx = (float)(Math.Sin(a * time + b * dxf));
                    cosy = (float)(Math.Cos(a * time + b * dxf));

                    x += sinx * const2 * time_static;
                    y += cosy * const2 * time_static;
                    break;

                case 993:
                    // case 15
                    const2 = 0.33f;     // General size of the shape
                    dt = 0.08f;         // more straight <--> more curving towards the circle
                    time += dt;

                    a = 0.25f;          // a
                    b = 1.55f;          // b affects distribution of spirals around the central point

                    b = (float)(Math.Sin(time_static)); // <------ + play with dt
                    //b = (float)(Math.Sin(time_static * dyf * dxf));   // <-- gives alien tail shapes

                    c = 0.10f;          // c here affects straight/curve ratio of the spiral

                    x += dxf * c;
                    y += dyf * c;

                    sinx = (float)(Math.Sin(a * time + b * dxf));
                    cosy = (float)(Math.Cos(a * time + b * dxf));

                    x += sinx * const2 * time_static;
                    y += cosy * const2 * time_static;
                    break;

                case 994:
                    // case 15
                    const2 = 0.33f;     // General size of the shape
                    //dt = 0.25f;
                    dt = 0.08f;         // more straight <--> more curving towards the circle
                    time += dt;

                    //a = 0.25f;          // a's sign affects the direction of spiralling
                    a = 0.25f * (rand.Next(3) - 1);
                    b = 1.55f;          // b affects distribution of spirals around the central point
                    c = 0.10f;          // c here affects straight/curve ratio of the spiral

                    x += dxf * c;
                    y += dyf * c;

                    sinx = (float)(Math.Sin(a * time + b * dxf));
                    cosy = (float)(Math.Cos(a * time + b * dxf));

                    x += sinx * const2 * time_static;
                    y += cosy * const2 * time_static;
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

        // Update global constants
        private void updateConstants()
        {
            switch (moveMode)
            {
                case 0:
                    const1 = rand.Next(10)+1;                   // old 5
                    const2 = (rand.Next(50) + 1) / 10.0f;       // old 2
                    break;
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
                    g.DrawRectangle(p, X-1, Y-1, 3, 3);
                    break;
            }
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int cnt = 0, threshold = 200;
            var list = new System.Collections.Generic.List<myObj_004_d>();

            Count = 50;

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            while (isAlive)
            {
                int cntActive = 0;

                if (list.Count < Count)
                {
                    list.Add(new myObj_004_d());
                }

                foreach (var obj in list)
                {
                    obj.Show();
                    obj.Move();

                    cntActive += obj.isActive ? 1 : 0;
                }

#if DEBUG
                string str = $"moveMode = {moveMode};";
                g.FillRectangle(Brushes.Black, 33, 33, 120, 21);
                g.DrawString(str, f, Brushes.Red, 35, 33);
#endif

                form.Invalidate();
                System.Threading.Thread.Sleep(t);

                time_static += dtStatic;

                // Wait untill every object finishes, then start from new point
                if (++cnt > threshold)
                {
                    generationAllowed = false;

                    threshold = rand.Next(100) + 50;

                    if (cntActive == 0)
                    {
                        x0 = rand.Next(Width);
                        y0 = rand.Next(Height);

                        moveMode = isRandomMove ? rand.Next(N) : moveMode;

                        if (doUpdateConstants)
                        {
                            updateConstants();
                        }
                        
                        getNewBrush(br);

                        time_static = 0.0f;

                        cnt = 0;
                        generationAllowed = true;
                        System.Threading.Thread.Sleep(333);
                    }
                }
            }

            return;
        }

        // -------------------------------------------------------------------------

        private void getNewBrush(SolidBrush br)
        {
            if (colorMode == 0)
            {
                colorPicker.getNewBrush(br);
            }
            else
            {
                int r = -1, g = -1, b = -1, alpha = rand.Next(256 - 75) + 75;

                colorPicker.getColor(x0, y0, ref r, ref g, ref b);

                br.Color = Color.FromArgb(alpha, r, g, b);
            }

            return;
        }

        // -------------------------------------------------------------------------
    };
};
