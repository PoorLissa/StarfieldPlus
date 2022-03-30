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
        // Number of cases in Move() function -- update as needed
        private const int N = 44;

        private int dxi, dyi, A, oldX, oldY;
        private float dxf = 0, dyf = 0, x = 0, y = 0, time, dt;
        private bool isActive = false;

        static int x0 = 0, y0 = 0, moveMode = -1, drawMode = -1, speedMode = -1, colorMode = -1, maxA = 255, t = -1;
        static bool generationAllowed = false;
        static bool isRandomMove = false;
        static bool doUpdateConstants = true;
        static float time_global = 0, dtGlobal = 0, dtCommon = 0;

        static int removeTraces = 1;
        static int   si1 = 0;
        static float sf2 = 0;
        static float sf3 = 0;
        static float a = 0.25f, b = 1.55f, c = 0.10f;
        static float sinx, cosy;

        // -------------------------------------------------------------------------

        public myObj_004_d()
        {
            if (colorPicker == null)
            {
                p = new Pen(Color.White);
                br = new SolidBrush(Color.White);
                colorPicker = new myColorPicker(Width, Height, 3);
                f = new Font("Segoe UI", 7, FontStyle.Regular, GraphicsUnit.Point);

                drawMode = rand.Next(6);
                moveMode = rand.Next(N);
                speedMode = rand.Next(2);
                colorMode = rand.Next(2);
                maxA = rand.Next(100) + 100;
                t = rand.Next(15) + 10;
                dtGlobal = 0.15f;

                x0 = Width  / 2;
                y0 = Height / 2;

                getNewBrush(br);
                updateConstants();

                generationAllowed = true;
                isRandomMove = rand.Next(10) == 0;

#if false
                // Override Move()
                moveMode = 99;
                moveMode = 43;
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

                A = rand.Next(maxA) + 1;
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
            int x0 = Width / 2;
            int x = rand.Next(Width);
            int y = rand.Next(Width);

            double dist = Math.Sqrt((x - x0) * (x - x0) + (y - x0) * (y - x0));

            dxi = (int)((x - x0) * speed / dist);
            dyi = (int)((y - x0) * speed / dist);

            return;
        }

        // -------------------------------------------------------------------------

        void getDxDy(int speed, ref float dxf, ref float dyf)
        {
            int x0 = Width / 2;
            int x = rand.Next(Width);
            int y = rand.Next(Width);

            double dist = Math.Sqrt((x - x0) * (x - x0) + (y - x0) * (y - x0));

            dxf = (float)((x - x0) * speed / dist);
            dyf = (float)((y - x0) * speed / dist);

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            oldX = X;
            oldY = Y;

            // Every option that uses constants will have them changed in updateConstants()

            switch (moveMode)
            {
                // --- option 1 ---
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    // si1 : Lower values for rather straigt beams;
                    // Higher values make the beams lightning-like
                    // Hight values make the beams erratic

                    x += dxf * sf2;
                    y += dyf * sf2;

                    x += (int)(Math.Sin(Y) * si1);
                    y += (int)(Math.Sin(X) * si1);
                    break;

                // --- option 2 ---
                case 5:
                    x += dxf;
                    y += dyf;
                    break;

                // --- option 3 ---
                case 6:
                    time += (float)(rand.Next(999) / 1000.0f);

                    x += dxf + (float)(Math.Sin(time) * sf2);
                    y += dyf + (float)(Math.Cos(time) * sf2);
                    break;

                // --- option 4 ---
                case 7:
                    time += 0.1f;

                    x += dxf + (float)(Math.Sin(time) * sf2);
                    y += dyf + (float)(Math.Cos(time) * sf2);
                    break;

                // --- option 5 ---
                case 8:
                    time += 0.001f + (rand.Next(10)) * 0.005f;

                    x += dxf + (float)(Math.Sin(time) * sf2);
                    y += dyf + (float)(Math.Cos(time) * sf2);
                    break;

                // --- option 6 --- Hair like 
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                    time += dtCommon;

                    x += dxf + (float)(Math.Sin(time * dxf) * sf2);
                    y += dyf + (float)(Math.Sin(time * dyf) * sf2);
                    break;

                // --- option 7 --- Spiraling Wheels
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                    time += dtCommon;

                    x += (float)Math.Sin(time + dyf) * time * sf2;
                    y += (float)Math.Cos(time + dyf) * time * sf2;
                    break;

                // --- option 8 --- Stars with Spiraling Rays
                case 22:
                case 23:
                    time += dtCommon;

                    x += (float)(Math.Sin(time + dyf) * sf2) * time;
                    y += (float)(Math.Cos(time + dyf) * sf2) * time;
                    break;

                // --- option 9 --- Spiraling Squares
                case 24:
                    time += dtCommon;

                    x += (float)Math.Sin(time + dyf) * time * sf2;
                    y += (float)Math.Cos(time + dxf) * time * sf2;
                    break;

                // --- option 10 --- Spiraling Eights
                case 25:
                    time += dtCommon;

                    x += (float)Math.Sin(time * 1) * time * sf2;
                    y += (float)Math.Cos(time * 2) * time * sf2;
                    break;

                // --- option 11 --- Balls of Strings
                case 26:
                case 27:
                    time += dtCommon;

                    x += (float)Math.Sin(time * dyf) * time * sf2;
                    y += (float)Math.Cos(time * dyf) * time * sf2;
                    break;

                // --- option 12 --- Spiraling Wheels, ver2
                case 28:
                    time += dtCommon;

                    x += (float)Math.Sin(time + dxf) * time * dyf * sf2;
                    y += (float)Math.Cos(time + dxf) * time * dyf * sf2;
                    break;

                // --- option 13 --- Zigzagging Rays
                case 29:
                case 30:
                case 31:
                case 32:
                    time += dtCommon;
                    time += (float)(Math.Sin(time) * sf3);

                    x += (float)Math.Sin(time + dxf) * time * dyf * sf2;
                    y += (float)Math.Cos(time + dxf) * time * dyf * sf2;
                    break;

                // --- option 14 --- Straight rays from spiralling center
                case 33:
                    time += dtCommon;
                    time += (float)(Math.Sin(time) * sf3);

                    x += (float)Math.Sin(time + dxf) * time * dyf * sf2;
                    y += (float)Math.Cos(time + dxf) * time * dyf * sf2;
                    break;

                // --- option 15 --- Stars with straight rays
                case 34:
                case 35:
                case 36:
                    time += dtCommon;
                    time += (float)(Math.Sin(time) * sf3);

                    x += (float)Math.Sin(time + dxf) * time * dyf * sf2;
                    y += (float)Math.Cos(time + dxf) * time * dyf * sf2;
                    break;

                // --- option 16 --- Curled fishing line
                case 37:
                case 38:
                case 39:
                    time += dtCommon;
                    time += (float)Math.Tan(time) / sf3;

                    x += (float)Math.Sin(time + dxf) * dyf * time * sf2;
                    y += (float)Math.Cos(time + dxf) * dyf * time * sf2;
                    break;

                // --- option 17 --- Various shapes with ever increasing dtCommon
                case 40:
                case 41:
                    time += dtCommon;

                    x += (float)Math.Sin(time + dxf) * dyf * time * sf2;
                    y += (float)Math.Cos(time + dxf) * dyf * time * sf2;
                    break;

                // --- option 18 --- Various shapes with ever increasing dtCommon
                case 42:
                    time += dtCommon;
                    time += (float)(Math.Sin(time)) / sf3;

                    x += (float)Math.Sin(time + dxf) * dyf * time * sf2;
                    y += (float)Math.Cos(time + dxf) * dyf * time * sf2;
                    break;

                // --- option 19 --- Waves of Spirals
                case 43:
                    time += dtCommon;

                    x += (float)Math.Sin(time + dxf) * time * time * sf2;
                    y += (float)Math.Cos(time + dxf) * time * time * sf2;
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

        private void Move_Old()
        {
            oldX = X;
            oldY = Y;

            switch (moveMode)
            {
                // Spiral
                case 15:
                    sf2 = 0.33f;     // General size of the shape

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

                    x += sinx * sf2 * time_global;
                    y += cosy * sf2 * time_global;
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
                    si1 = 1;
                    time += 0.01f;

                    x += (int)(Math.Sin(time * dyf) * 21 + time * si1);
                    y += (int)(Math.Cos(time * dxf) * 21 + time * si1);
                    break;

                case 22:
                    si1 = 1;
                    time += 0.01f;

                    x += (int)(Math.Sin(time + dyf) * 21 + time * si1);
                    y += (int)(Math.Cos(time + dxf) * 21 + time * si1);
                    break;

                case 23:
                    si1 = 1;
                    time += 0.01f;

                    x += (int)(Math.Sin(time + dyf) * 3 + time * si1);
                    y += (int)(Math.Cos(time + dxf) * 3 + time * si1);
                    break;

                case 24:
                    si1 = 1;
                    time += 0.1f;

                    x += (int)(Math.Sin(time + dyf) * 3 + time * si1);
                    y += (int)(Math.Cos(time + dxf) * 3 + time * si1);
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
                    sf2 = 0.33f;     // General size of the shape
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

                    x += sinx * sf2 * time_global;
                    y += cosy * sf2 * time_global;
                    break;

                case 991:
                    // case 15
                    sf2 = 0.33f;     // General size of the shape
                    //dt = 0.25f;
                    dt = 0.08f;         // more straight <--> more curving towards the circle
                    time += dt;

                    a = 0.25f;
                    b = 1.55f;
                    c = (float)(Math.Sin(time_global));     // this turns spirals into random tentacles

                    x += dxf * c;
                    y += dyf * c;

                    sinx = (float)(Math.Sin(a * time + b * dxf));
                    cosy = (float)(Math.Cos(a * time + b * dxf));

                    x += sinx * sf2 * time_global;
                    y += cosy * sf2 * time_global;
                    break;

                case 992:
                    // case 15
                    sf2 = 1.33f;     // General size of the shape
                    //dt = 0.25f;
                    dt = 0.08f;         // more straight <--> more curving towards the circle
                    time += dt;

                    a = 0.25f;          // a
                    b = 1.55f;          // b affects distribution of spirals around the central point

                    b = time_global * time; // this makes it fractal-like

                    c = 0.10f;          // c here affects straight/curve ratio of the spiral

                    x += dxf * c;
                    y += dyf * c;

                    sinx = (float)(Math.Sin(a * time + b * dxf));
                    cosy = (float)(Math.Cos(a * time + b * dxf));

                    x += sinx * sf2 * time_global;
                    y += cosy * sf2 * time_global;
                    break;

                case 993:
                    // case 15
                    sf2 = 0.33f;     // General size of the shape
                    dt = 0.08f;         // more straight <--> more curving towards the circle
                    time += dt;

                    a = 0.25f;          // a
                    b = 1.55f;          // b affects distribution of spirals around the central point

                    b = (float)(Math.Sin(time_global)); // <------ + play with dt
                    //b = (float)(Math.Sin(time_static * dyf * dxf));   // <-- gives alien tail shapes

                    c = 0.10f;          // c here affects straight/curve ratio of the spiral

                    x += dxf * c;
                    y += dyf * c;

                    sinx = (float)(Math.Sin(a * time + b * dxf));
                    cosy = (float)(Math.Cos(a * time + b * dxf));

                    x += sinx * sf2 * time_global;
                    y += cosy * sf2 * time_global;
                    break;

                case 994:
                    // case 15
                    sf2 = 0.33f;     // General size of the shape
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

                    x += sinx * sf2 * time_global;
                    y += cosy * sf2 * time_global;
                    break;

            }
        }

        // -------------------------------------------------------------------------

        // Update global constants
        private void updateConstants()
        {
            if (!doUpdateConstants)
                return;

            switch (moveMode)
            {
                // --- option 1 ---
                case 0:
                case 1:
                    si1 = rand.Next(10)+1;
                    sf2 = (rand.Next(10) + 1) / 100.0f;
                    break;

                case 2:
                case 3:
                    si1 = rand.Next(20) + 1;
                    sf2 = (rand.Next(100) + 1) / 100.0f;
                    break;

                case 4:
                    si1 = rand.Next(30) + 1;
                    sf2 = (rand.Next(300) + 1) / 100.0f;
                    break;

                // --- option 2 ---
                case 5:
                    break;

                // --- option 3 ---
                case 6:
                    sf2 = rand.Next(7) + 2;          // 0.2 - 8

                    if (rand.Next(11) > 3)
                        sf2 *= 0.1f;
                    break;

                // --- option 4 ---
                case 7:
                    sf2 = rand.Next(19) + 2;         // 0.2 - 2
                    sf2 *= 0.1f;
                    break;

                // --- option 5 ---
                case 8:
                    sf2 = rand.Next(30) + 1;
                    sf2 *= 0.1f;
                    break;

                // --- option 6 ---
                case 9:
                    dtCommon = 0.025f + rand.Next(5) * 0.025f;
                    sf2 = 0.5f + rand.Next(11) * 0.1f;
                    break;

                case 10:
                    dtCommon = 0.5f + rand.Next(115) * 0.25f;
                    sf2 = 0.5f + rand.Next(11) * 0.1f;
                    break;

                case 11:
                    dtCommon = 0.025f + rand.Next(5) * 0.025f;
                    sf2 = 1.0f + rand.Next(21) * 0.1f;
                    break;

                case 12:
                    dtCommon = 0.5f + rand.Next(115) * 0.25f;
                    sf2 = 1.0f + rand.Next(21) * 0.1f;
                    break;

                case 13:
                    dtCommon = 0.025f + rand.Next(5) * 0.025f;
                    sf2 = 1.5f + rand.Next(123) * 0.1f;
                    break;

                case 14:
                    dtCommon = 0.5f + rand.Next(115) * 0.25f;
                    sf2 = 1.5f + rand.Next(123) * 0.1f;
                    break;

                case 15:
                    dtCommon = 0.005f + rand.Next(7) * 0.0025f;
                    sf2 = 1.0f + rand.Next(10) * 0.5f;
                    break;

                case 16:
                    dtCommon = 0.005f + rand.Next(7) * 0.0025f;
                    sf2 = 3.0f + rand.Next(33) * 0.5f;
                    break;

                // --- option 7 ---
                case 17:
                    dtCommon = (rand.Next(2) == 0) ? 0.1f : -0.1f;
                    sf2 = 0.5f;
                    sf2 += rand.Next(20) * 0.0125f;
                    sf2 *= 2.0f;
                    break;

                case 18:
                    dtCommon = (rand.Next(2) == 0) ? 0.1f : -0.1f;
                    sf2 = 0.5f;
                    sf2 += rand.Next(100) * 0.125f;
                    break;

                case 19:
                    dtCommon = (rand.Next(2) == 0) ? 0.1f : -0.1f;
                    dtCommon *= (rand.Next(101) * 0.025f + 1.0f);

                    sf2 = 0.5f;
                    sf2 += rand.Next(20) * 0.0125f;
                    sf2 *= (rand.Next(3) + 1);
                    break;

                case 20:
                    dtCommon += 0.05f;
                    sf2 = 1.33f;
                    break;

                case 21:
                    dtCommon += 0.001f * rand.Next(301);
                    sf2 = 1.33f;
                    break;

                // --- option 8 ---
                case 22:
                case 23:
                    dtCommon = (rand.Next(2) == 0) ? 0.01f : -0.01f;
                    dtCommon *= (rand.Next(333)+1) * 0.01f;

                    sf2 = rand.Next(201) + 50;
                    sf2 *= (moveMode == 22) ? 0.01f : 0.5f;
                    break;

                // --- option 9 ---
                case 24:
                    dtCommon += 0.05f;
                    sf2 = 1.33f;
                    break;

                // --- option 10 ---
                case 25:
                    dtCommon += 0.05f;
                    sf2 = 1.33f;
                    break;

                // --- option 11 ---
                case 26:
                    dtCommon = 0.01f;
                    dtCommon *= (rand.Next(30) + 5);

                    sf2 = rand.Next(201) + 100;
                    sf2 *= 0.01f;
                    break;

                case 27:
                    dtCommon = (rand.Next(2) == 0) ? 0.1f : -0.1f;
                    dtCommon *= (rand.Next(33) + 1);

                    sf2 = rand.Next(201) + 100;
                    sf2 *= 0.0033f;
                    break;

                // --- option 12 ---
                case 28:
                    dtCommon = (rand.Next(2) == 0) ? 0.05f : -0.05f;
                    dtCommon *= (rand.Next(9) + 2);

                    sf2 = rand.Next(201) + 100;
                    sf2 *= 0.0025f;
                    break;

                // --- option 13 ---
                case 29:
                    dtCommon = 0.0001f;
                    sf2 = (rand.Next(15) + 11) * 0.05f;
                    sf3 = 10.0f;
                    break;

                case 30:
                    dtCommon = 0.0001f;
                    sf2 = (rand.Next(15) + 11) * 0.05f;
                    sf3 = 4.0f;
                    break;

                case 31:
                    dtCommon = 0.001f;
                    dtCommon *= rand.Next(101);

                    sf2 = (rand.Next(15) + 11) * 0.05f;
                    sf3 = 4.0f;
                    break;

                case 32:
                    dtCommon = 0.001f;

                    sf3 = 2.1f;     // 2.1f - 3.9f
                    sf3 += rand.Next(200) * 0.01f;

                    sf2 = (rand.Next(25) + 11) * 0.05f;
                    break;

                // --- option 14 ---
                case 33:
                    dtCommon = (rand.Next(2) == 0) ? 0.001f : -0.001f;
                    dtCommon *= (rand.Next(50) + 1);

                    sf3 = 0.1f;
                    sf3 += rand.Next(200) * 0.01f;

                    sf2 = 2;
                    break;

                // --- option 15 ---
                case 34:
                    dtCommon = 0.001f;
                    sf3 = 0;
                    sf2 = (rand.Next(66) + 1) * 0.1f;
                    break;

                case 35:
                    dtCommon = 0.001f;
                    sf3 = 0;
                    sf2 = (rand.Next(366) + 1) * 0.1f;
                    break;

                case 36:
                    dtCommon = (rand.Next(2) == 0) ? 0.001f : -0.001f;
                    sf3 = 0.0001f;
                    sf3 *= rand.Next(333);
                    sf2 = (rand.Next(200) + 1) * 0.1f;
                    break;

                // --- option 16 --- 
                case 37:
                    dtCommon = (rand.Next(2) == 0) ? 1.0f : -1.0f;
                    dtCommon *= 0.25f;

                    sf3 = 12.0f;   // try various si1 and dt
                    sf2 = 0.25f + rand.Next(100) * 0.01f;
                    break;

                case 38:
                    // So many different options, I don't know how to choose yet...
                    // todo: find a way
/*
                    dtCommon *= 0.01f * rand.Next(1000);  // random
                    sf3 = 12.0f;   // try various si1 and dt
                    sf2 = 0.05f;
*/
                    // Taking turns with large and small scale
                    if (si1 == 0)
                    {
                        si1 = 1;
                        dtCommon = (rand.Next(2) == 0) ? 1.0f : -1.0f;
                        dtCommon *= 0.01f * rand.Next(1000);
                        sf3 = 0.01f * (rand.Next(3001)+1);
                        sf2 = 5.0f;
                    }
                    else
                    {
                        si1 = 0;
                        sf2 = 0.5f;
                    }

                    // some good ones. require that dtCommon == +-1.0
                    if (false) { dtCommon *= 123.321f; sf2 = 0.0075f;  }
                    if (false) { dtCommon *= 75.4328f; sf2 = 0.00075f; }
                    if (false) { dtCommon *= 1.2573f;  sf2 = 0.25f;    }
                    if (false) { dtCommon *= 12.2591f; sf2 = 0.025f;   }

                    //sf2 = 0.25f + rand.Next(100) * 0.01f;
                    break;

                case 39:
                    dtCommon = (rand.Next(2) == 0) ? 1.0f : -1.0f;
                    dtCommon *= 0.25f;

                    sf3 = 118.0f;   // todo: try various sf3
                    sf2 = sf2 > 1 ? 0.5f : 5.0f;
                    break;

                // --- option 17 ---
                case 40:
                    dtCommon += 0.05f;      // todo: there are some nice shapes. Remember the dt
                    sf2 = 1.0f;             // also, looks like the shapes with the same set of params can be different. Look into this.
                    break;

                case 41:
                    dtCommon = 21 * 0.05f;
                    sf2 = 1.0f;
                    break;

                // --- option 18 ---
                case 42:
                    dtCommon += (dtCommon == 0) ? 0.25f : 0.05f;
                    sf2 = 1.0f;
                    sf3 = 100.0f;           // todo: try changing this
                    break;

                // --- option 19 ---
                case 43:
                    dtCommon = 0.05f;
                    sf2 = 0.5f + 0.1f * rand.Next(11);
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

            var dimBrush = new SolidBrush(Color.FromArgb(10, 0, 0, 0));

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

                time_global += dtGlobal;

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

                        updateConstants();
                        getNewBrush(br);

                        time_global = 0.0f;

                        cnt = 0;
                        generationAllowed = true;
                        System.Threading.Thread.Sleep(333);

                        // Dim traces constantly (if needed)
                        //if (removeTraces == 1 && cnt % 3 == 0)
                        {
                            g.FillRectangle(dimBrush, 0, 0, Width, Height);
                        }
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
