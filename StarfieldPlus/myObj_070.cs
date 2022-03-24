using System;
using System.Drawing;

/*
    - Pieces drop off the desktop and fall down -- random positions and sizes of the tiles

    todo:
        look for target color across all the cell, not only in a single pixel
*/

namespace my
{
    public class myObj_070 : myObject
    {
        protected float x, y, dx, dy;
        protected int A = 0, R = 0, G = 0, B = 0;
        protected bool alive = false;

        protected static int maxSize = 66;
        protected static int dim_A = 0;

        // -------------------------------------------------------------------------

        public myObj_070()
        {
            if (colorPicker == null)
            {
                p = new Pen(Color.Black);
                br = new SolidBrush(Color.White);
                colorPicker = new myColorPicker(Width, Height, rand.Next(2));
                f = new Font("Segoe UI", 11, FontStyle.Regular, GraphicsUnit.Point);

                dim_A = rand.Next(33) + 33;

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
                    maxSize -= (maxSize == 5) ? 0 : 1;
                    return;
                }
            }
            while (R == 0 && G == 0 && B == 0);

            x = X;
            y = Y;

            // Dim the tile on the src image
            br.Color = Color.FromArgb(dim_A, 0, 0, 0);
            colorPicker.GetGraphics().FillRectangle(br, X - Size, Y - Size, 2*Size, 2*Size);
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            br.Color = Color.FromArgb(A, R, G, B);
            g.FillRectangle(br, X - Size, Y - Size, 2*Size, 2*Size);
            g.DrawRectangle(p, X - Size, Y - Size, 2 * Size, 2 * Size);
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

            dy += (0.01f + Size / 5.0f);

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

            int t = 11, Cnt = 100, totalCnt = 0;
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
                        obj.Show();
                        obj.Move();
                    }
                }

#if DEBUG
                string str = $"total = {totalCnt++}; Count = {list.Count}; alive = {found}";
                g.FillRectangle(Brushes.Black, 50, 50, 333, 33);
                g.DrawString(str, f, Brushes.Red, 50, 50);

#endif
                if (found == 0)
                {
                    g.FillRectangle(Brushes.Black, form.Bounds);
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
