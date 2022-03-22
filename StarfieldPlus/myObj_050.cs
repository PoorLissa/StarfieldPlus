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
            if (colorPicker == null)
            {
                // Use only Desktop snapshots or static images
                colorPicker = new myColorPicker(Width, Height, rand.Next(2));
                mode = rand.Next(2);
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
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

        protected override void Move()
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

        protected override void Show()
        {
            switch (mode)
            {
                case 0:
                    rect1.Y = Y;
                    rect2.Y = X;
                    g.DrawImage(colorPicker.getImg(), rect1, rect2, GraphicsUnit.Pixel);
                    break;

                case 1:
                    g.DrawImage(colorPicker.getImg(), rect1, rect2, GraphicsUnit.Pixel);
                    g.DrawImage(colorPicker.getImg(), rect2, rect1, GraphicsUnit.Pixel);
                    break;
            }
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int t = 150;

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            g.DrawImage(colorPicker.getImg(), 0, 0, form.Bounds, GraphicsUnit.Pixel);

            while (isAlive)
            {
                Show();
                Move();

                form.Invalidate();
                System.Threading.Thread.Sleep(t);
            }

            return;
        }
    };
};
