using System;
using System.Drawing;
using System.Collections.Generic;


/*
    - Randomly Roaming Squares (Snow Like)
*/


namespace my
{
    public class myObj_010 : myObject
    {
        private int dx, dy;
        int A = 0, R = 0, G = 0, B = 0;

        static bool isDimmable = false;
        static int t = 0, minX = 0, minY = 0, maxX = 0, maxY = 0, showMode = 0, maxSize = 0;
        static SolidBrush dimBrush = null;
        static List<myObject> list = null;
        static Rectangle rect;

        public myObj_010()
        {
            if (colorPicker == null)
            {
                br = new SolidBrush(Color.Red);
                colorPicker = new myColorPicker(Width, Height);
                list = new List<myObject>();

                // In case the colorPicker pints to an image, try something different
                if (colorPicker.getMode() < 2)
                {
                    showMode = rand.Next(2);
                }

                isDimmable = rand.Next(2) == 0;

                var alpha = isDimmable ? 33 + rand.Next(130) : 255;

                // Sometimes set dimBrush to very low alpha -- to have longer tails
                if (isDimmable && rand.Next(11) == 0)
                {
                    alpha = 7 + rand.Next(11);
                }

                dimBrush = new SolidBrush(Color.FromArgb(alpha, 0, 0, 0));

                // In case the border is wider than the screen's bounds, the movement looks a bit different (no bouncing)
                int offset = rand.Next(2) == 0 ? 0 : 100 + rand.Next(500);

                minX = 0 - offset;
                minY = 0 - offset;
                maxX = Width + offset;
                maxY = Height + offset;

                maxSize = showMode == 0 ? 11 : 50;

                t = 33 + rand.Next(17);

                Log($"myObj_010: colorPicker({colorPicker.getMode()})");
            }

            X = rand.Next(Width);
            Y = rand.Next(Height);

            int maxSpeed = 20;

            dx = (rand.Next(maxSpeed) + 1) * (rand.Next(2) == 0 ? 1 : -1);
            dy = (rand.Next(maxSpeed) + 1) * (rand.Next(2) == 0 ? 1 : -1);

            Size = rand.Next(maxSize) + 1;

            A = rand.Next(256 - 75) + 75;
            colorPicker.getColor(X, Y, ref R, ref G, ref B);
        }

        // -------------------------------------------------------------------------

        protected override void Move()
        {
            X += dx;
            Y += dy;

            if (X < minX || X > maxX)
            {
                dx *= -1;
            }

            if (Y < minY || Y > maxY)
            {
                dy *= -1;
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            switch (showMode)
            {
                // Solid color
                case 0:
                    br.Color = Color.FromArgb(A, R, G, B);
                    g.FillRectangle(br, X, Y, Size, Size);

                    if (Size > 3)
                    {
                        g.FillRectangle(Brushes.Black, X, Y, 1, 1);
                        g.FillRectangle(Brushes.Black, X + Size - 1, Y, 1, 1);
                        g.FillRectangle(Brushes.Black, X, Y + Size - 1, 1, 1);
                        g.FillRectangle(Brushes.Black, X + Size - 1, Y + Size - 1, 1, 1);
                    }
                    break;

                // Show underlying image at the same coordinates
                case 1:
                    if (X >= 0 && Y >= 0 && X < Width - Size && Y < Height - Size)
                    {
                        rect.X = X;
                        rect.Y = Y;
                        rect.Width = Size;
                        rect.Height = Size;

                        g.DrawImage(colorPicker.getImg(), rect, rect, GraphicsUnit.Pixel);
                    }
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            while (isAlive && list.Count < Count)
            {
                g.FillRectangle(dimBrush, 0, 0, Width, Height);

                foreach (myObj_010 s in list)
                {
                    s.Show();
                    s.Move();
                }

                list.Add(new myObj_010());

                form.Invalidate();
                System.Threading.Thread.Sleep(t);
            }

            while (isAlive)
            {
                g.FillRectangle(dimBrush, 0, 0, Width, Height);

                foreach (myObj_010 s in list)
                {
                    s.Show();
                    s.Move();
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(t);
            }

            return;
        }
    };
};
