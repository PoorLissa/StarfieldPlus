using System;
using System.Drawing;

/*
    - 'Defragmenting' the screen
*/

namespace my
{
    public class myObj_060 : myObject
    {
        static int x = -1, y = -1;
        Bitmap buffer = null;
        static SolidBrush br = null;

        public myObj_060()
        {
            if (colorPicker == null)
            {
                // Use only Desktop snapshots
                colorPicker = new myColorPicker(Width, Height, rand.Next(2));
                br = new SolidBrush(Color.White);

                Log("myObj_060");
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
        }

        // -------------------------------------------------------------------------

        private object lockObject = new object();

        protected override void Move()
        {
            try
            {
                X = rand.Next(Width);
                Y = rand.Next(Height);

                lock (lockObject)
                {
                    Color clr = buffer.GetPixel(X, Y);
                    int total = clr.R + clr.G + clr.B;

                    //for (int j = 0; j < Height; j++)
                    for (int j = 0; j < 500; j++)
                    {
                        //for (int i = 0; i < Width; i++)
                        for (int i = 0; i < 500; i++)
                        {
                            clr = buffer.GetPixel(i, j);

                            if (total >= clr.R + clr.G + clr.B)
                            {
                                x = i;
                                y = j;
                                return;
                            }
                        }
                    }
                }

                x = -1;
                y = -1;
            }
            catch (Exception)
            {
                ;
            }
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            Color clr1 = buffer.GetPixel(x, y);
            Color clr2 = buffer.GetPixel(X, Y);

            br.Color = clr2;
            g.FillRectangle(br, x, y, 1, 1);
            br.Color = clr1;
            g.FillRectangle(br, X, Y, 1, 1);

            //g.FillRectangle(Brushes.Red, X, Y, 1, 1);

            //g.FillRectangle(Brushes.White, x, y, 1, 1);
            //g.FillRectangle(Brushes.Black, X, Y, 1, 1);
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            int t = 0;

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            g.DrawImage(colorPicker.getImg(), 0, 0, form.Bounds, GraphicsUnit.Pixel);

            while (isAlive)
            {
                Size = rand.Next(50) + 10;
                X = rand.Next(Width - Size);
                Y = rand.Next(Height - Size);

                var rect1 = new Rectangle(X, Y, Size, Size);

                X = rand.Next(Width);
                Y = rand.Next(Height);

                var rect2 = new Rectangle(X, Y, Size, Size);

                g.DrawImage(colorPicker.getImg(), rect1, rect2, GraphicsUnit.Pixel);
                g.DrawImage(colorPicker.getImg(), rect2, rect1, GraphicsUnit.Pixel);

/*
                Move();

                if (x < 0 && y < 0)
                {
                    isAlive = false;
                    break;
                }

                Show(g);
*/
                form.Invalidate();
                System.Threading.Thread.Sleep(t++);
            }

            return;
        }
    };
};
