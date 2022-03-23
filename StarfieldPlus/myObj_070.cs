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

        public myObj_070()
        {
            if (colorPicker == null)
            {
                p = new Pen(Color.White);
                br = new SolidBrush(Color.White);
                //colorPicker = new myColorPicker(Width, Height, rand.Next(2));
                colorPicker = new myColorPicker(Width, Height, 1);

                Log($"myObj_070: colorPicker({colorPicker.getMode()})");
            }

            alive = true;

            generateNew();
        }

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

            int half = Size / 2;

            for (int i = X - Size; i < X + Size; i++)
            {
                for (int j = Y - Size; j < Y + Size; j++)
                {
                    colorPicker.setPixel(i, j);
                }
            }
        }

        protected override void Show()
        {
            br.Color = Color.FromArgb(A, R, G, B);
            g.FillRectangle(br, X - Size, Y - Size, 2*Size, 2*Size);
        }

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

        protected override void Process()
        {
            g.DrawImage(colorPicker.getImg(), 0, 0, form.Bounds, GraphicsUnit.Pixel);
            System.Threading.Thread.Sleep(3000);

            int t = 33;

            t = 11;

            var list = new System.Collections.Generic.List<myObj_070>();

            while (isAlive)
            {
                g.DrawImage(colorPicker.getImg(), 0, 0, form.Bounds, GraphicsUnit.Pixel);

                bool foundLive = false;

                foreach (var s in list)
                {
                    if (alive)
                    {
                        foundLive = true;
                        s.Show();
                        s.Move();
                    }
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);

                if (list.Count < Count + 5000)
                {
                    list.Add(new myObj_070());
                }

                if (!foundLive)
                {
                    g.FillRectangle(Brushes.DarkGray, form.Bounds);
                    t = 100;
                }
            }

            br.Dispose();

            return;
        }

    };
};
