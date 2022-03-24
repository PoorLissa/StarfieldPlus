using System;
using System.Drawing;

/*
    - Pieces drop off the desktop and fall down -- even grid-like distribution of the tiles

    todo:
        look for target color across all the cell, not only in a single pixel
*/

namespace my
{
    public class myObj_072 : myObject
    {
        protected float x, y, dx, dy;
        protected int A = 0, R = 0, G = 0, B = 0;
        protected bool alive = false;

        protected static Pen p = null;
        protected static SolidBrush br = null;
        protected static int maxSize = 25;

        protected static Rectangle destRect;
        protected static Rectangle srcRect;

        protected Bitmap bmp1 = null;
        protected Bitmap bmp2 = null;
        protected Graphics g2 = null;

        private int oldX = -1, oldY = -1;

        // -------------------------------------------------------------------------

        public myObj_072()
        {
            if (colorPicker == null)
            {
                p = new Pen(Color.White);
                br = new SolidBrush(Color.FromArgb(50, 0, 0, 0));
                //colorPicker = new myColorPicker(Width, Height, rand.Next(2));
                colorPicker = new myColorPicker(Width, Height, 0);
                f = new Font("Segoe UI", 11, FontStyle.Regular, GraphicsUnit.Point);
                maxSize = rand.Next(maxSize) + 5;

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
            dx = 0;
            dy = 0;

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

            // Store the original main piece
            using (Graphics gg = Graphics.FromImage(bmp1))
            {
                srcRect.X = X;
                srcRect.Y = Y;
                gg.DrawImage(colorPicker.getImg(), destRect, srcRect, GraphicsUnit.Pixel);
            }

            // Darken the source
            colorPicker.GetGraphics().FillRectangle(br, X, Y, Size, Size);

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
/*
            if (Y % 5 == 0)
                x += rand.Next(3) - 1;
*/

            oldX = X;
            oldY = Y;

            x += (float)(Math.Sin(Y) * 2) + rand.Next(13);
            y += dy;

/*
            var aaa = (int)System.DateTime.Now.Ticks;
            x = (float)(Math.Sin(aaa) * 33);
            y = (float)(Math.Cos(aaa) * 33);
*/
            X = (int)x;
            Y = (int)y;

            dy += (0.01f + Size / 2.0f);

            if (Y > Height + Size)
            {
                Show();
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
#if DEBUG
            var proc = System.Diagnostics.Process.GetCurrentProcess();
#endif
            g.DrawImage(colorPicker.getImg(), 0, 0, form.Bounds, GraphicsUnit.Pixel);
            form.Invalidate();
            System.Threading.Thread.Sleep(33);

            int t = 11, Cnt = 333, totalCnt = 0;

            var list = new System.Collections.Generic.List<myObj_072>();

            while (isAlive)
            {
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
                string str = $"total = {totalCnt++}; Count = {list.Count}; alive = {found}\nmemory = {proc.PrivateMemorySize64/1024/1024} Mb";
                g.FillRectangle(Brushes.Black, 50, 50, 400, 60);
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

            br.Dispose();

            return;
        }

        // -------------------------------------------------------------------------
    };
};
