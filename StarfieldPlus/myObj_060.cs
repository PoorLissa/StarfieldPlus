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
            if (_colorPicker == null)
            {
                // Use only Desktop snapshots
                _colorPicker = new myColorPicker(Width, Height, 0);
                br = new SolidBrush(Color.White);
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected virtual void generateNew()
        {
        }

        // -------------------------------------------------------------------------

        private object lockObject = new object();

        public override void Move()
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
            catch (Exception ex)
            {
                ;
            }
        }

        // -------------------------------------------------------------------------

        protected void Show(Graphics g)
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

        public override void Process(System.Windows.Forms.Form form, ref bool isAlive)
        {
            // Using form's background image as our drawing surface
            buffer = new Bitmap(Width, Height);             // set the size of the image
            Graphics g = Graphics.FromImage(buffer);        // set the graphics to draw on the image
            form.BackgroundImage = buffer;                  // set the PictureBox's image to be the buffer

            int t = 0;

            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);
            g.DrawImage(_colorPicker.getImg(), 0, 0, form.Bounds, GraphicsUnit.Pixel);

            while (isAlive)
            {
                Size = rand.Next(333) + 10;
                X = rand.Next(Width - Size);
                Y = rand.Next(Height - Size);

                var rect1 = new Rectangle(X, Y, Size, Size);

                X = rand.Next(Width);
                Y = rand.Next(Height);

                var rect2 = new Rectangle(X, Y, Size, Size);

                g.DrawImage(_colorPicker.getImg(), rect1, rect2, GraphicsUnit.Pixel);
                g.DrawImage(_colorPicker.getImg(), rect2, rect1, GraphicsUnit.Pixel);

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

            g.Dispose();
            isAlive = true;

            return;
        }
    };
};
