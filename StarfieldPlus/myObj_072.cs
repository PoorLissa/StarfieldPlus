using System;
using System.Drawing;

/*
    - Pieces drop off the desktop and fall down -- even grid-like distribution
*/

namespace my
{
    public class myObj_072 : myObject
    {
        protected float x, y, dx, dy;
        protected int A = 0, R = 0, G = 0, B = 0, oldX = -1, oldY = -1, x0 = 0, y0 = 0;
        protected bool alive = false;
        protected float time = 0;

        protected static Pen p = null;
        protected static SolidBrush br = null;
        protected static Graphics g_orig = null;
        protected static int maxSize = 33;
        protected static int moveMode = 0;
        protected static int dir = 0;
        protected static Rectangle destRect;
        protected static Rectangle srcRect;

        protected Bitmap bmp1 = null;
        protected Bitmap bmp2 = null;
        protected Graphics g2 = null;

        // -------------------------------------------------------------------------

        public myObj_072()
        {
            if (colorPicker == null)
            {
                A = (rand.Next(5) + 1) * 50;

                p = new Pen(Color.White);
                br = new SolidBrush(Color.FromArgb(A, 0, 0, 0));
                colorPicker = new myColorPicker(Width, Height, rand.Next(2));
                f = new Font("Segoe UI", 11, FontStyle.Regular, GraphicsUnit.Point);
                g_orig = Graphics.FromImage(colorPicker.getImg());                      // Graphics to draw on the original image
                maxSize = rand.Next(50) + 5;
                moveMode = rand.Next(3);

//moveMode = 2;

                dir = rand.Next(2);
                dir = dir == 0 ? -1 : 1;

                destRect.X = 0;
                destRect.Y = 0;
                destRect.Width  = maxSize;
                destRect.Height = maxSize;
                 srcRect.Width  = maxSize;
                 srcRect.Height = maxSize;

                Log($"myObj_072: colorPicker({colorPicker.getMode()})");
            }
            else
            {
                bmp1 = new Bitmap(maxSize, maxSize);
                bmp2 = new Bitmap(maxSize, maxSize);
                g2 = Graphics.FromImage(bmp2);

                alive = true;
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            Size = maxSize;
            dx = rand.Next(3) - 1;
            dy = 0;
            R = 0;
            time = 0;
            oldX = -1;
            oldY = -1;

            int failCnt = 0;

            do
            {
                X = rand.Next(Width);
                Y = rand.Next(Height);

                X -= X % Size;
                Y -= Y % Size;

                colorPicker.getColor(X, Y, ref R, ref G, ref B);

                if (++failCnt > 50)
                {
                    alive = false;
                    return;
                }
            }
            while (R == 0 && G == 0 && B == 0);

            x = X;
            y = Y;

            x0 = X;
            y0 = Y;

            // Store the original main piece
            using (Graphics gg = Graphics.FromImage(bmp1))
            {
                srcRect.X = X;
                srcRect.Y = Y;
                gg.DrawImage(colorPicker.getImg(), destRect, srcRect, GraphicsUnit.Pixel);
            }

            // Darken the source
            g_orig.FillRectangle(br, X, Y, Size, Size);

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            if (oldX != -1)
            {
                g.DrawImage(bmp2, oldX, oldY);
            }

            // Store tmp bmp
            srcRect.X = X;
            srcRect.Y = Y;
            g2.DrawImage(colorPicker.getImg(), destRect, srcRect, GraphicsUnit.Pixel);

            // Draw our main bmp piece
            g.DrawImage(bmp1, X, Y);
            g.DrawRectangle(Pens.Black, X, Y, Size-1, Size-1);
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            oldX = X;
            oldY = Y;

            switch (moveMode)
            {
                case 0:
                    move_0();       // Vertically down
                    break;

                case 1:
                    move_1();       // Blown by the wind
                    break;

                case 2:
                    move_2();       // Test
                    break;
            }

            if (Y > Height + Size)
            {
                Show();
                generateNew();
            }

            return;
        }

        // -------------------------------------------------------------------------

        private void move_0()
        {
            x += dx;
            y += dy;

            X = (int)x;
            Y = (int)y;

            dy += (0.01f + Size / 20.0f);
        }

        // -------------------------------------------------------------------------

        private void move_1()
        {
            x += dir * rand.Next(99);
            y += dy;

            X = (int)x;
            Y = (int)y;

            dy += (0.01f + Size / 200.0f);
        }

        // -------------------------------------------------------------------------

        private void move_2()
        {
            x = (float)(Math.Sin(time) * R/8);
            x += dx;
            y += dy;

            X = x0 + (int)x;
            Y = (int)y;

            R++;
            time += 0.25f;

            dy += (0.01f + Size / 10.0f);
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            g.DrawImage(colorPicker.getImg(), 0, 0, form.Bounds, GraphicsUnit.Pixel);
            form.Invalidate();
            System.Threading.Thread.Sleep(33);

            int t = 11, Cnt = 100, totalCnt = 0;

            var list = new System.Collections.Generic.List<myObj_072>();

            while (isAlive)
            {
#if false
                g.FillRectangle(br, form.Bounds);
                form.Invalidate();
                System.Threading.Thread.Sleep(1000);
                continue;
#endif
                int found = 0;

                foreach (var obj in list)
                {
                    if (obj.alive)
                    {
                        found++;
                        obj.Show();
                        obj.Move();
                    }
                }
#if DEBUG
                string str = $"total = {totalCnt++}; Count = {list.Count}; alive = {found}";
                g.FillRectangle(Brushes.Black, 50, 50, 400, 33);
                g.DrawString(str, f, Brushes.Red, 50, 50);
#endif
                if (found == 0 && list.Count > 0)
                {
                    g.FillRectangle(Brushes.Gray, form.Bounds);
                    t = 100;
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);

                if (list.Count < Cnt)
                {
                    list.Add(new myObj_072());
                }
            }

            g_orig.Dispose();
            br.Dispose();

            return;
        }

        // -------------------------------------------------------------------------
    };
};
