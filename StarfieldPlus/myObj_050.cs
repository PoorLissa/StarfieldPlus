using System;
using System.Drawing;

/*
    - Divide the image in horizontal lines and swap them randomly
*/

namespace my
{
    public class myObj_050 : myObject
    {
        Rectangle rect1;
        Rectangle rect2;

        int mode = 0;

        public myObj_050()
        {
            if (_colorPicker == null)
            {
                // Use only Desktop snapshots or static images
                _colorPicker = new myColorPicker(Width, Height, rand.Next(2));
                mode = rand.Next(2);
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected virtual void generateNew()
        {
            switch (mode)
            {
                case 0: {

                        var list = new System.Collections.Generic.List<int>();

                        for (int i = 1; i < 100; i++)
                            if (Height % i == 0)
                                list.Add(i);

                        Size = list[rand.Next(list.Count)];
                        X = -1;
                        Y = -1;

                        rect1 = new Rectangle(0, 0, Width, Size);
                        rect2 = new Rectangle(0, 0, Width, Size);

                    }
                    break;

                case 1:
                    break;
            }
        }

        // -------------------------------------------------------------------------

        public override void Move()
        {
            switch (mode)
            {
                case 0: {

                        do
                        {
                            Y = rand.Next(Height / Size);
                            X = rand.Next(Height / Size);
                        }
                        while (X == Y);

                        X *= Size;
                        Y *= Size;

                    }
                    break;

                case 1: {

                        Size = rand.Next(50) + 10;

                        X = rand.Next(Width - Size);
                        Y = rand.Next(Height - Size);

                        rect1.X = X;
                        rect1.Y = Y;
                        rect1.Width = Size;
                        rect1.Height = Size;

                        X = rand.Next(Width);
                        Y = rand.Next(Height);

                        rect2.X = X;
                        rect2.Y = Y;
                        rect2.Width = Size;
                        rect2.Height = Size;

                    }
                    break;
            }
        }

        // -------------------------------------------------------------------------

        protected void Show(Graphics g)
        {
            switch (mode)
            {
                case 0:
                    rect1.Y = Y;
                    rect2.Y = X;
                    g.DrawImage(_colorPicker.getImg(), rect1, rect2, GraphicsUnit.Pixel);
                    break;

                case 1:
                    g.DrawImage(_colorPicker.getImg(), rect1, rect2, GraphicsUnit.Pixel);
                    g.DrawImage(_colorPicker.getImg(), rect2, rect1, GraphicsUnit.Pixel);
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

            int t = 150;

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            g.DrawImage(_colorPicker.getImg(), 0, 0, form.Bounds, GraphicsUnit.Pixel);

            while (isAlive)
            {
                Show(g);
                Move();

                form.Invalidate();
                System.Threading.Thread.Sleep(t);
            }

            g.Dispose();
            isAlive = true;

            return;
        }
    };
};
