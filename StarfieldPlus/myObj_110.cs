using System;
using System.Drawing;

/*
    - puts random colored rectangles all over the screen
*/

namespace my
{
    public class myObj_110 : myObject
    {
        private enum Shape { Square, Rectangle, Circle };

        int w, h, A, R, G, B, isBorder, maxSize;
        bool isRandomSize = false;
        int shape = 0;

        SolidBrush br = null;
        Pen p = null;

        // -------------------------------------------------------------------------

        public myObj_110()
        {
            br = new SolidBrush(Color.Red);
            p = new Pen(Color.Red);

            isRandomSize = (rand.Next(2) == 0);
            shape = rand.Next(3);

            if (isRandomSize)
            {
                getSize(ref maxSize);
            }
            else
            {
                getSize(ref maxSize);

                Size = maxSize;

                if (shape == (int)Shape.Rectangle)
                {
                    // Rectangle/ellipse with static dimensions
                    if (rand.Next(2) == 0)
                    {
                        w = 2 * Size;
                        h = w / (rand.Next(5) + 1);
                    }
                    else
                    {
                        h = 2 * Size;
                        w = h / (rand.Next(5) + 1);
                    }
                }
                else
                {
                    // Square/circle with static dimensions
                    w = 2 * Size;
                    h = 2 * Size;
                }
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            if (isRandomSize)
            {
                Size = rand.Next(maxSize) + 3;

                if (shape == (int)Shape.Rectangle)
                {
                    // Rectangle/ellipse with static dimensions
                    if (rand.Next(2) == 0)
                    {
                        w = 2 * Size;
                        h = w / (rand.Next(5) + 1);
                    }
                    else
                    {
                        h = 2 * Size;
                        w = h / (rand.Next(5) + 1);
                    }
                }
                else
                {
                    // Square/circle with static dimensions
                    w = 2 * Size;
                    h = 2 * Size;
                }
            }

            X = rand.Next(Width)  - Size;
            Y = rand.Next(Height) - Size;

            A = rand.Next(256);
            R = rand.Next(256);
            G = rand.Next(256);
            B = rand.Next(256);
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            br.Color = Color.FromArgb(A, R, G, B);

            switch ((Shape)(shape))
            {
                case Shape.Square:
                case Shape.Rectangle:
                    drawRectangle(g, br, p, X, Y, w, h, isBorder: isBorder);
                    break;

                case Shape.Circle:
                    drawCircle(g, br, p, X, Y, w, h, isBorder: isBorder);
                    break;
            }
            
        }

        // -------------------------------------------------------------------------

        private void getSize(ref int size)
        {
            switch (rand.Next(5))
            {
                case 0:
                    size = rand.Next(666) + 1;
                    break;

                case 1:
                    size = rand.Next(444) + 25;
                    break;

                case 2:
                    size = rand.Next(333) + 50;
                    break;

                case 3:
                    size = rand.Next(222) + 75;
                    break;

                case 4:
                    size = rand.Next(111) + 100;
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        private void drawRectangle(Graphics g, SolidBrush br, Pen p, int x, int y, int w, int h, int isBorder)
        {
            g.FillRectangle(br, x, y, w, h);

            switch (isBorder)
            {
                case 0:
                    // No Border
                    break;

                case 1:
                    g.DrawRectangle(Pens.Black, x, y, w, h);
                    break;

                case 2:
                    g.DrawRectangle(Pens.White, x, y, w, h);
                    break;

                case 3:
                    p.Color = Color.FromArgb(A, br.Color.R, br.Color.G, br.Color.B);
                    g.DrawRectangle(p, x, y, w, h);
                    break;

                case 4:
                    g.DrawRectangle(p, x, y, w, h);
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        private void drawCircle(Graphics g, SolidBrush br, Pen p, int x, int y, int w, int h, int isBorder)
        {
            g.FillEllipse(br, x, y, w, h);

            switch (isBorder)
            {
                case 0:
                    // No Border
                    break;

                case 1:
                    g.DrawEllipse(Pens.Black, x, y, w, h);
                    break;

                case 2:
                    g.DrawEllipse(Pens.White, x, y, w, h);
                    break;

                case 3:
                    p.Color = Color.FromArgb(A, br.Color.R, br.Color.G, br.Color.B);
                    g.DrawEllipse(p, x, y, w, h);
                    break;

                case 4:
                    g.DrawEllipse(p, x, y, w, h);
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int t = 1, cnt = 1000;

            isBorder = rand.Next(5);

            if (isBorder == 3 || isBorder == 4)
            {
                A = rand.Next( 56) + 200;
                R = rand.Next(256);
                G = rand.Next(256);
                B = rand.Next(256);

                p.Color = Color.FromArgb(A, R, G, B);
            }

            if (maxSize > 50)
            {
                t *= maxSize / 3;
            }

            while (isAlive)
            {
                generateNew();
                Show();

                form.Invalidate();
                System.Threading.Thread.Sleep(t);

                // Gradually slow the tempo
                if (--cnt == 0)
                {
                    t++;
                    cnt = 1000;
                }
            }

            return;
        }
    }
};
