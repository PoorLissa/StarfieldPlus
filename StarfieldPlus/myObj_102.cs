using System;
using System.Drawing;

/*
    - get random rectangle at the original screen
    - calculate average color in this rectangle
    - put figure (rect or sircle) filled with this average color at the same place
*/

namespace my
{
    public class myObj_102 : myObject
    {
        public myObj_102()
        {
            generateNew();
        }

        // -------------------------------------------------------------------------

        public override void takeSnapshot()
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
        }

        // -------------------------------------------------------------------------

        private void getSize(ref int size, ref int cnt)
        {
            switch (rand.Next(5))
            {
                case 0:
                    size = 666;
                    break;

                case 1:
                    size = 333;
                    break;

                case 2:
                    size = 111;
                    break;

                case 3:
                    size = 33;
                    break;

                case 4:
                    size = 15;
                    break;
            }

            size = rand.Next(size) + 2;

            if (size < 25)
                cnt = 10000000;
            else if (size < 50)
                cnt = 1000000;
            else if (size < 150)
                cnt = 100000;
            else if (size < 350)
                cnt = 10000;
            else
                cnt = 1000;

            return;
        }

        // -------------------------------------------------------------------------

        private void getAvgColor(SolidBrush br, int x, int y, int w, int h)
        {
            Color clr;
            int cnt = 0, R = 0, G = 0, B = 0;

            for (int i = x; i < x + w; i++)
            {
                for (int j = y; j < y + h; j++)
                {
                    if (i < Width && j < Height)
                    {
                        clr = _originalScreen.GetPixel(i, j);

                        R += clr.R;
                        G += clr.G;
                        B += clr.B;

                        cnt++;
                    }
                }
            }

            R /= cnt;
            G /= cnt;
            B /= cnt;

            br.Color = Color.FromArgb(155, R, G, B);

            return;
        }

        // -------------------------------------------------------------------------

        private void drawRectangle(Graphics g, SolidBrush br, Pen p, int x, int y, int w, int h, int isBorder)
        {
            g.FillRectangle(br, x, y, w, h);

            switch (isBorder)
            {
                case 1:
                    g.DrawRectangle(Pens.Black, x, y, w, h);
                    break;

                case 2:
                    g.DrawRectangle(Pens.White, x, y, w, h);
                    break;

                case 3:
                    p.Color = Color.FromArgb(255, br.Color.R, br.Color.G, br.Color.B);
                    g.DrawRectangle(p, x, y, w, h);
                    break;

                case 4:
                    g.DrawRectangle(p, x, y, w, h);
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        private void drawCircle(Graphics g, SolidBrush br, Pen p, int x, int y, int w, int h, int isBorder)
        {
            g.FillEllipse(br, x, y, w, h);

            switch (isBorder)
            {
                case 1:
                    g.DrawEllipse(Pens.Black, x, y, w, h);
                    break;

                case 2:
                    g.DrawEllipse(Pens.White, x, y, w, h);
                    break;

                case 3:
                    p.Color = Color.FromArgb(255, br.Color.R, br.Color.G, br.Color.B);
                    g.DrawEllipse(p, x, y, w, h);
                    break;

                case 4:
                    g.DrawEllipse(p, x, y, w, h);
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        public override void Process(System.Windows.Forms.Form form, ref bool isAlive)
        {
            if (_originalScreen != null)
            {
                Bitmap buffer = new Bitmap(Width, Height);      // set the size of the image
                Graphics g = Graphics.FromImage(buffer);        // set the graphics to draw on the image
                form.BackgroundImage = buffer;                  // set the PictureBox's image to be the buffer

                switch (rand.Next(2))
                {
                    case 0:
                        proc1(form, g, ref isAlive);
                        break;

                    case 1:
                        proc2(form, g, ref isAlive);
                        break;
                }

                g.Dispose();
                isAlive = true;
            }

            return;
        }

        // -------------------------------------------------------------------------

        private void proc1(System.Windows.Forms.Form form, Graphics g, ref bool isAlive)
        {
            using (SolidBrush br = new SolidBrush(Color.Red))
            using (Pen p = new Pen(Color.Red))
            {
                int a = rand.Next(256);
                int R = rand.Next(256);
                int G = rand.Next(256);
                int B = rand.Next(256);

                // Initial background
                br.Color = Color.FromArgb(a, R, G, B);
                g.FillRectangle(br, 0, 0, Width, Height);

                int size = 0, cnt = 0, t = 0;
                int maxX = rand.Next(100) + 1;
                int maxy = rand.Next(100) + 1;
                int maxZ = rand.Next(100) + 1;
                int isBorder = rand.Next(5);

                if (isBorder == 4)
                {
                    a = rand.Next(256);
                    R = rand.Next(256);
                    G = rand.Next(256);
                    B = rand.Next(256);

                    // Initial background
                    p.Color = Color.FromArgb(a, R, G, B);
                }

                getSize(ref size, ref cnt);

                while (isAlive)
                {
                    int x = rand.Next(Width);
                    int y = rand.Next(Height);

                    int w = size;
                    int h = size;

                    getAvgColor(br, x, y, w, h);
                    drawRectangle(g, br, p, x, y, w, h, isBorder: isBorder);

                    form.Invalidate();
                    System.Threading.Thread.Sleep(t);

                    if (--cnt == 0)
                    {
                        cnt = 10000;
                        t++;
                    }
                }
            }

            return;
        }

        // -------------------------------------------------------------------------

        private void proc2(System.Windows.Forms.Form form, Graphics g, ref bool isAlive)
        {
            using (SolidBrush br = new SolidBrush(Color.Red))
            using (Pen p = new Pen(Color.Red))
            {
                int a = rand.Next(256);
                int R = rand.Next(256);
                int G = rand.Next(256);
                int B = rand.Next(256);

                // Initial background
                br.Color = Color.FromArgb(a, R, G, B);
                g.FillRectangle(br, 0, 0, Width, Height);

                int size = 0, cnt = 0, t = 0;
                int maxX = rand.Next(100) + 1;
                int maxy = rand.Next(100) + 1;
                int maxZ = rand.Next(100) + 1;
                int isBorder = rand.Next(5);

                if (isBorder == 4)
                {
                    a = rand.Next(256);
                    R = rand.Next(256);
                    G = rand.Next(256);
                    B = rand.Next(256);

                    // Initial background
                    p.Color = Color.FromArgb(a, R, G, B);
                }

                getSize(ref size, ref cnt);

                while (isAlive)
                {
                    int x = rand.Next(Width);
                    int y = rand.Next(Height);

                    int w = size;
                    int h = size;

                    getAvgColor(br, x, y, w, h);
                    drawCircle(g, br, p, x, y, w, h, isBorder: isBorder);

                    form.Invalidate();
                    System.Threading.Thread.Sleep(t);

                    if (--cnt == 0)
                    {
                        cnt = 10000;
                        t++;
                    }
                }
            }

            return;
        }

        // -------------------------------------------------------------------------
    }





    public class myObj_103 : myObj_102
    {
        public myObj_103()
        {
            generateNew();
        }

        // -------------------------------------------------------------------------

        public override void takeSnapshot()
        {
            // Get desktop snapshot
/*
            string currentWallpaper = new string('\0', MAX_PATH);
            Win32.SystemParametersInfo(SPI_GETDESKWALLPAPER, currentWallpaper.Length, currentWallpaper, 0);
            return currentWallpaper.Substring(0, currentWallpaper.IndexOf('\0'));
*/

            string currentWallpaper = "E:\\iNet\\pix\\wallpapers_3840x1600\\_dsc_00341_0_ultrawide.jpg";
            currentWallpaper = "E:\\iNet\\pix\\wallpapers_3840x1600\\0jcj71fni5111.png";
            currentWallpaper = "E:\\iNet\\pix\\05-01-11.gif";

            _originalScreen = new Bitmap(currentWallpaper);

            if (_originalScreen.Width < Width || _originalScreen.Height < Height)
            {
                _originalScreen = new Bitmap(_originalScreen, Width, Height);
            }
        }
    }

};
