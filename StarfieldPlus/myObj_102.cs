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
        static int avgColorMode = 0;

        public myObj_102()
        {
            if (colorPicker == null)
            {
                colorPicker = new myColorPicker(Width, Height, rand.Next(2));
                avgColorMode = rand.Next(2);
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

            bool isRandomSize = rand.Next(2) == 0;
            int tmp = size;

            size = rand.Next(size);

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

            if (isRandomSize)
            {
                size = -tmp;
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected virtual void getAvgColor(SolidBrush br, int x, int y, int w, int h)
        {
            switch (avgColorMode)
            {
                case 0: {

                        int cnt = 0, R = 0, G = 0, B = 0, r = 0, g = 0, b = 0;

                        for (int i = x; i < x + w; i++)
                        {
                            for (int j = y; j < y + h; j++)
                            {
                                if (i > -1 && j > -1 && i < Width && j < Height)
                                {
                                    colorPicker.getColor(i, j, ref r, ref g, ref b);

                                    R += r;
                                    G += g;
                                    B += b;

                                    cnt++;
                                }
                            }
                        }

                        R /= cnt;
                        G /= cnt;
                        B /= cnt;

                        br.Color = Color.FromArgb(155, R, G, B);
                    }
                    break;

                case 1: {

                        if (x < 0)
                            x = 0;

                        if (y < 0)
                            y = 0;

                        if (x > colorPicker.getImg().Width)
                            x = colorPicker.getImg().Width;

                        if (y > colorPicker.getImg().Height)
                            y = colorPicker.getImg().Height;

                        Color clr = colorPicker.getImg().GetPixel(x, y);
                        br.Color = Color.FromArgb(133, clr.R, clr.G, clr.B);
                    }
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        private void drawRectangle(Graphics g, SolidBrush br, Pen p, int x, int y, int w, int h, int isBorder)
        {
            if (isBorder != 5)
            {
                g.FillRectangle(br, x, y, w, h);
            }

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

                case 5:
                    p.Color = Color.FromArgb(200, br.Color.R, br.Color.G, br.Color.B);
                    g.DrawRectangle(p, x, y, w, h);
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        private void drawCircle(Graphics g, SolidBrush br, Pen p, int x, int y, int w, int h, int isBorder)
        {
            if (isBorder != 5)
            {
                g.FillEllipse(br, x, y, w, h);
            }

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

                case 5:
                    p.Color = Color.FromArgb(200, br.Color.R, br.Color.G, br.Color.B);
                    g.DrawEllipse(p, x, y, w, h);
                    break;
            }

            return;
        }

        // -------------------------------------------------------------------------

        protected override void Process(System.Windows.Forms.Form form, Graphics g, ref bool isAlive)
        {
            switch (rand.Next(2))
            {
                // Rectangles
                case 0:
                    proc1(form, g, ref isAlive);
                    break;

                // Circles
                case 1:
                    proc2(form, g, ref isAlive);
                    break;
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
                int isBorder = rand.Next(6);

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

                if (size < 0)
                {
                    // Random size on each iteration
                    int maxSize = -size/2;

                    if (maxSize == 0)
                        maxSize = 1;

                    while (isAlive)
                    {
                        size = rand.Next(maxSize) + 1;

                        int x = rand.Next(Width)  - size;
                        int y = rand.Next(Height) - size;

                        int w = 2 * size;
                        int h = 2 * size;

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
                else
                {
                    // Constant size on each iteration
                    size /= (size > 1) ? 2 : 1;

                    int w = 2 * size;
                    int h = 2 * size;

                    while (isAlive)
                    {
                        int x = rand.Next(Width)  - size;
                        int y = rand.Next(Height) - size;

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
                int isBorder = rand.Next(6);

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

                if (size < 0)
                {
                    // Random size on each iteration
                    int maxSize = -size / 2;

                    if (maxSize == 0)
                        maxSize = 1;

                    while (isAlive)
                    {
                        size = rand.Next(maxSize) + 1;

                        int x = rand.Next(Width)  - size;
                        int y = rand.Next(Height) - size;

                        int w = 2 * size;
                        int h = 2 * size;

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
                else
                {
                    // Constant size on each iteration
                    size /= (size > 1) ? 2 : 1;

                    int w = 2 * size;
                    int h = 2 * size;

                    while (isAlive)
                    {
                        int x = rand.Next(Width)  - size;
                        int y = rand.Next(Height) - size;

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
            }

            return;
        }
    }
};
