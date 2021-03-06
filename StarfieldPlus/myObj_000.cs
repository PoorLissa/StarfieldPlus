using System;
using System.Drawing;
using System.Collections.Generic;

/*
    - Star Field

    todo:
        see what's with the flickering in [drawMode = 1] when static stars have background light
*/

namespace my
{
    public class myObj_000 : myObject
    {
        protected static List<myObject> list = null;
        protected static SolidBrush dimBrush = null;
        protected float x, y, dx, dy, acceleration = 1.0f;
        protected int cnt = 0, max = 0, color = 0;

        protected static int x0 = 0, y0 = 0, drawMode = 0;

        // -------------------------------------------------------------------------

        public myObj_000()
        {
            if (br == null)
            {
                p = new Pen(Color.White);
                br = new SolidBrush(Color.Black);
                dimBrush = new SolidBrush(Color.Green);
                colorPicker = new myColorPicker(Width, Height);
                list = new List<myObject>();

                drawMode = rand.Next(2);

                x0 = Width  / 2;
                y0 = Height / 2;

                Log($"myObj_000");
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            switch (color)
            {
                case 0: br.Color = Color.Red;    break;
                case 1: br.Color = Color.Yellow; break;
                case 2: br.Color = Color.Blue;   break;
                case 3: br.Color = Color.Orange; break;
                case 4: br.Color = Color.Aqua;   break;
                case 5: br.Color = Color.Violet; break;

                default:
                    br.Color = Color.White;
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            form.Invalidate();

            // Build and gradually display the background Galaxy
            var background = buildGalaxy();
            br.Color = Color.FromArgb(255, 0, 0, 0);

            while (isAlive && br.Color.A > 0)
            {
                g.DrawImage(background, form.Bounds, form.Bounds, GraphicsUnit.Pixel);
                g.FillRectangle(br, 0, 0, Width, Height);

                int opacity = br.Color.A - 7;
                br.Color = Color.FromArgb(opacity > 0 ? opacity : 0, 0, 0, 0);

                form.Invalidate();
                System.Threading.Thread.Sleep(100);
            }

            if (isAlive)
            {
                // Add static stars and comets
                {
                    int staticStarsCnt = rand.Next(333) + 333;
                    int cometsCnt = 3;

                    // Update main counter
                    Count += staticStarsCnt + cometsCnt;

                    for (int i = 0; i < staticStarsCnt; i++)
                        list.Add(new myObj_000_b());

                    for (int i = 0; i < cometsCnt; i++)
                        list.Add(new myObj_000_c());
                }

                while (isAlive)
                {
                    g.DrawImage(background, form.Bounds, form.Bounds, GraphicsUnit.Pixel);

                    foreach (myObj_000 obj in list)
                    {
                        obj.Show();
                        obj.Move();
                    }

                    form.Invalidate();
                    System.Threading.Thread.Sleep(33);

                    // Gradually increase number of moving stars, until the limit is reached
                    if (list.Count < Count)
                    {
                        list.Add(new myObj_000_a());
                    }
                }
            }

            return;
        }

        // -------------------------------------------------------------------------

        // Build random Galaxy background
        private Bitmap buildGalaxy()
        {
            Bitmap bmp = new Bitmap(Width, Height);
            int x1 = 0, y1 = 0, x2 = 0, y2 = 0;

            using (var gr = Graphics.FromImage(bmp))
            {
                // Black background
                gr.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                // Add low opacity colored spots
                for (int i = 0; i < 10; i++)
                {
                    int opacity = rand.Next(7) + 1;

                    x1 = rand.Next(Width);
                    y1 = rand.Next(Height);

                    x2 = rand.Next(333) + 100 * opacity;
                    y2 = rand.Next(333) + 100 * opacity;

                    br.Color = Color.FromArgb(1, rand.Next(256), rand.Next(256), rand.Next(256));

                    while (opacity != 0)
                    {
                        gr.FillRectangle(br, x1, y1, x2, y2);

                        x1 += rand.Next(25) + 25;
                        y1 += rand.Next(25) + 25;
                        x2 -= rand.Next(50) + 50;
                        y2 -= rand.Next(50) + 50;
                        opacity--;
                    }
                }

                //return bmp;

                // Add nebulae
                for (int k = 0; k < 111; k++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        x1 = rand.Next(Width);
                        y1 = rand.Next(Height);

                        int masterOpacity = rand.Next(100) + 100;
                        int radius1 = rand.Next(250) + 100;
                        int radius2 = rand.Next(250) + 100;
                        colorPicker.getColor(br, x1, y1, masterOpacity);

                        gr.FillRectangle(br, x1, y1, 1, 1);

                        for (int j = 0; j < 100; j++)
                        {
                            x2 = x1 + rand.Next(radius1) - radius1 / 2;
                            y2 = y1 + rand.Next(radius2) - radius2 / 2;

                            int maxSlaveOpacity = 50 + rand.Next(50);
                            int slaveOpacity = rand.Next(maxSlaveOpacity);

                            br.Color = Color.FromArgb(slaveOpacity, br.Color.R, br.Color.G, br.Color.B);
                            gr.FillRectangle(br, x2, y2, 1, 1);

                            int r = rand.Next(2) + 3;

                            br.Color = Color.FromArgb(3, br.Color.R, br.Color.G, br.Color.B);
                            gr.FillRectangle(br, x2 - r, y2 - r, 2 * r, 2 * r);
                        }
                    }
                }
            }

            return bmp;
        }

        // -------------------------------------------------------------------------
    }


    // ===========================================================================================================
    // ===========================================================================================================


    // Moving stars
    class myObj_000_a : myObj_000
    {
		protected override void generateNew()
        {
            int speed = rand.Next(10) + 1;

            max = rand.Next(75) + 20;
            cnt = 0;
            color = rand.Next(50);
            acceleration = 1.005f + (rand.Next(100) * 0.0005f);

            x = X = rand.Next(Width);

#if true
            y = Y = rand.Next(Width);

            double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - x0) * (Y - x0));
            double sp_dist = speed / dist;

            dx = (float)((X - x0) * sp_dist);
            dy = (float)((Y - x0) * sp_dist);

            y = Y = (Y - (Width - Height)/2);
#else
            y = Y = rand.Next(Height);

            double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));
            double sp_dist = speed / dist;

            dx = (float)((X - x0) * sp_dist);
            dy = (float)((Y - y0) * sp_dist);
#endif

            Size = 0;
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            base.Show();

            switch (drawMode)
            {
                case 0:
                    g.FillRectangle(br, X, Y, Size, Size);
                    break;

                case 1:
                    if (Size == 1)
                    {
                        g.FillRectangle(br, X, Y, 1, 1);
                    }
                    else
                    {
                        p.Color = br.Color;
                        g.DrawRectangle(p, X, Y, Size - 1, Size - 1);
                    }
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            x += dx;
            y += dy;

            X = (int)x;
            Y = (int)y;

            if (cnt++ > max)
            {
                cnt = 0;
                Size++;

                // Accelerate acceleration rate
                acceleration *= (1.0f + (Size * 0.001f));
            }

            // Accelerate our moving stars
            dx *= acceleration;
            dy *= acceleration;

            if (X < 0 || X > Width || Y < 0 || Y > Height)
            {
                generateNew();
            }

            return;
        }
    };


    // ===========================================================================================================
    // ===========================================================================================================


    // Static stars
    public class myObj_000_b : myObj_000
    {
        private int lifeCounter = 0;
        protected int alpha = 0, bgrAlpha = 0;
        private static int factor = 1;
        private static bool doMove = true;

        protected override void generateNew()
        {
            lifeCounter = (rand.Next(500) + 500) * factor;

            X = rand.Next(Width);
            Y = rand.Next(Height);
            color = rand.Next(50);
            alpha = rand.Next(50) + 175;

            bgrAlpha = 1;

            if (rand.Next(11) == 0)
                bgrAlpha = 2;

            max = (rand.Next(200) + 100) * factor;
            cnt = 0;
            Size = 0;

            // Make our static stars not so static
            if (doMove)
            {
                // linear speed outwards:
                double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));
                double sp_dist = 0.1f / dist;

                dx = (float)((X - x0) * sp_dist);
                dy = (float)((Y - y0) * sp_dist);

                x = X;
                y = Y;
            }
        }

        protected override void Move()
        {
            if (doMove)
            {
                x += dx;
                y += dy;

                X = (int)x;
                Y = (int)y;
            }

            if (lifeCounter-- == 0)
            {
                factor = 3;
                generateNew();
            }
            else
            {
                if (cnt++ > max)
                {
                    cnt = 0;
                    Size = rand.Next(5) + 1;
                }
            }

            return;
        }

        protected override void Show()
        {
            base.Show();

            dimBrush.Color = Color.FromArgb(bgrAlpha, br.Color.R, br.Color.G, br.Color.B);

            int b = 3 * Size;
            g.FillRectangle(dimBrush, X - Size, Y - Size, b, b);

            int a = 5 * Size;
            b = 11 * Size;
            g.FillEllipse(dimBrush, X - a, Y - a, b, b);

            a = rand.Next(5) + 3;
            b = (2 * a + 1) * Size;
            a = a * Size;
            g.FillEllipse(dimBrush, X - a, Y - a, b, b);

            switch (drawMode)
            {
                case 0:
                    g.FillRectangle(br, X, Y, Size, Size);
                    break;

                case 1:
                    if (Size == 1)
                    {
                        g.FillRectangle(br, X, Y, 1, 1);
                    }
                    else
                    {
                        p.Color = Color.FromArgb(255, br.Color.R, br.Color.G, br.Color.B);
                        g.DrawRectangle(p, X, Y, Size - 1, Size - 1);
                    }
                    break;
            }

            if (cnt % 100 == 0)
            {
                // ... and make them semitransparent
                alpha = rand.Next(50) + 175;

                if (rand.Next(11) == 0)
                {
                    alpha -= rand.Next(alpha);
                }
            }

            dimBrush.Color = Color.FromArgb(alpha, 0, 0, 0);
            g.FillRectangle(dimBrush, X, Y, Size, Size);
        }
    };


    // ===========================================================================================================
    // ===========================================================================================================


    // Comets
    public class myObj_000_c : myObj_000
    {
        private int lifeCounter = 0, xOld = 0, yOld = 0;

        protected override void generateNew()
        {
            lifeCounter = rand.Next(1000) + 666;
            //lifeCounter = rand.Next(100) + 66;

            int x0 = rand.Next(Width);
            int y0 = rand.Next(Height);
            int x1 = rand.Next(Width);
            int y1 = rand.Next(Height);

            float a = (float)(y1 - y0) / (float)(x1 - x0);
            float b = y1 - a * x1;

            int speed = rand.Next(200) + 200;

            double dist = Math.Sqrt((x1 - x0) * (x1 - x0) + (y1 - y0) * (y1 - y0));
            double sp_dist = speed / dist;

            dx = (float)((x1 - x0) * sp_dist);
            dy = (float)((y1 - y0) * sp_dist);

            if (dx > 0)
            {
                x = X = xOld = 0;
                y = Y = yOld = (int)b;
            }
            else
            {
                x = xOld = X = Width;
                y = a * x + b;
                Y = yOld = (int)y;
            }

            Size = rand.Next(5) + 1;
        }

        protected override void Move()
        {
            // Wait for the counter to reach zero. Then start moving the comet
            if (lifeCounter-- < 0)
            {
                xOld = X;
                yOld = Y;

                x += dx;
                y += dy;

                X = (int)x;
                Y = (int)y;

                if ((dx > 0 && X > Width) || (dx < 0 && X < 0))
                {
                    generateNew();
                }
            }

            return;
        }

        protected override void Show()
        {
            if (lifeCounter < 0)
            {
                // Spot of light
                br.Color = Color.FromArgb(5, 245, 195, 60);
                g.FillEllipse(br, X - 100, Y - 100, 200, 200);
                g.FillEllipse(br, X - 200, Y - 200, 400, 400);

                br.Color = Color.FromArgb(rand.Next(66) + 11, 245, 195, 60);
                g.FillRectangle(br, X - 3*Size, Y - 3*Size, 6*Size, 6*Size);

                // Tail
                p.Color = Color.FromArgb(rand.Next(66) + 33, 225 + rand.Next(25), rand.Next(50), rand.Next(50));
                g.DrawLine(p, X-2*Size, Y, xOld, yOld);
                g.DrawLine(p, X+0, Y, xOld, yOld);
                g.DrawLine(p, X+2*Size, Y, xOld, yOld);

                p.Color = Color.FromArgb(rand.Next(50) + 100, Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B);
                g.DrawEllipse(p, X - 2*Size, Y - 2*Size, 4*Size, 4*Size);

                g.FillRectangle(Brushes.OrangeRed, X - Size, Y - Size, 2*Size, 2*Size);
            }
        }

    };

};
