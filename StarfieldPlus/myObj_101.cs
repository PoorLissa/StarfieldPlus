using System;
using System.Drawing;



namespace my
{
    public class myObj_101 : myObject
    {
        public myObj_101()
        {
            if (colorPicker == null)
            {
                colorPicker = new myColorPicker(Width, Height, rand.Next(2));
            }

            generateNew();
        }

        // -------------------------------------------------------------------------

        protected override void generateNew()
        {
        }

        // -------------------------------------------------------------------------

        protected override void Show()
        {
            //g.FillRectangle(brush, X, Y, Size, Size);
        }

        // -------------------------------------------------------------------------

        protected override void Process()
        {
            g.FillRectangle(Brushes.Black, 0, 0, Width, Height);

            using (Brush br = new SolidBrush(Color.Red))
            {
                var rect = new Rectangle(1, 1, 1, 1);

                int cnt = 100, t = 0;

                int maxX = rand.Next(100) + 1;
                int maxy = rand.Next(100) + 1;
                int maxZ = rand.Next(100) + 0;

                while (isAlive)
                {
                    int x = rand.Next(Width);
                    int y = rand.Next(Height);

                    int w = rand.Next(maxX) + maxZ;
                    int h = rand.Next(maxy) + maxZ;

                    rect.X = x;
                    rect.Y = y;
                    rect.Width = w;
                    rect.Height = h;

                    // Normal image
                    //g.DrawImage(_originalScreen, x, y, rect, GraphicsUnit.Pixel);

                    g.DrawImage(colorPicker.getImg(), x / 2 + Width / 4, y / 2 + Height / 4, rect, GraphicsUnit.Pixel);

                    form.Invalidate();
                    System.Threading.Thread.Sleep(t);

                    if (--cnt == 0)
                    {
                        cnt = 1000;
                        t++;
                    }
                }
            }

            return;
        }
    }
};
