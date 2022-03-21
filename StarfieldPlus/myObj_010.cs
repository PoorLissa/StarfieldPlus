using System;
using System.Drawing;


/*
    - Randomly Roaming Squares (Snow Like)
*/


namespace my
{
    public class myObj_010 : myObject
    {
        private int dx, dy;
        static SolidBrush br = null;

        int A = 0, R = 0, G = 0, B = 0;

        public myObj_010()
        {
            if (_colorPicker == null)
            {
                br = new SolidBrush(Color.Red);
                _colorPicker = new myColorPicker(Width, Height);
            }

            X = rand.Next(Width);
            Y = rand.Next(Height);

            int maxSpeed = 20;

            dx = (rand.Next(maxSpeed) + 1) * (rand.Next(2) == 0 ? 1 : -1);
            dy = (rand.Next(maxSpeed) + 1) * (rand.Next(2) == 0 ? 1 : -1);

            Size = rand.Next(11) + 1;

            A = rand.Next(256 - 75) + 75;
            _colorPicker.getColor(X, Y, ref R, ref G, ref B);
        }

        // -------------------------------------------------------------------------

        public override void Move()
        {
            X += dx;
            Y += dy;

            if (X < 0 || X > Width)
            {
                dx *= -1;
            }

            if (Y < 0 || Y > Height)
            {
                dy *= -1;
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected void Show(Graphics g)
        {
            br.Color = Color.FromArgb(A, R, G, B);
            g.FillRectangle(br, X, Y, Size, Size);

            if (Size > 3)
            {
                g.FillRectangle(Brushes.Black, X,            Y,            1, 1);
                g.FillRectangle(Brushes.Black, X + Size - 1, Y,            1, 1);
                g.FillRectangle(Brushes.Black, X,            Y + Size - 1, 1, 1);
                g.FillRectangle(Brushes.Black, X + Size - 1, Y + Size - 1, 1, 1);
            }
        }

        // -------------------------------------------------------------------------

        // Using form's background image as our drawing surface
        public override void Process(System.Windows.Forms.Form form, ref bool isAlive)
        {
            Bitmap buffer = new Bitmap(Width, Height);      // set the size of the image
            Graphics g = Graphics.FromImage(buffer);        // set the graphics to draw on the image
            form.BackgroundImage = buffer;                  // set the PictureBox's image to be the buffer

            var list = new System.Collections.Generic.List<myObj_010>();

            while (isAlive)
            {
                g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

                foreach (var s in list)
                {
                    s.Show(g);
                    s.Move();
                }

                if (list.Count < Count)
                {
                    list.Add(new myObj_010());
                }

                form.Invalidate();
                System.Threading.Thread.Sleep(50);
            }

            g.Dispose();
            isAlive = true;

            return;
        }
    };
};
