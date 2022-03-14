using System;
using System.Drawing;



namespace my
{
    public class myObj_004 : myObject
    {
        private int dx, dy;

        public myObj_004()
        {
            X = rand.Next(Width);
            Y = rand.Next(Height);

            int speed = rand.Next(20) + 1;

            speed = 2;

            int x0 = Width  / 2;
            int y0 = Height / 2;

            int dist = (int)Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));

            dx = (X - x0) * speed / dist;
            dy = (Y - y0) * speed / dist;

            Size = 3;
        }

        // -------------------------------------------------------------------------

        public override void Move()
        {
            X += dx;
            Y += dy;

            X += dx + (int)(Math.Sin(Y) * 5);
            Y += dy + (int)(Math.Sin(X) * 5);

            if (X < 0 || X > Width || Y < 0 || Y > Height)
            {
                X = rand.Next(Width);
                Y = rand.Next(Height);

                int speed = 5;

                int x0 = Width  / 2;
                int y0 = Height / 2;

                //int dist = (int)Math.Sqrt((X - x0)*(X - x0) + (Y - y0)*(Y - y0));

                double dist = Math.Sqrt((X - x0) * (X - x0) + (Y - y0) * (Y - y0));

                dx = (int)((X - x0) * speed / dist);
                dy = (int)((Y - y0) * speed / dist);

                Size = rand.Next(6) + 1;
            }

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

            var list = new System.Collections.Generic.List<myObj_004>();

            for (int i = 0; i < Count; i++)
            {
                list.Add(new myObj_004());
            }

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            using (SolidBrush br = new SolidBrush(Color.FromArgb(250, 250, 250, 250)))
            {
                while (isAlive)
                {
                    //g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                    foreach (var s in list)
                    {
                        g.FillRectangle(br, s.X, s.Y, s.Size, s.Size);
                        s.Move();
                        g.FillRectangle(Brushes.Red, s.X, s.Y, s.Size, s.Size);
                    }

                    form.Invalidate();
                    //System.Threading.Thread.Sleep(33);
                    System.Threading.Thread.Sleep(11);

                    if (++cnt > 1000)
                    {
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
