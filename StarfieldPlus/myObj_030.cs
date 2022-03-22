using System;
using System.Drawing;



namespace my
{
    public class myObj_030 : myObject
    {
        protected float x, y, dx, dy;
        protected int lifeCounter = -1;
        protected int A = 0, R = 0, G = 0, B = 0;

        protected static Pen p = null;
        protected static SolidBrush br = null;

        protected bool isSlow = false;

        public myObj_030()
        {
            if (colorPicker == null)
            {
                p = new Pen(Color.White);
                br = new SolidBrush(Color.White);
                colorPicker = new myColorPicker(Width, Height);
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            isSlow = false;

            if (lifeCounter == -1)
            {
                lifeCounter = rand.Next(100);
            }
            else
            {
                lifeCounter = rand.Next(100) + 100;
            }

            int rnd = rand.Next(1000);
            int maxSize = 13;

            if (rnd < 100)
                maxSize = 27;

            if (rnd == 666)
                maxSize = 123;

            if (rnd > 750)
            {
                isSlow = true;
                maxSize = 2;
                lifeCounter = rand.Next(5) + 1;
            }

            Size = rand.Next(maxSize) + 1;
            A = rand.Next(256);
            X = rand.Next(Width);
            Y = isSlow ? rand.Next(Height) : 1;

            x = X;
            y = Y;

            dx = 0;
            dy = 0;

            colorPicker.getColor(X, Y, ref R, ref G, ref B);
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            if (isSlow)
            {
                if (Y % 5 == 0)
                {
                    x += rand.Next(3) - 1;
                }

                y += dy;
                X = (int)x;
                Y = (int)y;

                dy += (0.01f + Size / 20.0f);

                if (Y >= Height)
                {
                    generateNew();
                }
            }
            else
            {
                if (lifeCounter == 0)
                {
                    x += dx;
                    y += dy;
                    X = (int)x;
                    Y = (int)y;

                    dy += (1.0f + Size / 10.0f);

                    if (Y >= Height)
                    {
                        dy *= -0.025f * (rand.Next(11) + 1);

                        dx += 0.5f * (rand.Next(10)) * (rand.Next(3) - 1);

                        if (dy > -1)
                        {
                            generateNew();
                        }
                    }
                }
                else
                {
                    if (--lifeCounter == 0)
                    {
                        dy = -0.1f * (rand.Next(20) + 10);
                    }
                }
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            br.Color = Color.FromArgb(A, R, G, B);

            if (Size < 3)
            {
                g.FillRectangle(br, X, Y, Size, Size);
            }
            else
            {
                if (Y == 1)
                {
                    g.FillEllipse(br, X, Y - Size / 2, Size, Size);
                }
                else
                {
                    g.FillEllipse(br, X, Y, Size, Size);

                    p.Color = Color.FromArgb((A < 225 ? A + 30 : A), R, G, B);
                    g.DrawEllipse(p, X, Y, Size, Size);
                }
            }
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int t = 33;

            var list = new System.Collections.Generic.List<myObj_030>();

            while (isAlive)
            {
                g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                foreach (var s in list)
                {
                    s.Show();
                    s.Move();
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);

                if (list.Count < Count + 150)
                {
                    list.Add(new myObj_030());
                }
            }

            br.Dispose();

            return;
        }
    };
};







namespace my
{
    public class myObj_031 : myObject
    {
        protected float x, y, dx, dy;
        protected int A = 0, R = 0, G = 0, B = 0;

        protected static Pen p = null;
        protected static SolidBrush br = null;

        public myObj_031()
        {
            if (colorPicker == null)
            {
                p = new Pen(Color.White);
                br = new SolidBrush(Color.White);
                colorPicker = new myColorPicker(Width, Height, rand.Next(2));
            }

            generateNew();
        }

        protected override void generateNew()
        {
            do
            {
                Size = rand.Next(11) + 1;
            }
            while (Size % 2 != 1);

            dx = rand.Next(3)-1;
            dy = 0;

            do {
                A = rand.Next(56) + 200;
                X = rand.Next(Width);
                Y = rand.Next(Height);
                colorPicker.getColor(X, Y, ref R, ref G, ref B);
            }
            while (R == 0 && G == 0 && B == 0);

            x = X;
            y = Y;

            int half = Size / 2;

            for (int i = X - half; i < X + half; i++)
            {
                for (int j = Y - half; j < Y + half; j++)
                {
                    colorPicker.setPixel(i, j);
                }
            }
        }

        protected override void Show()
        {
            int half = Size / 2;

            br.Color = Color.FromArgb(A, R, G, B);
            g.FillRectangle(br, X - half, Y - half, Size, Size);
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

            if (Y >= Height)
            {
                generateNew();
            }
        }

        protected override void Process()
        {
            int t = 33;

            t = 11;

            var list = new System.Collections.Generic.List<myObj_031>();

            while (isAlive)
            {
                g.DrawImage(colorPicker.getImg(), 0, 0, form.Bounds, GraphicsUnit.Pixel);

                foreach (var s in list)
                {
                    s.Show();
                    s.Move();
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);

                if (list.Count < Count + 2150)
                {
                    list.Add(new myObj_031());
                }
            }

            br.Dispose();

            return;
        }

    };
};
