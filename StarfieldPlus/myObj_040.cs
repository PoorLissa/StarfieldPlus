                                                                                                                                                                                                                                                                                                                    using System;
using System.Drawing;



namespace my
{
    public class myObj_040 : myObject
    {
        static int x0 = Width  / 2;
        static int y0 = Height / 2;
        static int shape = 0;
        static int moveType = 0;

        float x, y, dx, dy, a, size;
        int R = -1, G = -1, B = -1;

        public myObj_040()
        {
            if (p == null)
            {
                p = new Pen(Color.White);
                br = new SolidBrush(Color.White);
                shape = rand.Next(2);
                moveType = rand.Next(2);

                Log($"myObj_040: shape({shape}), moveType({moveType})");
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
            dx = 0;
            dy = 0;

            //do
            {
                x = X = rand.Next(Width);
                y = Y = rand.Next(Height);

                int speed = rand.Next(3) + 2;

                float dist = (float)(Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0)));

                dx = (int)((X - x0) * speed / dist);
                dy = (int)((Y - y0) * speed / dist);

                size = Size = rand.Next(6) + 1;
            }
            //while (dx == 0 && dy == 0);

            a = 0;
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
moveType = 0;
shape = 0;

            switch (moveType)
            {
                case 0:
                    move0();
                    break;

                case 1:
                    move1();
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            Size = (int)size;

            switch (shape)
            {
                case 0:
                    br.Color = Color.FromArgb((int)a, br.Color.R, br.Color.G, br.Color.B);
                    //g.FillRectangle(br, x, y, Size, Size);
                    g.FillRectangle(br, X, Y, Size, Size);
                    break;

                case 1:
                    br.Color = Color.FromArgb((int)a / 5, br.Color.R, br.Color.G, br.Color.B);
                    g.FillRectangle(br, x - 1, y - 1, Size + 1, Size + 1);

                    p.Color = Color.FromArgb((int)a, br.Color.R, br.Color.G, br.Color.B);
                    g.DrawRectangle(p, x, y, Size, Size);
                    break;
            }
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int cnt = 0, t = 22;
t = 1;
            var list = new System.Collections.Generic.List<myObj_040>();

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            while (myObject.isAlive)
            {
                foreach (var s in list)
                {
                    s.Show();
                    s.Move();
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);

                if (list.Count < Count)
                {
                    list.Add(new myObj_040());
                }

                if (++cnt > 1000)
                {
                    bool gotNewBrush = getNewBrush(br, cnt == 1001);

                    if (gotNewBrush)
                    {
                        cnt = 0;
                    }
                }
            }

            return;
        }

        // -------------------------------------------------------------------------

        private void move0()
        {
            X += (int)dx;
            Y += (int)dy;

            X += (int)dx + (int)(Math.Sin(Y) * 5);
            Y += (int)dy + (int)(Math.Sin(X) * 5);

            x = X;
            y = Y;

            if (a < 255)
                a += 1.0f;

            if (X < 0 || X > Width || Y < 0 || Y > Height)
            {
                generateNew();
            }
        }

        // -------------------------------------------------------------------------

        private void move1()
        {
            x += dx;
            y += dy;

            x += dx + (float)(Math.Sin(y) * 5);
            y += dy + (float)(Math.Sin(x) * 5);

            if (x < 0 || x > Width || y < 0 || y > Height)
            {
                generateNew();
            }

            if (a < 255)
                a += 0.2f;

            size += 0.05f;
        }

        // -------------------------------------------------------------------------

        private bool getNewBrush(SolidBrush br, bool doGenerate)
        {
            if (doGenerate)
            {
                while (R + G + B < 100)
                {
                    R = rand.Next(256);
                    G = rand.Next(256);
                    B = rand.Next(256);
                }
            }

            int r = br.Color.R;
            int g = br.Color.G;
            int b = br.Color.B;

            r += r == R ? 0 : r > R ? -1 : 1;
            g += g == G ? 0 : g > G ? -1 : 1;
            b += b == B ? 0 : b > B ? -1 : 1;

            br.Color = Color.FromArgb(255, r, g, b);

            return r == R && g == G && b == B;
        }
    };
};
