using System;
using System.Drawing;



namespace my
{
    public class myObj_040 : myObject
    {
        static Pen p = null;
        static SolidBrush br = null;
        static int x0 = Width  / 2;
        static int y0 = Height / 2;
        static int shape = 0;
        static int moveType = 0;

        float x, y, dx, dy, a, size;
        int A = 0, R = -1, G = -1, B = -1;

        public myObj_040()
        {
            if (p == null)
            {
                p = new Pen(Color.White);
                br = new SolidBrush(Color.White);
                shape = rand.Next(2);
                moveType = rand.Next(2);
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected virtual void generateNew()
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

        public override void Move()
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

        protected void Show(Graphics g)
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

        public override void Process(System.Windows.Forms.Form form, ref bool isAlive)
        {
            // Using form's background image as our drawing surface
            Bitmap buffer = new Bitmap(Width, Height);      // set the size of the image
            Graphics g = Graphics.FromImage(buffer);        // set the graphics to draw on the image
            form.BackgroundImage = buffer;                  // set the PictureBox's image to be the buffer

            int cnt = 0, t = 22;

t = 5;

            var list = new System.Collections.Generic.List<myObj_040>();

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            while (isAlive)
            {
                foreach (var s in list)
                {
                    s.Show(g);
                    s.Move();
                    //g.FillRectangle(Brushes.DarkRed, s.x, s.y, s.Size, s.Size);
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

            g.Dispose();
            isAlive = true;

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






namespace my
{
    public class myObj_004_b : myObject
    {
        private int dx, dy, a;

        public myObj_004_b()
        {
            dx = 0;
            dy = 0;

            do
            {

                X = rand.Next(Width);
                Y = rand.Next(Height);

                int speed = rand.Next(20) + 1;

                speed = 3;

                int x0 = Width / 2;
                int y0 = Height / 2;

                int dist = (int)Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));

                dx = (X - x0) * speed / dist;
                dy = (Y - y0) * speed / dist;

            }
            while (dx == 0 && dy == 0);

            Size = 3;
            a = 0;
        }

        // -------------------------------------------------------------------------

        private void getNew()
        {
            dx = 0;
            dy = 0;

            do
            {

                X = rand.Next(Width);
                Y = rand.Next(Height);

                a = 0;

                int speed = 5;

                int x0 = Width / 2;
                int y0 = Height / 2;

                //int dist = (int)Math.Sqrt((X - x0)*(X - x0) + (Y - y0)*(Y - y0));

                double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));

                dx = (int)((X - x0) * speed / dist);
                dy = (int)((Y - y0) * speed / dist);

                Size = rand.Next(6) + 1;
            }
            while (dx == 0 && dy == 0);
        }

        public override void Move()
        {
            X += dx;
            Y += dy;

            X += dx + (int)(Math.Sin(Y) * 5);
            Y += dy + (int)(Math.Sin(X) * 5);

            if (X < 0 || X > Width || Y < 0 || Y > Height)
            {
                getNew();
            }

            if (a != 255)
                a++;

            return;
        }

        // -------------------------------------------------------------------------

        // Using form's background image as our drawing surface
        public override void Process(System.Windows.Forms.Form form, ref bool isAlive)
        {
            Bitmap buffer = new Bitmap(Width, Height);      // set the size of the image
            Graphics g = Graphics.FromImage(buffer);        // set the graphics to draw on the image
            form.BackgroundImage = buffer;                  // set the PictureBox's image to be the buffer

            int cnt = 0;

            var list = new System.Collections.Generic.List<myObj_004_b>();

            for (int i = 0; i < Count; i++)
            {
                list.Add(new myObj_004_b());
            }

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            using (SolidBrush br = new SolidBrush(Color.FromArgb(250, 250, 250, 250)))
            {
                while (isAlive)
                {
                    foreach (var s in list)
                    {
                        br.Color = Color.FromArgb(s.a, br.Color.R, br.Color.G, br.Color.B);

                        //g.FillRectangle(Brushes.Black, s.X, s.Y, 2, 2);
                        g.FillRectangle(br, s.X, s.Y, s.Size, s.Size);
                        s.Move();
                        //g.FillRectangle(Brushes.Red, s.X, s.Y, 2, 2);
                    }

                    form.Invalidate();
                    //System.Threading.Thread.Sleep(33);
                    System.Threading.Thread.Sleep(11);

                    if (++cnt > 1001)
                    {
                        //g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
                        getNewBrush(br);
                        cnt = 0;
                    }
                }
            }

            g.Dispose();
            isAlive = true;

            return;
        }

        // -------------------------------------------------------------------------

        private void getNewBrush(SolidBrush br)
        {
            int alpha = 0, R = 0, G = 0, B = 0, max = 256;

            while (alpha + R + G + B < 100)
            {
                alpha = rand.Next(max - 75) + 75;
                R = rand.Next(max);
                G = rand.Next(max);
                B = rand.Next(max);
            }

            br.Color = Color.FromArgb(alpha, R, G, B);
        }
    };
};







namespace my
{
    public class myObj_004_c : myObject
    {
        private int dx, dy, a;

        public myObj_004_c()
        {
            dx = 0;
            dy = 0;

            do
            {

                X = rand.Next(Width);
                Y = rand.Next(Height);

                int speed = rand.Next(20) + 1;

                speed = 3;

                int x0 = Width / 2;
                int y0 = Height / 2;

                int dist = (int)Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));

                dx = (X - x0) * speed / dist;
                dy = (Y - y0) * speed / dist;

            }
            while (dx == 0 && dy == 0);

            Size = 3;
            a = 0;
        }

        // -------------------------------------------------------------------------

        private void getNew()
        {
            dx = 0;
            dy = 0;

            do
            {

                X = rand.Next(Width);
                Y = rand.Next(Height);

                a = 0;

                int speed = 5;

                int x0 = Width / 2;
                int y0 = Height / 2;

                //int dist = (int)Math.Sqrt((X - x0)*(X - x0) + (Y - y0)*(Y - y0));

                double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));

                dx = (int)((X - x0) * speed / dist);
                dy = (int)((Y - y0) * speed / dist);

                Size = rand.Next(6) + 1;
            }
            while (dx == 0 && dy == 0);
        }

        public override void Move()
        {
            //X += dx;
            //Y += dy;

            //X += dx + (int)(Math.Sin(Y) * 5);
            //Y += dy + (int)(Math.Sin(X) * 5);

            X += (int)(Math.Sin(Y) * 5);
            Y += (int)(Math.Sin(X) * 5);


            if (X < 0 || X > Width || Y < 0 || Y > Height)
            {
                getNew();
            }

            if (a != 255)
                a++;

            return;
        }

        // -------------------------------------------------------------------------

        // Using form's background image as our drawing surface
        public override void Process(System.Windows.Forms.Form form, ref bool isAlive)
        {
            Bitmap buffer = new Bitmap(Width, Height);      // set the size of the image
            Graphics g = Graphics.FromImage(buffer);        // set the graphics to draw on the image
            form.BackgroundImage = buffer;                  // set the PictureBox's image to be the buffer

            int cnt = 0;

            var list = new System.Collections.Generic.List<myObj_004_c>();

            for (int i = 0; i < Count; i++)
            {
                list.Add(new myObj_004_c());
            }

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            using (SolidBrush br = new SolidBrush(Color.FromArgb(250, 250, 250, 250)))
            {
                while (isAlive)
                {
                    /*
                                        if ((cnt % 500) == 0 && cnt > 0)
                                        {
                                            br.Color = Color.FromArgb(1, 0, 0, 0);
                                            g.FillRectangle(br, 0, 0, Width, Height);
                                        }
                    */
                    foreach (var s in list)
                    {
                        br.Color = Color.FromArgb(s.a, br.Color.R, br.Color.G, br.Color.B);

                        //g.FillRectangle(Brushes.Black, s.X, s.Y, 2, 2);
                        g.FillRectangle(br, s.X, s.Y, s.Size, s.Size);
                        s.Move();
                        //g.FillRectangle(Brushes.Red, s.X, s.Y, 2, 2);
                    }

                    form.Invalidate();
                    //System.Threading.Thread.Sleep(33);
                    //System.Threading.Thread.Sleep(11);
                    System.Threading.Thread.Sleep(0);

                    if (++cnt > 1001)
                    {
                        //g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
                        getNewBrush(br);
                        cnt = 0;
                    }
                }
            }

            g.Dispose();
            isAlive = true;

            return;
        }

        // -------------------------------------------------------------------------

        private void getNewBrush(SolidBrush br)
        {
            int alpha = 0, R = 0, G = 0, B = 0, max = 256;

            while (alpha + R + G + B < 100)
            {
                alpha = rand.Next(max - 75) + 75;
                R = rand.Next(max);
                G = rand.Next(max);
                B = rand.Next(max);
            }

            br.Color = Color.FromArgb(alpha, R, G, B);
        }
    };
};

