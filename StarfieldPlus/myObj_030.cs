﻿using System;
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
            if (_colorPicker == null)
            {
                p = new Pen(Color.White);
                br = new SolidBrush(Color.White);
                _colorPicker = new myColorPicker(Width, Height);
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected virtual void generateNew()
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

            _colorPicker.getColor(X, Y, ref R, ref G, ref B);
        }

        // -------------------------------------------------------------------------

        public override void Move()
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

        protected virtual void Show(Graphics g)
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

        // Using form's background image as our drawing surface
        public override void Process(System.Windows.Forms.Form form, ref bool isAlive)
        {
            Bitmap buffer = new Bitmap(Width, Height);      // set the size of the image
            Graphics g = Graphics.FromImage(buffer);        // set the graphics to draw on the image
            form.BackgroundImage = buffer;                  // set the PictureBox's image to be the buffer

            int t = 33;

            var list = new System.Collections.Generic.List<myObj_030>();

            while (isAlive)
            {
                g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                foreach (var s in list)
                {
                    s.Show(g);
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
            g.Dispose();
            isAlive = true;

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
            if (_colorPicker == null)
            {
                p = new Pen(Color.White);
                br = new SolidBrush(Color.White);
                _colorPicker = new myColorPicker(Width, Height, rand.Next(2));
            }

            generateNew();
        }

        protected void generateNew()
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
                _colorPicker.getColor(X, Y, ref R, ref G, ref B);
            }
            while (R == 0 && G == 0 && B == 0);

            x = X;
            y = Y;

            int half = Size / 2;

            for (int i = X - half; i < X + half; i++)
            {
                for (int j = Y - half; j < Y + half; j++)
                {
                    _colorPicker.setPixel(i, j);
                }
            }
        }

        protected void Show(Graphics g)
        {
            int half = Size / 2;

            br.Color = Color.FromArgb(A, R, G, B);
            g.FillRectangle(br, X - half, Y - half, Size, Size);
        }

        public override void Move()
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

        public override void Process(System.Windows.Forms.Form form, ref bool isAlive)
        {
            Bitmap buffer = new Bitmap(Width, Height);      // set the size of the image
            Graphics g = Graphics.FromImage(buffer);        // set the graphics to draw on the image
            form.BackgroundImage = buffer;                  // set the PictureBox's image to be the buffer

            int t = 33;

            t = 11;

            var list = new System.Collections.Generic.List<myObj_031>();

            while (isAlive)
            {
                g.DrawImage(_colorPicker.getImg(), 0, 0, form.Bounds, GraphicsUnit.Pixel);

                foreach (var s in list)
                {
                    s.Show(g);
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
            g.Dispose();
            isAlive = true;

            return;
        }

    };
};