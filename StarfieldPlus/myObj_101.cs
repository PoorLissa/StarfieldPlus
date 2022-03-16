using System;
using System.Drawing;



namespace my
{
    public class myObj_101 : myObject
    {
        public myObj_101()
        {
            generateNew();
        }

        // -------------------------------------------------------------------------

        public override void getImage()
        {
            // Get desktop snapshot
            _originalScreen = new Bitmap(Width, Height);
            
            using (Graphics g = Graphics.FromImage(_originalScreen))
            {
                g.CopyFromScreen(Point.Empty, Point.Empty, new Size(Width, Height));
            }
        }

        // -------------------------------------------------------------------------

        protected virtual void generateNew()
        {
        }

        // -------------------------------------------------------------------------

        protected virtual void Show(Graphics g)
        {
            //g.FillRectangle(brush, X, Y, Size, Size);
        }

        // -------------------------------------------------------------------------

        public override void Process(System.Windows.Forms.Form form, ref bool isAlive)
        {
            if (_originalScreen != null)
            {
                Bitmap buffer = new Bitmap(Width, Height);      // set the size of the image
                Graphics g = Graphics.FromImage(buffer);        // set the graphics to draw on the image
                form.BackgroundImage = buffer;                  // set the PictureBox's image to be the buffer

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

                        g.DrawImage(_originalScreen, x / 2 + Width / 4, y / 2 + Height / 4, rect, GraphicsUnit.Pixel);

                        form.Invalidate();
                        System.Threading.Thread.Sleep(t);

                        if (--cnt == 0)
                        {
                            cnt = 1000;
                            t++;
                        }
                    }
                }

                g.Dispose();
                isAlive = true;
            }

            return;
        }
    }
};
