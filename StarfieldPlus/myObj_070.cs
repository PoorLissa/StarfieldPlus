using System;
using System.Drawing;

/*
    - 'Defragmenting' the screen

    todo:
        checkered grid pattern (all drops from predefined grid places)
        look for target color across all the cell, not only in a single pixel
*/

namespace my
{
    public class myObj_070 : myObject
    {
        protected float x, y, dx, dy;
        protected int A = 0, R = 0, G = 0, B = 0;
        protected bool alive = false;

        protected static Pen p = null;
        protected static SolidBrush br = null;
        protected static int maxSize = 33;

        // -------------------------------------------------------------------------

        public myObj_070()
        {
            if (colorPicker == null)
            {
                p = new Pen(Color.White);
                br = new SolidBrush(Color.White);
                //colorPicker = new myColorPicker(Width, Height, rand.Next(2));
                colorPicker = new myColorPicker(Width, Height, 0);

                f = new Font("Segoe UI", 15, FontStyle.Regular, GraphicsUnit.Point);

                Log($"myObj_070: colorPicker({colorPicker.getMode()})");
            }
            else
            {
                alive = true;
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            Size = rand.Next(maxSize) + 1;
            dx = rand.Next(3) - 1;
            dy = 0;

            int failCnt = 0;

            do
            {
                A = rand.Next(56) + 200;
                X = rand.Next(Width);
                Y = rand.Next(Height);
                colorPicker.getColor(X, Y, ref R, ref G, ref B);

                if (++failCnt > 50)
                {
                    alive = false;
                    //maxSize -= (maxSize == 5) ? 0 : 1;
                    return;
                }
            }
            while (R == 0 && G == 0 && B == 0);

            x = X;
            y = Y;

            // this is lame, do rewrite
            for (int i = X - Size; i < X + Size; i++)
            {
                for (int j = Y - Size; j < Y + Size; j++)
                {
                    colorPicker.setPixel(i, j);
                }
            }
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            br.Color = Color.FromArgb(A, R, G, B);
            g.FillRectangle(br, X - Size, Y - Size, 2*Size, 2*Size);
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            if (Y % 5 == 0)
            {
                x += rand.Next(3) - 1;
            }

            y += dy;
            X = (int)x;
            Y = (int)y;

            dy += (0.01f + Size / 2.0f);

            if (Y > Height + Size)
            {
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            g.DrawImage(colorPicker.getImg(), 0, 0, form.Bounds, GraphicsUnit.Pixel);
            form.Invalidate();
            System.Threading.Thread.Sleep(3000);

            int t = 33, Cnt = Count/2;
t = 11;
            var list = new System.Collections.Generic.List<myObj_070>();

            while (isAlive)
            {
                g.DrawImage(colorPicker.getImg(), 0, 0, form.Bounds, GraphicsUnit.Pixel);

                int found = 0;

                foreach (var obj in list)
                {
                    if (obj.alive)
                    {
                        found++;
                        //obj.Show();
                        //obj.Move();
                    }
                }

                string str = $"Count = {list.Count}; alive = {found}";
                g.DrawString(str, f, Brushes.Red, 100, 100);

                if (found == 0)
                {
                    g.FillRectangle(Brushes.Gray, form.Bounds);
                    t = 100;
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);

                if (list.Count < Cnt)
                {
                    list.Add(new myObj_070());
                }
            }

            br.Dispose();

            return;
        }

        // -------------------------------------------------------------------------
    };
};


// =========================================================================================================================================
// =========================================================================================================================================
// =========================================================================================================================================


namespace my
{
    public class myObj_071 : myObject
    {
        protected float x, y, dx, dy;
        protected int A = 0, R = 0, G = 0, B = 0;
        protected bool alive = false;

        protected static Pen p = null;
        protected static SolidBrush br = null;
        protected static int maxSize = 33;

        protected Bitmap bmp1 = null;
        protected Bitmap bmp2 = null;

        // -------------------------------------------------------------------------

        public myObj_071()
        {
            if (colorPicker == null)
            {
                p = new Pen(Color.White);
                br = new SolidBrush(Color.White);
                colorPicker = new myColorPicker(Width, Height, 0);

                f = new Font("Segoe UI", 11, FontStyle.Regular, GraphicsUnit.Point);

                Log($"myObj_071: colorPicker({colorPicker.getMode()})");
            }
            else
            {
                alive = true;
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            Size = rand.Next(maxSize) + 1;
            dx = rand.Next(3) - 1;
            dy = 0;

            x = X = rand.Next(Width);
            y = Y = rand.Next(Height);

Size = 66;

            //if (bmp1 == null && bmp1.Width != Size)
            {
                bmp1 = new Bitmap(Size, Size);

                using (Graphics g = Graphics.FromImage(bmp1))
                {
                    Rectangle destRect = new Rectangle(0, 0, Size, Size);
                    Rectangle srcRect = new Rectangle(X, Y, Size, Size);
                    g.DrawImage(colorPicker.getImg(), destRect, srcRect, GraphicsUnit.Pixel);
                }
            }

            return;

            // this is lame, do rewrite
            for (int i = X - Size; i < X + Size; i++)
            {
                for (int j = Y - Size; j < Y + Size; j++)
                {
                    colorPicker.setPixel(i, j);
                }
            }
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            g.DrawImage(bmp1, X, Y);

/*
            br.Color = Color.FromArgb(A, R, G, B);
            g.FillRectangle(br, X - Size, Y - Size, 2 * Size, 2 * Size);
*/
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            if (Y % 5 == 0)
            {
                x += rand.Next(3) - 1;
            }

            y += dy;
            X = (int)x;
            Y = (int)y;

            dy += (0.01f + Size / 20.0f);

            if (Y > Height + Size)
            {
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            g.DrawImage(colorPicker.getImg(), 0, 0, form.Bounds, GraphicsUnit.Pixel);
            form.Invalidate();
            System.Threading.Thread.Sleep(33);

            int t = 11, Cnt = 33, totalCnt = 0;

            var list = new System.Collections.Generic.List<myObj_071>();

            while (isAlive)
            {
                g.DrawImage(colorPicker.getImg(), 0, 0, form.Bounds, GraphicsUnit.Pixel);

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

                string str = $"total = {totalCnt++}; Count = {list.Count}; alive = {found}";
                g.FillRectangle(Brushes.Black, 50, 50, 333, 33);
                g.DrawString(str, f, Brushes.Red, 50, 50);

                form.Invalidate();
                System.Threading.Thread.Sleep(t);

                if (list.Count < Cnt)
                {
                    list.Add(new myObj_071());
                }
            }

            br.Dispose();

            return;
        }

        // -------------------------------------------------------------------------
    };
};
